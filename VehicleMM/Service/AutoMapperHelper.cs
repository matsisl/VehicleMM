using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class AutoMapperHelper
    {
        internal static IMapper Maps()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                conf=> { conf.CreateMap<VehicleMake, Repository.VehicleMake>().ReverseMap();
                    conf.CreateMap<VehicleModel, Repository.VehicleModel>().ReverseMap();
                });
            
            return mapperConfiguration.CreateMapper();
        }
    }
}
