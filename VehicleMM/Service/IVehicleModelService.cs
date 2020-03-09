using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IVehicleModelService :IService<VehicleModel>
    {
        List<VehicleModel> GetVehicleModelsByMakeId(int makeId);
    }
}
