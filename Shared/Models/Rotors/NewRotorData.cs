using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class NewRotorData : BaseEntity
    {
        public string CustomerName { get; set; }
        public string WorkOrderNumber { get; set; }
        public string Workcenters { get; set; } 
    }
}
