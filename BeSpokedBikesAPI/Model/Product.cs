using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikesAPI.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Style { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public decimal CommissionPercentage { get; set; }
    }
}
