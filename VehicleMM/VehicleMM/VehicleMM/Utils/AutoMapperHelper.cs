using AutoMapper;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMM.Model;

namespace VehicleMM.Utils
{
    public class AutoMapperHelper
    {
        internal static IMapper Maps()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                conf => conf.CreateMap<VehicleMake,VehicleMakeModel>().ReverseMap());
            return mapperConfiguration.CreateMapper();
        }
    }
}
