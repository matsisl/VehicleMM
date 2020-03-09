using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using SQLite;

namespace Repository
{
    public class DataSource : IDataSource
    {
        private SQLiteAsyncConnection connection;

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
            connection.CreateTableAsync<VehicleMake>();
            connection.CreateTableAsync<VehicleModel>();
        }
        private void createMockDataInDB()
        {
            if (connection.Table<VehicleMake>().CountAsync().Result == 0)
            {
                connection.InsertAsync(new VehicleMake(){ Name="Volkswagen", Abrv="VW" });
                connection.InsertAsync(new VehicleMake() { Name="Opel", Abrv="Opel" });
                connection.InsertAsync(new VehicleMake() { Name ="Bayerische Motoren Werke", Abrv="BMW" });
            }
            if (connection.Table<VehicleModel>().CountAsync().Result == 0)
            {
                connection.InsertAsync(new VehicleModel() {MakeId=1, Name="Bettle 2.0 Cabriolet", Abrv="Bettle" });
                connection.InsertAsync(new VehicleModel() { MakeId = 1, Name = "Passat 2.0 Sedan", Abrv = "Passat" });
                connection.InsertAsync(new VehicleModel() { MakeId = 1, Name = "Tiguan S", Abrv = "Tiguan" });
                connection.InsertAsync(new VehicleModel() { MakeId = 2, Name = "Astra 2.0 TurboSport", Abrv = "Astra" });
                connection.InsertAsync(new VehicleModel() { MakeId = 2, Name = "Corsa LiteSport", Abrv = "Corsa" });
                connection.InsertAsync(new VehicleModel() { MakeId = 2, Name = "Insignia 2.0 CDTi AWD", Abrv = "Insignia" });
                connection.InsertAsync(new VehicleModel() { MakeId = 3, Name = "330d", Abrv = "330" });
                connection.InsertAsync(new VehicleModel() { MakeId = 3, Name = "320i 4dr Sedan", Abrv = "320" });
                connection.InsertAsync(new VehicleModel() { MakeId = 3, Name = "X5 sDrive35i 4dr SUV ", Abrv = "X5" });

            }
        }

    }
}
