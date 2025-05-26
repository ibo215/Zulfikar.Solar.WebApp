namespace Zulfikar.Solar.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public bool IsActive { get; set; } = true; // لتحديد ما إذا كان المنتج نشطًا ومتاحًا للعرض
        public DateTime DateAdded { get; set; } = DateTime.UtcNow; // تاريخ إضافة المنتج
        public DateTime? LastModified { get; set; } // تاريخ آخر تعديل على المنتج، يمكن أن يكون اختياريًا

        // Navigation Property
        public Category Category { get; set; }
    }
}