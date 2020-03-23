using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ServiceAutoMapperProfile : Profile
    {
        public ServiceAutoMapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
        }
    }
}
