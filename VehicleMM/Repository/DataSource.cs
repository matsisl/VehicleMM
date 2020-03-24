using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SQLite;

namespace Repository
{
    public class DataSource : IDataSource
    {
        SQLiteAsyncConnection connection;

        public DataSource()
        {
            connection = new SQLiteAsyncConnection(getDataBasePath());
            createTableInDB();
            createMockDataInDB();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return connection;
        }

        private string getDataBasePath()
        {
            string dataBaseFileName = " VehicleMM.db";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folderPath,dataBaseFileName);
        }

        private void createTableInDB()
        {
            connection.CreateTableAsync<VehicleMakeEntity>().Wait();
            connection.CreateTableAsync<VehicleModelEntity>().Wait();
        }
        private void createMockDataInDB()
        {
            int idVW=0, idBMW=0, idOpel=0;
            if (connection.Table<VehicleMakeEntity>().CountAsync().Result == 0)
            {
                idVW =  connection.InsertAsync(new VehicleMakeEntity(){ Name="Volkswagen", Abrv="VW" }).Result;
                idOpel = connection.InsertAsync(new VehicleMakeEntity() { Name="Opel", Abrv="Opel" }).Result;
                idBMW =  connection.InsertAsync(new VehicleMakeEntity() { Name ="Bayerische Motoren Werke", Abrv="BMW" }).Result;
            }
            if (connection.Table<VehicleModelEntity>().CountAsync().Result == 0)
            {
                connection.InsertAsync(new VehicleModelEntity() {MakeId=1, Name="Bettle 2.0 Cabriolet", Abrv="Bettle" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 1, Name = "Passat 2.0 Sedan", Abrv = "Passat" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 1, Name = "Tiguan S", Abrv = "Tiguan" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId =2, Name = "Astra 2.0 TurboSport", Abrv = "Astra" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 2, Name = "Corsa LiteSport", Abrv = "Corsa" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 2, Name = "Insignia 2.0 CDTi AWD", Abrv = "Insignia" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 3, Name = "330d", Abrv = "330" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 3, Name = "320i 4dr Sedan", Abrv = "320" });
                connection.InsertAsync(new VehicleModelEntity() { MakeId = 3, Name = "X5 sDrive35i 4dr SUV ", Abrv = "X5" });

            }
        }

    }
}
