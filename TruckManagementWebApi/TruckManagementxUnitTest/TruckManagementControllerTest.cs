using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using TruckManagement.Application.Controllers;
using TruckManagement.Domain;
using TruckManagement.Domain.Entities;
using TruckManagement.Domain.Interfaces;
using TruckManagement.Domain.Services;
using TruckManagement.Infrastructure;
using TruckManagement.Infrastructure.Repositories;

namespace TruckManagementxUnitTest
{
    public class TruckManagementControllerTest
    {
        TrucksController _trucksController;
        Truck truck;
        Result<Truck> resultTruck;
        List<Truck> trucks;
        Result<IEnumerable<Truck>> resultTrucks;

        public TruckManagementControllerTest()
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
        public async void GetTrucks()
        {
            //Arrange
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.GetTrucks()).ReturnsAsync(resultTrucks);

            var mockService = new Mock<ITruckService>();
            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.GetTrucks();
            var contentResult = ((OkObjectResult)actionResult.Result);
            var response = (IEnumerable<Truck>) contentResult.Value;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(contentResult.StatusCode, 200);
        }

        [Fact]
        public async void GetTruck()
        {
            //Arrange           
            var mockRepository = new Mock<ITruckRepository>();
            var mockService = new Mock<ITruckService>();
            mockRepository.Setup(x => x.GetTruck(1)).ReturnsAsync(resultTruck);

            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.GetTruck(1);
            var contentResult = ((OkObjectResult)actionResult.Result);
            var response = (Result<Truck>)contentResult.Value;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(contentResult.StatusCode, 200);
        }

        [Fact]
        public async void GetModels()
        {
            //Arrange
            var mockRepository = new Mock<ITruckRepository>();
            var mockService = new Mock<ITruckService>();
            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.GetModels();
            var contentResult = ((OkObjectResult)actionResult.Result);

            //Assert
            Assert.NotNull(actionResult.Result);
            Assert.Equal(contentResult.StatusCode, 200);
        }

        [Fact]
        public async void GetSites()
        {
            //Arrange
            var mockRepository = new Mock<ITruckRepository>();
            var mockService = new Mock<ITruckService>();
            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.GetSites();
            var contentResult = ((OkObjectResult)actionResult.Result);

            //Assert
            Assert.NotNull(actionResult.Result);
            Assert.Equal(contentResult.StatusCode, 200);
        }

        [Fact]
        public async void PostTruck()
        {       
            //Arrange
            var mockRepository = new Mock<ITruckRepository>();
            var mockService = new Mock<ITruckService>();
            mockService.Setup(x => x.SaveTruck(truck)).ReturnsAsync(resultTruck);

            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.PostTruck(truck);
            var contentResult = ((OkObjectResult)actionResult.Result);
            var response = (Result<Truck>)contentResult.Value;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(contentResult.StatusCode, 200);            
        }

        [Fact]
        public async void PatchTruck()
        {
            //Arrange              
            truck.TruckColor = "Changed";

            var mockRepository = new Mock<ITruckRepository>();
            
            var mockService = new Mock<ITruckService>();
            mockService.Setup(x => x.PatchTruck(truck)).ReturnsAsync(resultTruck);

            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.PatchTruck(truck);
            var contentResult = ((OkObjectResult)actionResult.Result);
            var response = (Result<Truck>)contentResult.Value;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(contentResult.StatusCode, 200);
        }

        [Fact]
        public async void PutTruck()
        {              
            truck.TruckChassis = "Changed";
            truck.TruckColor = "Changed";
            truck.TruckManufacturingPlant = "França";
            truck.TruckManufacturingDate = DateTime.Now;
            truck.TruckModel = "VM";

            var mockRepository = new Mock<ITruckRepository>();
            
            var mockService = new Mock<ITruckService>();
            mockService.Setup(x => x.PutTruck(truck)).ReturnsAsync(resultTruck);

            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.PutTruck(truck);
            var contentResult = ((OkObjectResult)actionResult.Result);
            var response = (Result<Truck>)contentResult.Value;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(contentResult.StatusCode, 200);
        }

        [Fact]
        public async void DeleteTruck()
        {
            //Arrange           
            var mockRepository = new Mock<ITruckRepository>();
            mockRepository.Setup(x => x.DeleteTruck(1)).ReturnsAsync(resultTruck);

            var mockService = new Mock<ITruckService>();

            _trucksController = new TrucksController(mockRepository.Object, mockService.Object);

            //Act
            var actionResult = await _trucksController.DeleteTruck(1);
            var contentResult = ((OkObjectResult)actionResult);
            var response = (Result<Truck>)contentResult.Value;

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal(contentResult.StatusCode, 200);
        }       
    }
}