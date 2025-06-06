using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class RotorGrindingData : BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Module { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? WorkOrder { get; set; }
        public string? MatNumber { get; set; }
        public string Customer { get; set; }       
        public string RotorsNumber { get; set; }        
        public string? Materials { get; set; }
        public string? RotorsDia { get; set; }
        public DateTime? DateTime { get; set; }
        public string RotorCategorization { get; set; }
        public string ComponentType { get; set; }
        public string Users { get; set; }
        public DateTime? TargetDate { get; set; }
        public string CustomerImportance { get; set; }
        public string? AdvancedSharpingStatus { get; set; }
        public string Workcenters { get; set; }
        public string RotorsDiaLeft { get; set; }
        public string RotorsDiaRight { get; set; }
        public string ReliefLand { get; set; }
        public string ToothFaceLeft { get; set; }
        public string ToothFaceRight { get; set; }
        public string CentersLeft { get; set; }
        public string CentersRight { get; set; }
        public string VisualChecks { get; set; }
        public string InspectedBy { get; set; }
        public DateTime? GrindingStartDate { get; set; }
        public string Notes { get; set; }
        public string DelayReasonTracking { get; set; }
        public string AdditionalSalesComments { get; set; }
        public bool IsMoveoutsideoperation { get; set; }
        public string GrindingdataSubmiteddBy { get; set; }
        public string GrindingdataSubmitedByDate { get; set; }
    }
}
       
