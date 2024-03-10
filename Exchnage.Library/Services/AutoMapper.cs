using AutoMapper;
using Exchange.Library.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Library.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {


            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();


        }

    }
}
