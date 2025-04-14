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
    public class IncomingImages
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
         public required List<Imagedata> Images { get; set; }
    }

    public class Imagedata
    {

        public int ID { get; set; }
        public byte[] Data { get; set; }
        public string ImageFilePath { get; set; }

        public int IncomingImageId { get; set; } 
        [JsonIgnore] 
        public IncomingImages? IncomingImages { get; set; }
    }
}

