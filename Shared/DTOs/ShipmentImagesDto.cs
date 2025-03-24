﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class ShipmentImagesDto
    {
        public int SerialNumber { get; set; }
        public string Module { get; set; }
        public List<ImageDataDto> Images { get; set; }
    }

    public class ImageDataDto
    {
        public byte[] Data { get; set; }

        public int ITSImageID { get; set; }
    }
}
