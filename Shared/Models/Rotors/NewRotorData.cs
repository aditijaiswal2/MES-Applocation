using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class NewRotorData : BaseEntity
    {
        public string SerialNumber { get; set; }
        public string Module { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? WorkOrder { get; set; }
        public string? MaterialNumber { get; set; }
        public string RotorsNumber { get; set; }
        public string ComponentType { get; set; }
        public string CustomerImportance { get; set; }
        public DateTime? TargetDate { get; set; }
        public string? PlannedHours { get; set; }
        public string CustomerName { get; set; }       
        public string Workcenters { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? NewRotorDataSubmitDate { get; set; }
        public string NewRotorDataSSubmitBy { get; set; }
    }
}
