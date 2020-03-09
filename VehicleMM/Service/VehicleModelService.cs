using Autofac;
using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private IDataSource dataSource;
        private IMapper mapper;
        public VehicleModelService()
        {
            IContainer container = AutofacContainer.Build();
            dataSource = container.Resolve<DataSource>();
            mapper = AutoMapperHelper.Maps();
        }

        public int Create(VehicleModel entity)
        {
            Repository.VehicleModel vehicleModel = mapper.Map<Repository.VehicleModel>(entity);
            return dataSource.GetConnection().InsertAsync(vehicleModel).Result;
        }

        public int Delete(VehicleModel entity)
        {
            Repository.VehicleModel vehicleModel = mapper.Map<Repository.VehicleModel>(entity);
            return dataSource.GetConnection().DeleteAsync(vehicleModel).Result;
        }

        public List<VehicleModel> GetAll()
        {
            List<Repository.VehicleModel> vehicleModelsDB = dataSource.GetConnection().Table<Repository.VehicleModel>().ToListAsync().Result;
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            foreach (Repository.VehicleModel item in vehicleModelsDB)
            {
                vehicleModels.Add(mapper.Map<VehicleModel>(item));
            }
            return vehicleModels;
        }

        public VehicleModel GetById(int id)
        {
            Repository.VehicleModel vehicleModel = dataSource.GetConnection().FindAsync<Repository.VehicleModel>(x => x.Id == id).Result;
            return mapper.Map<VehicleModel>(vehicleModel);
        }

        public List<VehicleModel> GetVehicleModelsByMakeId(int makeId)
        {
            List<Repository.VehicleModel> vehicleModelsDB = dataSource.GetConnection().Table<Repository.VehicleModel>().Where(x=>x.MakeId==makeId).ToListAsync().Result;
            List<VehicleModel> vehicleModels = new List<VehicleModel>();
            foreach (Repository.VehicleModel item in vehicleModelsDB)
            {
                vehicleModels.Add(mapper.Map<VehicleModel>(item));
            }
            return vehicleModels;
        }

        public int Update(VehicleModel entity)
        {
            Repository.VehicleModel vehicleModle = mapper.Map<Repository.VehicleModel>(entity);
            return dataSource.GetConnection().UpdateAsync(vehicleModle).Result;
        }
    }
}
