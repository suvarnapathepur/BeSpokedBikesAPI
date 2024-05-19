namespace BeSpokedBikesAPI.Model
{
    public class QuarterlyCommissionReport
    {
        public long SalespersonId { get; set; }
        public string Salesperson { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal TotalCommission { get; set; }
    }

}
