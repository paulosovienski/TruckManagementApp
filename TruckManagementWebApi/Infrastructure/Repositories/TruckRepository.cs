using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TruckManagement.Domain;
using TruckManagement.Domain.Entities;
using TruckManagement.Domain.Interfaces;

namespace TruckManagement.Infrastructure.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly TruckManagementDbContext _context;
        public TruckRepository(TruckManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IResult<Truck>> GetTruck(int id)
        {
            try
            {
                if (id == null)
                {
                    return Result<Truck>.Failed("Id wrong");
                }

                var entity = await _context.Trucks.FindAsync(id);

                if (entity == null)
                {
                    return Result<Truck>.Failed("Not Found");
                }

                return Result<Truck>.Successful(entity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IResult<IEnumerable<Truck>>> GetTrucks()
        {
            try
            {
                var allRecords = _context.Trucks.ToList().OrderByDescending(x => x.Id);
                return Result<IEnumerable<Truck>>.Successful(allRecords);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IResult<Truck>> PostTruck(Truck truck)
        {
            try
            {
                var truckExists = await _context.Trucks.FindAsync(truck.Id);
                if (truckExists != null)
                {
                    _context.Trucks.Entry(truckExists).State = EntityState.Detached;
                    _context.Trucks.Update(truck);
                    await _context.SaveChangesAsync();
                    return Result<Truck>.Successful(truck);
                }
                               
                _context.Trucks.Add(truck);
                _context.SaveChanges();
                return Result<Truck>.Successful(truck);
            }
            catch (Exception ex)
            {
                return Result<Truck>.Failed(ex.Message);               
            }         
        }

        public async Task<IResult<Truck>> PatchTruck(Truck truck)
        {
            try
            {
                var truckExists = await _context.Trucks.FindAsync(truck.Id);
                if (truckExists != null)
                {
                    _context.Trucks.Entry(truckExists).State = EntityState.Detached;
                    _context.Trucks.Update(truck);
                    await _context.SaveChangesAsync();
                    return Result<Truck>.Successful(truck);
                }
                else
                {
                    return Result<Truck>.Failed("Truck doesn't exist");
                }                
            }
            catch (Exception ex)
            {
                return Result<Truck>.Failed(ex.Message);
            }
        }

        public async Task<IResult<Truck>> PutTruck(Truck truck)
        {
            try
            {
                var truckExists = await _context.Trucks.FindAsync(truck.Id);
                if (truckExists != null)
                {
                    _context.Trucks.Entry(truckExists).State = EntityState.Detached;
                    _context.Trucks.Update(truck);
                    await _context.SaveChangesAsync();
                    return Result<Truck>.Successful(truck);
                }
                else
                {
                    return Result<Truck>.Failed("Truck doesn't exist");
                }
            }
            catch (Exception ex)
            {
                return Result<Truck>.Failed(ex.Message);
            }
        }

        public async Task<IResult<Truck>> DeleteTruck(int id)
        {
            try
            {
                var truckExist = await _context.Trucks.FindAsync(id);
                if (truckExist == null)
                {
                    return Result<Truck>.Failed("Record don't exist");
                }

                _context.Remove(truckExist);
                await _context.SaveChangesAsync();
                var truck = new Truck()
                {
                    Id = id
                };

                return Result<Truck>.Successful(truck);
            }
            catch (Exception ex)
            {
                return Result<Truck>.Failed(ex.Message);
            }
           
        }


    }
}
