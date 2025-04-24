using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class FinalInspectionImageDTO
    {

        public string SerialNumber { get; set; }
  
        public List<FinalImageDto> Images { get; set; }
    }

    public class FinalImageDto
    {
        public byte[] Data { get; set; }

        public int IncomingImageId { get; set; }
    }
}
