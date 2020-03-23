using Autofac;
using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VehicleMakeService : IService<VehicleMake>
    {
        IDataSource dataSource;
        IMapper mapper;

        public VehicleMakeService(IDataSource ds)
        {
            dataSource = ds;
            mapper = AutoMapperHelper.Maps();
        }

        public async Task<int> Add(VehicleMake entity)
        {
            try
            {
                VehicleMakeEntity vehicleMake = mapper.Map<VehicleMakeEntity>(entity);
                int added = await dataSource.GetConnection().InsertAsync(vehicleMake);
                return added;
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<int> Delete(VehicleMake entity)
        {
            try
            {
                int provjera = 0;
                VehicleMakeEntity vehicleMake = mapper.Map<VehicleMakeEntity>(entity);
                provjera = await dataSource.GetConnection().Table<VehicleModelEntity>().DeleteAsync(x => x.MakeId == vehicleMake.Id);
                int deleted = await dataSource.GetConnection().DeleteAsync(vehicleMake);
                return deleted;
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleMake>> Filter(string filter)
        {
            try
            {
                List<VehicleMakeEntity> vehicleMakes = await dataSource.GetConnection().Table<VehicleMakeEntity>().Where(
                    x => x.Name.Contains(filter) || x.Abrv.Contains(filter)).ToListAsync();
                List<VehicleMake> makes = new List<VehicleMake>();
                foreach (VehicleMakeEntity item in vehicleMakes)
                {
                    makes.Add(mapper.Map<VehicleMake>(item));
                }
                return makes;
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleMake>> GetAll()
        {
            try
            {
                List<VehicleMakeEntity> vehicleMakesFromDB = await dataSource.GetConnection().Table<Repository.VehicleMakeEntity>().ToListAsync();
                List<VehicleMake> vehicleMakes = new List<VehicleMake>();
                foreach (VehicleMakeEntity item in vehicleMakesFromDB)
                {
                    vehicleMakes.Add(mapper.Map<VehicleMake>(item));
                }
                return vehicleMakes;
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<VehicleMake> GetById(int id)
        {
            try
            {
                VehicleMakeEntity vehicleMakeFromDB = await dataSource.GetConnection().FindAsync<VehicleMakeEntity>(x => x.Id == id);
                return mapper.Map<VehicleMake>(vehicleMakeFromDB);
            }
            catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleMake>> Paging(int indexOfPage, int pageSize)
        {
            try
            {
                if (indexOfPage < 0 || pageSize <= 0 || indexOfPage > pageSize)
                {
                    return null;
                }
                else
                {
                    string sqlQuery = "SELECT * FROM VehiclMake LIMIT ? OFFSET ?";
                    int limit = indexOfPage * pageSize;
                    object[] param = { limit, pageSize };
                    List<VehicleMakeEntity> vehicleMakes = await dataSource.GetConnection().QueryAsync<VehicleMakeEntity>(sqlQuery, param);
                    List<VehicleMake> makes = new List<VehicleMake>();
                    foreach (VehicleMakeEntity item in vehicleMakes)
                    {
                        makes.Add(mapper.Map<VehicleMake>(item));
                    }
                    return makes;
                }
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleMake>> SortAsc()
        {
            try
            {
                List<VehicleMakeEntity> vehicleMakes = await dataSource.GetConnection().Table<Repository.VehicleMakeEntity>().OrderBy(x => x.Name).ToListAsync();
                List<VehicleMake> makes = new List<VehicleMake>();
                foreach (VehicleMakeEntity item in vehicleMakes)
                {
                    makes.Add(mapper.Map<VehicleMake>(item));
                }
                return makes;
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<List<VehicleMake>> SortDesc()
        {
            try
            {
                List<VehicleMakeEntity> vehicleMakes = await dataSource.GetConnection().Table<VehicleMakeEntity>().OrderByDescending(x => x.Name).ToListAsync();
                List<VehicleMake> makes = new List<VehicleMake>();
                foreach (VehicleMakeEntity item in vehicleMakes)
                {
                    makes.Add(mapper.Map<VehicleMake>(item));
                }
                return makes;
             }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }

        public async Task<int> Update(VehicleMake entity)
        {
            try
            {
                VehicleMakeEntity vehicleMake = mapper.Map<VehicleMakeEntity>(entity);
                return await dataSource.GetConnection().UpdateAsync(vehicleMake);
            }catch(Exception ex)
            {
                throw new ServiceException();
            }
        }
    }
}
