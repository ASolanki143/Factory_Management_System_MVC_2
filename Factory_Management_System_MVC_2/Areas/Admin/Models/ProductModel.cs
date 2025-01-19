namespace Factory_Management_System_MVC_2.Areas.Admin.Models
{
    public class ProductModel
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Material { get; set; }
        public double? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? AdminID { get; set; }
    }
}
