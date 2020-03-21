using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    [Table("VehicleMake")]
    public class VehicleMakeEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
