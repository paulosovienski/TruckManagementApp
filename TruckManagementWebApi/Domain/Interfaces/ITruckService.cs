using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManagement.Domain.Entities;

namespace TruckManagement.Domain.Interfaces
{
    public interface ITruckService
    {
        Task<IResult<Truck>> SaveTruck(Truck truck);
        Task<IResult<Truck>> PatchTruck(Truck truck);
        Task<IResult<Truck>> PutTruck(Truck truck);
    }
}
