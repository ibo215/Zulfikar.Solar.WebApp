namespace Zulfikar.Solar.API.DTOs.ProductDTO
{
    public class PreviewProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // لإظهار الوصف في قائمة المنتجات
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; } // لعرض اسم التصنيف

        // الخصائص الجديدة للعرض
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? LastModified { get; set; }
    }
}