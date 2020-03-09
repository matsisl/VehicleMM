using Autofac;
using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class VehicleMakeService : IService<VehicleMake>
    {
        private IDataSource dataSource;
        private IMapper mapper;

        public VehicleMakeService()
        {
            IContainer container = AutofacContainer.Build();
            dataSource = container.Resolve<DataSource>();
            mapper = AutoMapperHelper.Maps();
        }

        public int Create(VehicleMake entity)
        {
            Repository.VehicleMake vehicleMake = mapper.Map<Repository.VehicleMake>(entity);
            return dataSource.GetConnection().InsertAsync(vehicleMake).Result;
        }

        public int Delete(VehicleMake entity)
        {
            Repository.VehicleMake vehicleMake = mapper.Map<Repository.VehicleMake>(entity);
            return dataSource.GetConnection().DeleteAsync(vehicleMake).Result;
        }

        public List<VehicleMake> GetAll()
        {
            List<Repository.VehicleMake> vehicleMakesFromDB = dataSource.GetConnection().Table<Repository.VehicleMake>().ToListAsync().Result;
            List<VehicleMake> vehicleMakes = new List<VehicleMake>();
            foreach (Repository.VehicleMake item in vehicleMakesFromDB)
            {
                vehicleMakes.Add(mapper.Map<VehicleMake>(item));
            }
            return vehicleMakes;
        }

        public VehicleMake GetById(int id)
        {
            Repository.VehicleMake vehicleMakeFromDB = dataSource.GetConnection().FindAsync<Repository.VehicleMake>(x=>x.Id==id).Result;
            return mapper.Map<VehicleMake>(vehicleMakeFromDB);
        }

        public int Update(VehicleMake entity)
        {
            Repository.VehicleMake vehicleMake = mapper.Map<Repository.VehicleMake>(entity);
            return dataSource.GetConnection().UpdateAsync(vehicleMake).Result;
        }
    }
}
