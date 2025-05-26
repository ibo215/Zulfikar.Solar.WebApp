using AutoMapper;
using Zulfikar.API.DTOs; // لـ PreviewCategoryDto
using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.Solar.API.DTOs.ProductDTO;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Category Mappings
            CreateMap<Category, PreviewCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();

            // Product Mappings
            CreateMap<Product, PreviewProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); // إضافة ربط لاسم التصنيف
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, UpdateProductDto>();
        }
    }
}