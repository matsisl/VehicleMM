using AutoMapper;
using Repository;
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
                conf=> { conf.CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
                    conf.CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
                });
            
            return mapperConfiguration.CreateMapper();
        }
    }
}
