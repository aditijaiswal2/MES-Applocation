using MES.Shared.Models.Rotors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class SalesAttachedFileDto
    {
        public string? SerialNumber { get; set; }
        public List<FiledataDto> File { get; set; } 
    }

    public class FiledataDto
    {
        public byte[] Data { get; set; }
        public int SalesAttachedFileId { get; set; }
    }
}
