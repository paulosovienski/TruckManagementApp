using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManagement.Application.Controllers;
using TruckManagement.Infrastructure.Repositories;
using TruckManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TruckManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TruckManagement.Domain.Interfaces;
using TruckManagement.Domain.Services;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using TruckManagement.Domain;

namespace TruckManagementxUnitTest
{
    public class TruckManagementServicesTest
    {
        TruckService _truckService;
        Truck truck;
        Result<Truck> resultTruck;
        List<Truck> trucks;
        Result<IEnumerable<Truck>> resultTrucks;

        public TruckManagementServicesTest()
        {
            truck = new Truck()
            {
                Id = 1,
                TruckModel = "FM",
                TruckChassis = "123456789",
                TruckColor = "CINZA",
                TruckManufacturingDate = DateTime.Today,
                TruckManufacturingPlant = "BRASIL"
            };
            resultTruck = new Result<Truck>(truck, true);

            trucks = new List<Truck>()
            {
                new Truck { Id = 1, TruckChassis = "123456789", TruckColor = "AZUL", TruckManufacturingDate = DateTime.Today, TruckManufacturingPlant = "Brasil", TruckModel = "FM" },
                new Truck { Id = 2, TruckChassis = "234567891", TruckColor = "VERDE", TruckManufacturingDate = DateTime.Today, TruckManufacturingPlant = "Brasil", TruckModel = "FH" },
                new Truck { Id = 3, TruckChassis = "323456789", TruckColor = "CINZA", TruckManufacturingDate = DateTime.Today, TruckManufacturingPlant = "Brasil", TruckModel = "VM" }
            };
            resultTrucks = new Result<IEnumerable<Truck>>(trucks, true);
        }

        [Fact]
        public async void SaveTruck()
        {
            //Arrange           
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.PostTruck(truck)).ReturnsAsync(resultTruck);
            
            _truckService = new TruckService(mockRepository.Object);

            //Act
            var result = await _truckService.SaveTruck(truck);
            //var contentResult = ((OkObjectResult)actionResult.Result);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Id, truck.Id);
        }

        [Fact]
        public async void SaveTruckFailed()
        {
            //Assert
            Truck truck = new Truck()
            {
                Id = 0,
                TruckChassis = "",
                TruckColor = "",
                TruckManufacturingDate = DateTime.Now,
                TruckManufacturingPlant = "",
                TruckModel = ""
            };

            //Arrange           
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.PostTruck(truck)).ReturnsAsync(resultTruck);

            _truckService = new TruckService(mockRepository.Object);

            //Act
            var result = await _truckService.SaveTruck(truck);
            //var contentResult = ((OkObjectResult)actionResult.Result);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Success, false);
        }

        [Fact]
        public async void PatchTruck()
        {
            truck.TruckColor = "Changed";

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.PatchTruck(truck)).ReturnsAsync(resultTruck);

            _truckService = new TruckService(mockRepository.Object);

            //Act
            var result = await _truckService.PatchTruck(truck);
            //var contentResult = ((OkObjectResult)actionResult.Result);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Data.Id, truck.Id);
            Assert.Equal(result.Data.TruckColor, result.Data.TruckColor);
            Assert.True(result.Success);
        }

        [Fact]
        public async void PatchTruckFailed()
        {
            //Arrange           
            truck.TruckColor = "";

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.PatchTruck(truck)).ReturnsAsync(resultTruck);

            _truckService = new TruckService(mockRepository.Object);

            //Act
            var result = await _truckService.PatchTruck(truck);
            //var contentResult = ((OkObjectResult)actionResult.Result);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Success, false);
            
        }

        [Fact]
        public async void PutTruck()
        {
            //Arrange
            truck.TruckChassis = "Changed";
            truck.TruckColor = "Changed";
            truck.TruckManufacturingPlant = "França";
            truck.TruckManufacturingDate = DateTime.Now;
            truck.TruckModel = "VM";

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.PutTruck(truck)).ReturnsAsync(resultTruck);

            _truckService = new TruckService(mockRepository.Object);

            //Act
            var result = await _truckService.PutTruck(truck);
           
            //Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Id, truck.Id);
            Assert.Equal(result.Data.TruckColor, result.Data.TruckColor);
            Assert.Equal(result.Data.TruckChassis, result.Data.TruckChassis);
            Assert.Equal(result.Data.TruckManufacturingPlant, result.Data.TruckManufacturingPlant);
            Assert.Equal(result.Data.TruckModel, result.Data.TruckModel);
        }

        [Fact]
        public async void PutTruckFailed()
        {
            //Arrange
            truck.TruckChassis = "";
            truck.TruckColor = "";
            truck.TruckManufacturingPlant = "";
            truck.TruckManufacturingDate = DateTime.Now;
            truck.TruckModel = "";

            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.PutTruck(truck)).ReturnsAsync(resultTruck);

            _truckService = new TruckService(mockRepository.Object);

            //Act
            var result = await _truckService.PutTruck(truck);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Success,false);
            
        }       
    }
}
