using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckManagement.Domain.Entities
{
    public class Sites
    {
        public Sites(string s)
        {
            Site = s;
        }

        [NotMapped]
        public string Site { get; set; }

        //public List<Model> ListModel { get; set; }

        public static IEnumerable<Sites> ListSites()
        {
            List<Sites> listSites = new List<Sites>();
            listSites.Add(new Sites("Brasil"));
            listSites.Add(new Sites("Suécia"));
            listSites.Add(new Sites("Estados Unidos"));
            listSites.Add(new Sites("França"));
            return (IEnumerable<Sites>)listSites;

        }
    }
}
