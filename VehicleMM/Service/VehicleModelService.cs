using Autofac;
using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VehicleModelService : IVehicleModelService
    {
        IDataSource dataSource;
        IMapper mapper;
        public VehicleModelService(IDataSource ds, IMapper m)
        {
            dataSource = ds;
            mapper = m;
        }

        public async Task<int> Add(VehicleModel entity)
        {
            try
            {
                VehicleModelEntity vehicleModel = mapper.Map<VehicleModelEntity>(entity);
                return await dataSource.GetConnection().InsertAsync(vehicleModel);
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<int> Delete(VehicleModel entity)
        {
            try
            {
                VehicleModelEntity vehicleModel = mapper.Map<VehicleModelEntity>(entity);
                return await dataSource.GetConnection().Table<VehicleModelEntity>().DeleteAsync(x => x.Id == vehicleModel.Id && x.MakeId == vehicleModel.MakeId);
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> Filter(string filter)
        {
            try
            {
                List<VehicleModelEntity> vehicleModles = await dataSource.GetConnection().Table<VehicleModelEntity>().Where(
                    x => x.Name.Contains(filter) || x.Abrv.Contains(filter)).ToListAsync();
                List<VehicleModel> models = new List<VehicleModel>();
                foreach (VehicleModelEntity item in vehicleModles)
                {
                    models.Add(mapper.Map<VehicleModel>(item));
                }
                return models;
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> GetAll()
        {
            try
            {
                List<VehicleModelEntity> vehicleModelsDB = await dataSource.GetConnection().Table<VehicleModelEntity>().ToListAsync();
                List<VehicleModel> vehicleModels = new List<VehicleModel>();
                foreach (VehicleModelEntity item in vehicleModelsDB)
                {
                    vehicleModels.Add(mapper.Map<VehicleModel>(item));
                }
                return vehicleModels;
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<VehicleModel> GetById(int id)
        {
            try
            {
                VehicleModelEntity vehicleModel = await dataSource.GetConnection().FindAsync<VehicleModelEntity>(x => x.Id == id);
                return mapper.Map<VehicleModel>(vehicleModel);
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> GetVehicleModelsByMakeId(int makeId)
        {
            try
            {
                List<VehicleModelEntity> vehicleModelsDB = await dataSource.GetConnection().Table<VehicleModelEntity>().Where(x => x.MakeId == makeId).ToListAsync();
                List<VehicleModel> vehicleModels = new List<VehicleModel>();
                foreach (VehicleModelEntity item in vehicleModelsDB)
                {
                    vehicleModels.Add(mapper.Map<VehicleModel>(item));
                }
                return vehicleModels;
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> Paging(int indexOfPage, int pageSize)
        {
            try
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
                    List<VehicleModelEntity> vehicleModels = await dataSource.GetConnection().QueryAsync<VehicleModelEntity>(sqlQuery, param);
                    List<VehicleModel> models = new List<VehicleModel>();
                    foreach (VehicleModelEntity item in vehicleModels)
                    {
                        models.Add(mapper.Map<VehicleModel>(item));
                    }
                    return models;
                }
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> PagingByMake(int makeId, int indexOfPage, int pageSize)
        {
            try
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
                    List<VehicleModelEntity> vehicleModels = await dataSource.GetConnection().QueryAsync<VehicleModelEntity>(sqlQuery, param);
                    List<VehicleModel> models = new List<VehicleModel>();
                    foreach (VehicleModelEntity item in vehicleModels)
                    {
                        models.Add(mapper.Map<VehicleModel>(item));
                    }
                    return models;
                }
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> SortAsc()
        {
            try
            {
                List<VehicleModelEntity> vehicleModels = await dataSource.GetConnection().Table<VehicleModelEntity>().OrderBy(x => x.Name).ToListAsync();
                List<VehicleModel> models = new List<VehicleModel>();
                foreach (VehicleModelEntity item in vehicleModels)
                {
                    models.Add(mapper.Map<VehicleModel>(item));
                }
                return models;
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleModel>> SortDesc()
        {
            try
            {
                List<VehicleModelEntity> vehicleModels = await dataSource.GetConnection().Table<VehicleModelEntity>().OrderByDescending(x => x.Name).ToListAsync();
                List<VehicleModel> models = new List<VehicleModel>();
                foreach (VehicleModelEntity item in vehicleModels)
                {
                    models.Add(mapper.Map<VehicleModel>(item));
                }
                return models;
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }

        }
        public async Task<int> Update(VehicleModel entity)
        {
            try
            {
                VehicleModelEntity vehicleModle = mapper.Map<VehicleModelEntity>(entity);
                return await dataSource.GetConnection().UpdateAsync(vehicleModle);
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }
    }
}
