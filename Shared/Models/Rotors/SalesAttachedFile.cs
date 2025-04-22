using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MES.Shared.Models.Rotors
{
    public class SalesAttachedFile : BaseEntity
    {      
        public string? SerialNumber { get; set; }

        //  One salesfile can have many Filedata
        public List<Filedata> File { get; set; } = new();
    }

    public class Filedata
    {
        public int ID { get; set; }

        public byte[] Data { get; set; } 

        public string FilePath { get; set; } 

        // Foreign key reference to salesfile
        public int SalesAttachedFileId { get; set; }

        [JsonIgnore] // Good for avoiding circular references in API responses
        public SalesAttachedFile? SalesAttachedFile { get; set; }
    }
}
