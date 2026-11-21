using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    [Authorize]
    public class InvoiceProfileSettingsController : Controller
    {
        private const int SettingsId = 1;
        private readonly ElQasrDbContext _db;
        private readonly ILogger<InvoiceProfileSettingsController> _logger;

        public InvoiceProfileSettingsController(ElQasrDbContext db, ILogger<InvoiceProfileSettingsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _db.InvoiceProfileSettings.FindAsync(SettingsId);
            if (settings == null)
            {
                settings = new InvoiceProfileSettings { Id = SettingsId };
                _db.InvoiceProfileSettings.Add(settings);
                await _db.SaveChangesAsync();
            }

            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(InvoiceProfileSettings submitted)
        {
            submitted.Id = SettingsId;
            if (!ModelState.IsValid)
                return View(submitted);

            var current = await _db.InvoiceProfileSettings.FindAsync(SettingsId);
            if (current == null)
            {
                current = new InvoiceProfileSettings { Id = SettingsId };
                _db.InvoiceProfileSettings.Add(current);
            }

            current.DisplayNameEn = submitted.DisplayNameEn.Trim();
            current.DisplayNameAr = submitted.DisplayNameAr.Trim();
            current.BusinessNameEn = submitted.BusinessNameEn.Trim();
            current.BusinessNameAr = submitted.BusinessNameAr.Trim();
            current.SubtitleEn = submitted.SubtitleEn?.Trim() ?? string.Empty;
            current.SubtitleAr = submitted.SubtitleAr?.Trim() ?? string.Empty;
            current.AddressEn = submitted.AddressEn?.Trim() ?? string.Empty;
            current.AddressAr = submitted.AddressAr?.Trim() ?? string.Empty;
            current.Phone = submitted.Phone?.Trim() ?? string.Empty;
            current.Email = submitted.Email?.Trim() ?? string.Empty;

            await _db.SaveChangesAsync();
            _logger.LogInformation("Invoice seller profile settings updated by {UserName}.", User.Identity?.Name);
            TempData["InvoiceProfileSettingsSaved"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
