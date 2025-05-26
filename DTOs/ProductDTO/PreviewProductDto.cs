﻿namespace Zulfikar.Solar.API.DTOs.ProductDTO
{
    public class PreviewProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } 
    }
}
