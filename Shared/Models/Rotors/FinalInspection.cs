using MES.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class FinalInspection
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public ICollection<FinalImagedata> Images { get; set; } = new List<FinalImagedata>();

    }

    public class FinalImagedata
    {

        public int ID { get; set; }
        public byte[] Data { get; set; }
        public string ImageFilePath { get; set; }

        public int FinalInspectionId { get; set; }
        [JsonIgnore]
        public FinalInspection? FinalInspection { get; set; }
    }
}

