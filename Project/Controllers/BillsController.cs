using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using Project.Models;
using Project.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {

        IBillRepo billRepo;
        IProductBillRepo ProductBillRepo;
        IPayedquantityRepo payedquantityRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BillsController> _logger;
        private readonly ElQasrDbContext _db;
       

        public BillsController(IBillRepo billRepo , IProductBillRepo ProductBillRepo , IPayedquantityRepo payedquantityRepo, IWebHostEnvironment environment, IConfiguration configuration, ILogger<BillsController> logger, ElQasrDbContext db)
        {
            this.billRepo = billRepo;   
            this.ProductBillRepo= ProductBillRepo; 
            this.payedquantityRepo = payedquantityRepo;
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
            _db = db;
           

        }

        // GET
        public IActionResult Index()
        {
              return View(billRepo.GetAll());
        }

        // GET
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Bill bi = billRepo.GetById(id);
                if (bi == null)
                    return NotFound();
                bi.Product_Bills = ProductBillRepo.GetAll(id);
                bi.PayedQuantities = payedquantityRepo.GetByBillId(id);
                ViewBag.InvoiceProfileSettings = _db.InvoiceProfileSettings.Find(1) ?? new InvoiceProfileSettings();
                return View(bi);
            }
               
        }

        // A clean, print-friendly invoice without billing management controls.
        public IActionResult Print(int id)
        {
            var bill = billRepo.GetById(id);
            if (bill == null)
            {
                return NotFound();
            }

            bill.Product_Bills = ProductBillRepo.GetAll(id);
            bill.PayedQuantities = payedquantityRepo.GetByBillId(id);
            ViewBag.InvoiceProfileSettings = _db.InvoiceProfileSettings.Find(1) ?? new InvoiceProfileSettings();
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> GeneratePdf(int id, [FromForm] string html)
        {
            var bill = billRepo.GetById(id);
            if (bill == null)
                return NotFound(new { message = "Invoice not found." });

            if (string.IsNullOrWhiteSpace(html) || html.Length > 10 * 1024 * 1024)
                return BadRequest(new { message = "Invoice content is empty or too large." });

            // Do not execute page scripts while the headless browser renders the invoice.
            html = System.Text.RegularExpressions.Regex.Replace(html, "<script\\b[^>]*>[\\s\\S]*?</script\\s*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            var safeBillNumber = GetSafeInvoiceFileName(bill);
            var directorySetting = _configuration["InvoicePdf:StoragePath"];
            var directory = string.IsNullOrWhiteSpace(directorySetting) || !Path.IsPathRooted(directorySetting)
                ? Path.Combine(_environment.ContentRootPath, string.IsNullOrWhiteSpace(directorySetting) ? "PDF" : directorySetting)
                : directorySetting;
            Directory.CreateDirectory(directory);

            var destination = Path.Combine(directory, safeBillNumber + ".pdf");
            var workDirectory = Path.Combine(Path.GetTempPath(), "CeramicaInvoice", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workDirectory);
            var htmlPath = Path.Combine(workDirectory, "invoice.html");
            var pdfPath = Path.Combine(workDirectory, safeBillNumber + ".pdf");

            try
            {
                await System.IO.File.WriteAllTextAsync(htmlPath, html, new UTF8Encoding(false));
                var edgePath = FindEdgeExecutable();
                if (edgePath == null)
                {
                    _logger.LogError("Unable to generate invoice PDF {BillNumber}: Microsoft Edge was not found on the server.", bill.Bill_Num);
                    return StatusCode(503, new { message = "Microsoft Edge is required on the server to generate invoice PDFs." });
                }

                var startInfo = new ProcessStartInfo(edgePath)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    WorkingDirectory = workDirectory
                };
                startInfo.ArgumentList.Add("--headless");
                startInfo.ArgumentList.Add("--disable-gpu");
                startInfo.ArgumentList.Add("--no-first-run");
                startInfo.ArgumentList.Add("--no-pdf-header-footer");
                startInfo.ArgumentList.Add("--disable-extensions");
                startInfo.ArgumentList.Add("--user-data-dir=" + Path.Combine(workDirectory, "profile"));
                startInfo.ArgumentList.Add("--print-to-pdf=" + pdfPath);
                startInfo.ArgumentList.Add(new Uri(htmlPath).AbsoluteUri);

                using var process = Process.Start(startInfo);
                if (process == null)
                {
                    _logger.LogError("Unable to start PDF renderer for invoice {BillNumber}.", bill.Bill_Num);
                    return StatusCode(503, new { message = "Could not start the PDF renderer." });
                }

                using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(60));
                try
                {
                    await process.WaitForExitAsync(timeout.Token);
                }
                catch (OperationCanceledException)
                {
                    process.Kill(true);
                    return StatusCode(504, new { message = "Invoice PDF generation timed out." });
                }

                if (process.ExitCode != 0 || !System.IO.File.Exists(pdfPath))
                {
                    var error = await process.StandardError.ReadToEndAsync();
                    _logger.LogError("PDF renderer failed for invoice {BillNumber}. ExitCode={ExitCode}; Error={RendererError}", bill.Bill_Num, process.ExitCode, error);
                    return StatusCode(500, new { message = "Invoice PDF generation failed.", detail = error });
                }

                System.IO.File.Move(pdfPath, destination, true);
                _logger.LogInformation("Invoice PDF {BillNumber} generated at {PdfPath}.", bill.Bill_Num, destination);
                return Ok(new { fileName = safeBillNumber + ".pdf", path = destination });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error generating PDF for invoice {BillNumber}.", bill.Bill_Num);
                return StatusCode(500, new { message = "Could not save the invoice PDF." });
            }
            finally
            {
                if (Directory.Exists(workDirectory))
                    Directory.Delete(workDirectory, true);
            }
        }

        private static string GetSafeInvoiceFileName(Bill bill)
        {
            var invalidCharacters = Path.GetInvalidFileNameChars()
                .Concat(new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' });
            var fileName = string.Concat((bill.Bill_Num ?? bill.ID.ToString())
                .Where(ch => !invalidCharacters.Contains(ch) && !char.IsControl(ch)))
                .Trim().TrimEnd('.', ' ');
            return string.IsNullOrWhiteSpace(fileName) ? bill.ID.ToString() : fileName;
        }

        private static string? FindEdgeExecutable()
        {
            var candidates = new[]
            {
                Environment.GetEnvironmentVariable("ProgramFiles(x86)"),
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Environment.GetEnvironmentVariable("ProgramW6432")
            }
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(path => Path.Combine(path!, "Microsoft", "Edge", "Application", "msedge.exe"));

            return candidates.FirstOrDefault(System.IO.File.Exists);
        }

        // GET
        public IActionResult Create()
        {
            var nwDt = DateTime.Now.ToShortDateString();
          
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                Bill bill1 = new Bill()
                {
                    Bill_Num = bill.Bill_Num,
                    Client_Name = bill.Client_Name,
                    Bill_State = false,
                   sell_State = false,

                      Date = bill.Date,
                     // Date = DateTime.Now.Date,
                      Exp_Date = bill.Date.AddMonths(3),
                    Client_phone = bill.Client_phone,
                    Total_Price = 0,
                    Note = bill.Note,
                    Employee_ID= null
                 };    
               billRepo.Insert(bill1);   
               return RedirectToAction("Index");
        }
            return View(bill);
    }









        // GET
        public IActionResult Edit(int id )
        {
            return View(billRepo.GetById(id));
        }

       
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Bill_Num,Client_Name,Bill_State,sell_State,Date,Exp_Date,Client_phone,Total_Price,Note")] Bill bill)
        {
          

            //if (ModelState.IsValid)
            //{
                try
                {
                    billRepo.Update(id, bill);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    return RedirectToAction("Index");
                }

            //}
            //return View(bill);
        }

        // GET
        public IActionResult Delete(int id)
        {
            Bill bill = billRepo.GetById(id);

            

            return View(bill);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            int result1 = ProductBillRepo.GetByBillId(id).Count();
            if(result1 != 0) 
            { 
                TempData["deleteProduct"] = "Please Delete All Product  in this Bill";
                return RedirectToAction("Delete", new { id = id }); 
            }
            int result2 = payedquantityRepo.GetByBillId(id).Count();
            if (result2 != 0)
            {
                TempData["deletequantity"] = "Please Delete All Payed quantity  in this Bill";
                return RedirectToAction("Delete", new { id = id });
            }
            int result = billRepo.Delete(id);
            if (result == 1) 
            { TempData["sucDelete"] = "Delete Bill is Done Succesfully";
                return RedirectToAction("Index"); 
            }
            else { ViewBag.delfail = "fail"; }
            return RedirectToAction("Delete", new { id = id });


            //int result = billRepo.Delete(id);
            //if (result != 1) { 
            //    TempData["delFail"] = "Sorry Delete is Not Done"; 
            //    return RedirectToAction("Delete", new { ID = id });
            //}
            ////========================== 
            //TempData["delsuc"] = "Delete Company is Done";
            //return RedirectToAction("Index");
        }

       
    }
}
