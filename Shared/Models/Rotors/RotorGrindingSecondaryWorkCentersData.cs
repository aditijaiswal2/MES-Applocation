using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class RotorGrindingSecondaryWorkCentersData : BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Module { get; set; }        
        public string RotorsNumber { get; set; }
        public string Workcenters { get; set; }
        public DateTime? GrindingStartDate { get; set; }
        public bool IsMoveoutsideoperation { get; set; }
        public bool IsSecondaryWorkCenters { get; set; }
        public string SecondaryWorkCenters { get; set; }
        public string GrindingdataSubmiteddBy { get; set; }
        public string GrindingdataSubmitedByDate { get; set; }
    }
}

