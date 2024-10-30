using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckManagement.Domain;
using TruckManagement.Domain.Entities;
using TruckManagement.Domain.Interfaces;
using TruckManagement.Infrastructure;

namespace TruckManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckRepository _repository;
        private readonly ITruckService _service;

     

        public TrucksController(ITruckRepository repository, ITruckService service)
        {
            _repository = repository;
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Truck>>> GetTrucks()
        {
            var result = await _repository.GetTrucks();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("GetModels")]
        public async Task<ActionResult<IEnumerable<TruckModels>>> GetModels()
        {
            return Ok(TruckModels.ListModel());
        }

        [HttpGet("GetSites")]
        public async Task<ActionResult<IEnumerable<Sites>>> GetSites()
        {
            return Ok(Sites.ListSites());
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(int id)
        {
            var result = await _repository.GetTruck(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Truck>> PostTruck(Truck truck)
        {
            try
            {
                var result = await _service.SaveTruck(truck);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
       
        [HttpPatch]
        public async Task<ActionResult<Truck>> PatchTruck(Truck truck)
        {
            try
            {
                var result = await _service.PatchTruck(truck);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<Truck>> PutTruck(Truck truck)
        {
            try
            {
                var result = await _service.PutTruck(truck);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var result = await _repository.DeleteTruck(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }       
    }
}
