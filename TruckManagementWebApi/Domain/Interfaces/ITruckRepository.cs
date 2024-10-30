using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManagement.Domain.Entities;

namespace TruckManagement.Domain.Interfaces
{
    public interface ITruckRepository
    {
        Task<IResult<Truck>> GetTruck(int id);
        Task<IResult<IEnumerable<Truck>>> GetTrucks();
        Task<IResult<Truck>> PostTruck(Truck truck);
        Task<IResult<Truck>> PatchTruck(Truck truck);
        Task<IResult<Truck>> PutTruck(Truck truck);
        Task<IResult<Truck>> DeleteTruck(int id);

    }
}
