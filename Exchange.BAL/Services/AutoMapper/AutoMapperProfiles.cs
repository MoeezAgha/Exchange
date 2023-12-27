using AutoMapper;

using Exchange.DAL.Models;
using Exchange.Library.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.BAL.Services.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            //CreateMap<Store, StoreViewModel>()
            //          .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            //          .ForMember(dest => dest.Categories, opt => opt.Ignore()) // Ignore mapping for Categories
            //          .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags)).ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            //CreateMap<Tag, TagViewModel>()

            //           .ReverseMap(); // Optionally enable reverse mapping
            //CreateMap<Category, CategoryViewModel>()

            //          .ReverseMap(); // Optionally enable reverse mapping
            //CreateMap<Product, ProductViewModel>()

            //          .ReverseMap(); // Optionally enable reverse mapping
            //CreateMap<ProductImage, ProductImageViewModel>()

            //          .ReverseMap(); // Optionally enable reverse mapping

        }

    }
}
