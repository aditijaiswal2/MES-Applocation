using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class IncomingInspectionFeedRolls
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string? ReceivedWithEccentrics { get; set; }
        public string? FeedRollDesc { get; set; }
        public string FeedRollSerialNum { get; set; }
        public string? Type { get; set; }
        public string? SMBR { get; set; }
        public string? SMBC { get; set; }
        public string SMBL { get; set; }
        public string? SMFR { get; set; }
        public string? SMFC { get; set; }
        public string? SMFL { get; set; }
        public string? BJL { get; set; }
        public string? BJR { get; set; }
        public string SJL { get; set; }
        public string? SJR { get; set; }
        public string? OL { get; set; }
        public string? BL { get; set; }
        public string? LocknutThreadsL { get; set; }
        public string LocknutThreadsR { get; set; }
        public string? BackPlatesL { get; set; }
        public string? BackPlatesR { get; set; }
        public string? CentersL { get; set; }
        public string? CentersR { get; set; }
        public int? BearingPartNUmber { get; set; }
        public string? Notes { get; set; }
        public string InspectedBY { get; set; }
        public string SerialNumber { get; set; }
        public string Module { get; set; }
        public DateTime Date { get; set; }
    }
}
