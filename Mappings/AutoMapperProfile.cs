using AutoMapper;
using Zulfikar.API.DTOs;
using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.Solar.API.DTOs.ProductDTO;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.API.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Product Mappings
        CreateMap<Product, PreviewProductDto>()
            .ForMember(
            dest => dest.CategoryName, 
            opt => opt.MapFrom(
                src => src.Category != null ? src.Category.Name : null
                )
            )
            .ReverseMap(); 

        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, UpdateProductDto>();

        // Category Mappings 
        CreateMap<Category, PreviewCategoryDto>()
            .ReverseMap(); 

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Category, UpdateCategoryDto>();
    }

}
