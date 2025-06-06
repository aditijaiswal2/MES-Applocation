using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class RotorShipping : BaseEntity
    {
        public string? SerialNumber { get; set; }
        public string? Module { get; set; }
        public string? SalesOrderNumber { get; set; }
        public string? WorkOrder { get; set; }
        public string? MatNumber { get; set; }
        public string? Customer { get; set; }
        public string? Received { get; set; }
        public string? Inspected { get; set; }
        public string? RotorsNumber { get; set; }
        public DateTime? DateTime { get; set; }
        public string? RotorCategorization { get; set; }
        public string? ComponentType { get; set; }
        public DateTime? TargetDate { get; set; }
        public string? CustomerImportance { get; set; }
        public string? AdvancedSharpingStatus { get; set; }
        public string? Workcenters { get; set; }
        public string? AdditionalWSalesComments { get; set; }
        public string? ShipSubmiteddBy { get; set; }
        public DateTime? ShipSubmitedByDate { get; set; }
    }
}