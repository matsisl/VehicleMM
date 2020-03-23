using AutoMapper;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMM.Model;

namespace VehicleMM.Utils
{
    public class AutoMapperHelper
    {
        IMapper mapper;
        static AutoMapperHelper Instance;
        private AutoMapperHelper()
        {
            CreteMaps();
        }

        public static AutoMapperHelper GetInsance()
        {
            if (Instance == null)
            {
                Instance = new AutoMapperHelper();
            }
            return Instance;
        }
        private void CreteMaps()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                conf => {
                    conf.AddProfile(new ServiceAutoMapperProfile());
                    conf.AddProfile(new AutoMapperProfile());
                });
            mapper = mapperConfiguration.CreateMapper();
        }

        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
