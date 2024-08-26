using System.Diagnostics.CodeAnalysis;

namespace SponsorAPI.Data
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string SponsorName { get; set; }
        [AllowNull]
        public string IndustryType { get; set; }
        [AllowNull]
        public string ContactEmail { get; set; }
        [AllowNull]
        public string PhoneNumber {  get; set; }
        
        [AllowNull]
        public decimal TotalPayment { get; set; }
        [AllowNull]
        public int NumberOfPayment { get; set; }

        [AllowNull]
        public int MatchID { get; set; }
        [AllowNull]
        public string MatchName { get; set; }
        [AllowNull]
        public string Date {  get; set; }
        [AllowNull]
        public string Location { get; set; }


        public int PaymentID { get; set; }
        public int ContractID { get; set; }
        public string PaymentDate { get; set; }

        public decimal AmountPaid { get; set; }
        public string PaymentStatus { get; set; }

    }
}
