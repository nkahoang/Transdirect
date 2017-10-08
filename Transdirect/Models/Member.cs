namespace Transdirect.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public double DiscountFactor { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CreditLimitAvailable { get; set; }
        public string ChargeType { get; set; }
        public decimal LastInvoiceTimestamp { get; set; }
        public string Term { get; set; }
        public string Active { get; set; }
    }
}