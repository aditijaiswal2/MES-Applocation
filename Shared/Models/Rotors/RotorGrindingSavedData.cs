using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class RotorGrindingSavedData : BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Module { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? WorkOrder { get; set; }
        public string? MatNumber { get; set; }
        public string Customer { get; set; }       
        public string RotorsNumber { get; set; }     
        public string RotorCategorization { get; set; }
        public string ComponentType { get; set; }       
        public DateTime? TargetDate { get; set; }       
        public string CustomerImportance { get; set; }      
        public string? AdvancedSharpingStatus { get; set; }
        public string Workcenters { get; set; }   
        public DateTime? GrindingStartDate { get; set; }
        public bool IsStarted { get; set; }
        public bool IsSecondaryWorkCenters { get; set; }
        public string SecondaryWorkCenters { get; set; }
        public string GrindingdataSecondaryWorkCentersSubmiteddBy { get; set; }
        public string GrindingdataSecondaryWorkCentersSubmitedByDate { get; set; }
        public string GrindingdataSavedBy { get; set; }
        public string GrindingdataSavedByDate { get; set; }
    }
}
