using AutoMapper;
using Dummy.Core.Model.Classes;
using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //TODO:Configuration

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

        }
    }
}
