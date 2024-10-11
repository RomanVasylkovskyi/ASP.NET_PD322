using ApiStore.Data.Entities;
using ApiStore.Models.Category;
using ApiStore.Models.Product;
using AutoMapper;

namespace ApiStore.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryCreateViewModel, CategoryEntity>() 
                .ForMember(x=>x.Image, opt=>opt.Ignore());
            CreateMap<CategoryEditViewModel, CategoryEntity>()
                .ForMember(x => x.Image, opt => opt.Ignore());
            CreateMap<CategoryEntity, CategoryItemViewModel>();

            CreateMap<ProductEntity, ProductItemViewModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages
                    .Select(p => p.Image).ToArray()));

            CreateMap<ProductCreateViewModel, ProductEntity>();
        }
    }
}
