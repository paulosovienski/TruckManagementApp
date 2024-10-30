using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckManagement.Domain.Entities
{
    public class TruckModels
    {
        public TruckModels(string s)
        {
            Model = s;
        }

        [NotMapped]
        public string Model { get; set; }

        //public List<Model> ListModel { get; set; }

        public static IEnumerable<TruckModels> ListModel()
        {
            List<TruckModels> models = new List<TruckModels>();
            models.Add(new TruckModels("FH"));
            models.Add(new TruckModels("FM"));
            models.Add(new TruckModels("VM"));
            return (IEnumerable<TruckModels>)models;

        }
    }
}
