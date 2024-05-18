namespace BeSpokedBikesAPI.Model
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SalespersonId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SalesDate { get; set; }
    }
}
