using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IVehicleModelService :IService<VehicleModel>
    {
        Task<List<VehicleModel>> GetVehicleModelsByMakeId(int makeId);
        Task<List<VehicleModel>> PagingByMake(int makeId, int indexOfPage, int pageSize);
    }
}
