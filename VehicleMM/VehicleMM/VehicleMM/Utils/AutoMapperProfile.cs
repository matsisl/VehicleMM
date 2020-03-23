using AutoMapper;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMM.Model;

namespace VehicleMM.Utils
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMakeModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleModelModel, VehicleModel>().ReverseMap();
        }
    }
}
