using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    [Table("VehicleModel")]
    public class VehicleModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
