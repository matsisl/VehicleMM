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
        IDataSource dataSource;
        IMapper mapper;
        public VehicleModelService()
        {
            IContainer container = AutofacContainer.Build();
            dataSource = container.Resolve<DataSource>();
            mapper = AutoMapperHelper.Maps();
        }

        public int Add(VehicleModel entity)
        {
            Repository.VehicleModel vehicleModel = mapper.Map<Repository.VehicleModel>(entity);
            return dataSource.GetConnection().InsertAsync(vehicleModel).Result;
        }

        public int Delete(VehicleModel entity)
        {
            Repository.VehicleModel vehicleModel = mapper.Map<Repository.VehicleModel>(entity);
            return dataSource.GetConnection().Table<Repository.VehicleModel>().DeleteAsync(x => x.Id == vehicleModel.Id && x.MakeId == vehicleModel.MakeId).Result;
        }

        public List<VehicleModel> Filter(string filter)
        {
            List<Repository.VehicleModel> vehicleModles = dataSource.GetConnection().Table<Repository.VehicleModel>().Where(
                x => x.Name.Contains(filter) || x.Abrv.Contains(filter)).ToListAsync().Result;
            List<VehicleModel> models = new List<VehicleModel>();
            foreach (Repository.VehicleModel item in vehicleModles)
            {
                models.Add(mapper.Map<VehicleModel>(item));
            }
            return models;
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

        public List<VehicleModel> Paging(int indexOfPage, int pageSize)
        {
            if (indexOfPage < 0 || pageSize <= 0 || indexOfPage > pageSize)
            {
                return null;
            }
            else
            {
                string sqlQuery = "SELECT * FROM VehiclModel LIMIT ? OFFSET ?";
                int limit = indexOfPage * pageSize;
                object[] param = { limit, pageSize };
                List<Repository.VehicleModel> vehicleModels = dataSource.GetConnection().QueryAsync<Repository.VehicleModel>(sqlQuery, param).Result;
                List<VehicleModel> models = new List<VehicleModel>();
                foreach (Repository.VehicleModel item in vehicleModels)
                {
                    models.Add(mapper.Map<VehicleModel>(item));
                }
                return models;
            }
        }

        public List<VehicleModel> PagingByMake(int makeId, int indexOfPage, int pageSize)
        {
            if (indexOfPage < 0 || pageSize <= 0)
            {
                return null;
            }
            else
            {
                int offset = indexOfPage * pageSize;
                string sqlQuery = "SELECT * FROM VehicleModel WHERE MakeId=? LIMIT ? OFFSET ?";
                object[] param = { makeId, pageSize, offset };
                List<Repository.VehicleModel> vehicleModels = dataSource.GetConnection().QueryAsync<Repository.VehicleModel>(sqlQuery,param).Result;
                List<VehicleModel> models = new List<VehicleModel>();
                foreach (Repository.VehicleModel item in vehicleModels)
                {
                    models.Add(mapper.Map<VehicleModel>(item));
                }
                return models;
            }
        }

        public List<VehicleModel> SortAsc()
        {
            List<Repository.VehicleModel> vehicleModels = dataSource.GetConnection().Table<Repository.VehicleModel>().OrderBy(x => x.Name).ToListAsync().Result;
            List<VehicleModel> models = new List<VehicleModel>();
            foreach (Repository.VehicleModel item in vehicleModels)
            {
                models.Add(mapper.Map<VehicleModel>(item));
            }
            return models;
        }

        public List<VehicleModel> SortDesc()
        {
            List<Repository.VehicleModel> vehicleModels = dataSource.GetConnection().Table<Repository.VehicleModel>().OrderByDescending(x => x.Name).ToListAsync().Result;
            List<VehicleModel> models = new List<VehicleModel>();
            foreach (Repository.VehicleModel item in vehicleModels)
            {
                models.Add(mapper.Map<VehicleModel>(item));
            }
            return models;

        }
        public int Update(VehicleModel entity)
        {
            Repository.VehicleModel vehicleModle = mapper.Map<Repository.VehicleModel>(entity);
            return dataSource.GetConnection().UpdateAsync(vehicleModle).Result;
        }
    }
}
