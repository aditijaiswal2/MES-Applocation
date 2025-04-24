using MES.Shared.Models.Rotors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
   public class RotorFinalInspectionDTO
    {
        public RotorGrindingData InspectionData { get; set; }
        public RotorsFinalInspection FinalInspectionData { get; set; }
        public List<RotorsFinalInspection> TableData { get; set; }
    }
}
