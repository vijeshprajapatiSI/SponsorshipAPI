namespace SponsorAPI.Models
{
    public class SponsorPaymentSummary
    {
        public int SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string IndustryType { get; set; }
        public string ContactEmail { get; set; }
        public decimal TotalPayments { get; set; }
        public int NumberOfPayments { get; set; }
        public DateTime? LatestPaymentDate { get; set; }
    }
}
