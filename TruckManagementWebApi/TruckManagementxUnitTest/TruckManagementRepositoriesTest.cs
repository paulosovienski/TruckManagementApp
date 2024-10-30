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
using TruckManagement.Domain;

namespace TruckManagementxUnitTest
{
    public class TruckManagementRepositoriesTest
    {
        TruckManagementDbContext _dbContextInMemory;
        TruckRepository _repositoryInMemory;
        Truck truck;

        public TruckManagementRepositoriesTest()
        {
            _dbContextInMemory = SeedDatabaseInMemory();
            _repositoryInMemory = new TruckRepository(_dbContextInMemory);

            truck = new Truck()
            {
                Id = 0,
                TruckModel = "FM",
                TruckChassis = "123456789",
                TruckColor = "CINZA",
                TruckManufacturingDate = DateTime.Today,
                TruckManufacturingPlant = "BRASIL"
            };
        }

        [Fact]
        public async void GetTruck()
        {
            //Act           
            var fakeTrucks = await _repositoryInMemory.GetTruck(1);

            //Assert
            Assert.NotNull(fakeTrucks.Data);
            Assert.Equal(fakeTrucks.Success, true);
            Assert.Equal(fakeTrucks.Data.Id, 1);

            await _dbContextInMemory.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void GetTrucks()
        {
            //Act            
            var fakeTrucks = await _repositoryInMemory.GetTrucks();

            //Assert
            Assert.NotNull(fakeTrucks.Data);
            Assert.Equal(fakeTrucks.Success, true);
            Assert.Equal(fakeTrucks.Data.Count(), 3);

            await _dbContextInMemory.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void PostTruck()
        {
            //Act
            var fakeTruck = await _repositoryInMemory.PostTruck(truck);
            _dbContextInMemory.SaveChanges();

            //Assert
            Assert.NotNull(fakeTruck);
            Assert.Equal(fakeTruck.Success, true);
            Assert.Equal(fakeTruck.Data.Id, 4);

            await _dbContextInMemory.Database.EnsureDeletedAsync();

        }

        [Fact]
        public async void PatchTruck()
        {
            //Arrange
            truck.Id = 1;
            truck.TruckColor = "Changed";

            //Act
            var fakeTruck = await _repositoryInMemory.PatchTruck(truck);
            _dbContextInMemory.SaveChanges();

            //Assert
            Assert.NotNull(fakeTruck);
            Assert.Equal(fakeTruck.Success, true);
            Assert.Equal(fakeTruck.Data.TruckColor, "Changed");

            await _dbContextInMemory.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void PutTruck()
        {
            //Arrange
            truck.Id = 1;
            truck.TruckManufacturingPlant = "França";
            truck.TruckManufacturingDate = DateTime.Now;
            truck.TruckColor = "Changed";
            truck.TruckChassis = "Changed";
            truck.TruckModel = "VM";

            //Act
            var fakeTruck = await _repositoryInMemory.PutTruck(truck);
            _dbContextInMemory.SaveChanges();

            //Assert
            Assert.NotNull(fakeTruck);
            Assert.Equal(fakeTruck.Success, true);
            Assert.Equal(fakeTruck.Data.TruckManufacturingPlant, "França");
            Assert.Equal(fakeTruck.Data.TruckColor, "Changed");
            Assert.Equal(fakeTruck.Data.TruckChassis, "Changed");
            Assert.Equal(fakeTruck.Data.TruckModel, "VM");

            await _dbContextInMemory.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void DeleteTruck()
        {
            //Arrange
            
            //Act
            var fakeTruck = await _repositoryInMemory.DeleteTruck(1);
            _dbContextInMemory.SaveChanges();

            //Assert
            Assert.NotNull(fakeTruck);
            Assert.Equal(fakeTruck.Success, true);
            Assert.Equal(fakeTruck.Data.Id, 1);

            await _dbContextInMemory.Database.EnsureDeletedAsync();
        }
        private TruckManagementDbContext SeedDatabaseInMemory()
        {
            var options = new DbContextOptionsBuilder<TruckManagementDbContext>()
                    .UseInMemoryDatabase(databaseName: "TruckListDatabase")
                    .Options;

            var _context = new TruckManagementDbContext(options);
            _context.Trucks.Add(new Truck { Id = 1, TruckChassis = "123456789", TruckColor = "AZUL", TruckManufacturingDate = DateTime.Today, TruckManufacturingPlant = "Brasil", TruckModel = "FM" });
            _context.Trucks.Add(new Truck { Id = 2, TruckChassis = "234567891", TruckColor = "VERDE", TruckManufacturingDate = DateTime.Today, TruckManufacturingPlant = "Brasil", TruckModel = "FH" });
            _context.Trucks.Add(new Truck { Id = 3, TruckChassis = "323456789", TruckColor = "CINZA", TruckManufacturingDate = DateTime.Today, TruckManufacturingPlant = "Brasil", TruckModel = "VM" });
            _context.SaveChanges();

            return _context;
        }
    }
}
