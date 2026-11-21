using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    /// <summary>Editable bilingual seller details printed on sales invoices.</summary>
    public class InvoiceProfileSettings
    {
        [Key]
        public int Id { get; set; } = 1;

        [Required, StringLength(120)]
        public string DisplayNameEn { get; set; } = "Ceramica ElQaser";

        [Required, StringLength(120)]
        public string DisplayNameAr { get; set; } = "سيراميكا القصر";

        [Required, StringLength(160)]
        public string BusinessNameEn { get; set; } = "Ceramica Cleopatra";

        [Required, StringLength(160)]
        public string BusinessNameAr { get; set; } = "سيراميك كليوباترا";

        [StringLength(300)]
        public string? SubtitleEn { get; set; } = "Invoice to Ceramic trade";

        [StringLength(300)]
        public string? SubtitleAr { get; set; } = "فاتورة إلى تجار السيراميك";

        [StringLength(1000)]
        public string? AddressEn { get; set; } = "Eastern Helwan\nRiyadh Street Intersection, Ismail Kamel\nCairo Governorate";

        [StringLength(1000)]
        public string? AddressAr { get; set; } = "حلوان الشرقية\nتقاطع شارع الرياض مع إسماعيل كامل\nمحافظة القاهرة";

        [StringLength(50)]
        public string? Phone { get; set; } = "01001688621";

        [EmailAddress, StringLength(256)]
        public string? Email { get; set; } = "kareemsaeed1@yahoo.com";
    }
}
