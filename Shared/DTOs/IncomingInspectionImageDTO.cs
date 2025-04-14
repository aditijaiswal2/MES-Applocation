using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class IncomingInspectionImageDTO
    {
        public string SerialNumber { get; set; }
  
        public List<ImageDto> Images { get; set; }
    }

    public class ImageDto
    {
        public byte[] Data { get; set; }

        public int IncomingImageId { get; set; }
    }
}
