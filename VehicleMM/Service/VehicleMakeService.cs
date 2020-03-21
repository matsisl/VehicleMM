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
        IDataSource dataSource;
        IMapper mapper;

        public VehicleMakeService()
        {
            IContainer container = AutofacContainer.Build();
            dataSource = container.Resolve<DataSource>();
            mapper = AutoMapperHelper.Maps();
        }

        public int Add(VehicleMake entity)
        {
            Repository.VehicleMake vehicleMake = mapper.Map<Repository.VehicleMake>(entity);
            return dataSource.GetConnection().InsertAsync(vehicleMake).Result;
        }

        public int Delete(VehicleMake entity)
        {
            int provjera = 0;
            Repository.VehicleMake vehicleMake = mapper.Map<Repository.VehicleMake>(entity);
            provjera = dataSource.GetConnection().Table<Repository.VehicleModel>().DeleteAsync(x => x.MakeId == vehicleMake.Id).Result;
            return dataSource.GetConnection().DeleteAsync(vehicleMake).Result;
        }

        public List<VehicleMake> Filter(string filter)
        {
            List<Repository.VehicleMake> vehicleMakes = dataSource.GetConnection().Table<Repository.VehicleMake>().Where(
                x => x.Name.Contains(filter) || x.Abrv.Contains(filter)).ToListAsync().Result;
            List<VehicleMake> makes = new List<VehicleMake>();
            foreach (Repository.VehicleMake item in vehicleMakes)
            {
                makes.Add(mapper.Map<VehicleMake>(item));
            }
            return makes;
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

        public List<VehicleMake> Paging(int indexOfPage, int pageSize)
        {
            if(indexOfPage<0 || pageSize<=0 || indexOfPage > pageSize)
            {
                return null;
            }
            else
            {
                string sqlQuery = "SELECT * FROM VehiclMake LIMIT ? OFFSET ?";
                int limit = indexOfPage * pageSize;
                object[] param = { limit, pageSize };
                List<Repository.VehicleMake> vehicleMakes = dataSource.GetConnection().QueryAsync<Repository.VehicleMake>(sqlQuery, param).Result;
                List<VehicleMake> makes = new List<VehicleMake>();
                foreach (Repository.VehicleMake item in vehicleMakes)
                {
                    makes.Add(mapper.Map<VehicleMake>(item));
                }
                return makes;
            }
        }

        public List<VehicleMake> SortAsc()
        {
            List<Repository.VehicleMake> vehicleMakes = dataSource.GetConnection().Table<Repository.VehicleMake>().OrderBy(x => x.Name).ToListAsync().Result;
            List<VehicleMake> makes = new List<VehicleMake>();
            foreach (Repository.VehicleMake item in vehicleMakes)
            {
                makes.Add(mapper.Map<VehicleMake>(item));
            }
            return makes;
        }

        public List<VehicleMake> SortDesc()
        {
            List<Repository.VehicleMake> vehicleMakes = dataSource.GetConnection().Table<Repository.VehicleMake>().OrderByDescending(x => x.Name).ToListAsync().Result;
            List<VehicleMake> makes = new List<VehicleMake>();
            foreach (Repository.VehicleMake item in vehicleMakes)
            {
                makes.Add(mapper.Map<VehicleMake>(item));
            }
            return makes;
        }

        public int Update(VehicleMake entity)
        {
            Repository.VehicleMake vehicleMake = mapper.Map<Repository.VehicleMake>(entity);
            return dataSource.GetConnection().UpdateAsync(vehicleMake).Result;
        }
    }
}
