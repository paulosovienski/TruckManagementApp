using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TruckManagement.Domain.Entities
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }
        public string TruckModel { get; set; }
        public DateTime TruckManufacturingDate { get; set; }
        public string TruckChassis { get; set; }
        public string TruckColor { get; set; }
        public string TruckManufacturingPlant { get; set; }

    }   
}
