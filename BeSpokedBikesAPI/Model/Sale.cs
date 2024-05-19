namespace BeSpokedBikesAPI.Model
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int SalespersonId { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Commission { get; set; }
    }

    public class SaleDetailsDto
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string CustomerName { get; set; }
        public string SalespersonName { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal SalespersonCommission { get; set; }

    }

}
