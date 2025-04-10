using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class RotorProcessDimensionsReport : BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Module { get; set; }
        public string RotorsNumber { get; set; }
        public string RotorsDiaLeft { get; set; }
        public string RotorsDiaRight { get; set; }
        public string ReliefLand { get; set; }
        public string ToothFaceLeft { get; set; }
        public string ToothFaceRight { get; set; }
        public string CentersLeft { get; set; }
        public string CentersRight { get; set; }
        public string VisualChecks { get; set; }
        public string InspectedBy { get; set; }
        public DateTime? Date { get; set; }
        public string SavedBy { get; set; }
        public string Notes { get; set; }


    }
}
