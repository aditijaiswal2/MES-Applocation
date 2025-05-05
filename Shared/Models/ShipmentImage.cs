using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MES.Shared.Models
{
    public class ShipmentImage : BaseEntity
    {
        public string? SerialNumber { get; set; }
        public required List<Image> Images { get; set; }
    }

    public class Image
    {
        public int ID { get; set; }
        public byte[] Data { get; set; }
        public string ImageFilePath { get; set; }

        public int ShipmentImageId { get; set; }
        [JsonIgnore]
        public ShipmentImage ShipmentImage { get; set; }
    }
}
