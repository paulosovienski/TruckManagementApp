using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManagement.Domain.Entities;
using TruckManagement.Domain.Interfaces;
using System.Linq;
using System.Globalization;

namespace TruckManagement.Domain.Services
{
    public class TruckService : ITruckService
    {
        protected readonly ITruckRepository _repository;
        public TruckService(ITruckRepository repository)
        {
            _repository = repository;                
        }

        public async Task<IResult<Truck>> SaveTruck(Truck truck)
        {
            var errorMessage = ValidateTruck(truck);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    truck = UppercaseTruck(truck);
                    await _repository.PostTruck(truck);
                    return Result<Truck>.Successful(truck);
                }
                catch (Exception ex)
                {
                    return Result<Truck>.Failed(ex.Message);
                }
            }
            else
            {
                return Result<Truck>.Failed(errorMessage);
            }            
        }

        public async Task<IResult<Truck>> PatchTruck(Truck truck)
        {
            var errorMessage = ValidateTruck(truck);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    truck = UppercaseTruck(truck);
                    await _repository.PatchTruck(truck);
                    return Result<Truck>.Successful(truck);
                }
                catch (Exception ex)
                {
                    return Result<Truck>.Failed(ex.Message);
                }
            }
            else
            {
                return Result<Truck>.Failed(errorMessage);
            }
        }

        public async Task<IResult<Truck>> PutTruck(Truck truck)
        {
            var errorMessage = ValidateTruck(truck);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    truck = UppercaseTruck(truck);
                    await _repository.PutTruck(truck);
                    return Result<Truck>.Successful(truck);
                }
                catch (Exception ex)
                {
                    return Result<Truck>.Failed(ex.Message);
                }
            }
            else
            {
                return Result<Truck>.Failed(errorMessage);
            }
        }

        private string ValidateTruck(Truck truck)
        {
            string message = $"";
            if (truck == null)
            {
                message += $"truck null!{Environment.NewLine}";
            }

            var siteexist = Sites.ListSites().Where(x => x.Site.ToUpper() == truck.TruckManufacturingPlant.ToUpper().Trim()).FirstOrDefault();
            if (siteexist == null)
            {
                message += $"Site wrong!{Environment.NewLine}";
            }

            var modelexist = TruckModels.ListModel().Where(x => x.Model.ToUpper().Trim() == truck.TruckModel.ToUpper().Trim()).FirstOrDefault();
            if (modelexist == null)
            {
                message += $"Model wrong!{ Environment.NewLine}";
            }

            var dateValid = DateTime.TryParse(truck.TruckManufacturingDate.ToString("dd/MM/yyyy"), out DateTime dt);
            if (!dateValid)
            {
                message += $"TruckManufacturingDate wrong!{Environment.NewLine}";
            }

            if (string.IsNullOrEmpty(truck.TruckChassis))
            {
                message += $"TruckChassis empty!{Environment.NewLine}";
            }

            if (string.IsNullOrEmpty(truck.TruckColor))
            {
                message += $"TruckColor empty!{Environment.NewLine}";
            }

            return message;
        }

        private Truck UppercaseTruck(Truck truck)
        {
            truck.TruckManufacturingPlant = truck.TruckManufacturingPlant.ToUpper();
            truck.TruckChassis = truck.TruckChassis.ToUpper();
            truck.TruckColor = truck.TruckColor.ToUpper();
            truck.TruckModel = truck.TruckModel.ToUpper();
            return truck;
        }
    }
}
