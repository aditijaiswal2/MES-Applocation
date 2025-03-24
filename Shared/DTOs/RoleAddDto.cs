using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class RoleAddDto
    {
        public string Name { get; set; }
        public string WcList { get; set; }
        public bool ReadOnly { get; set; }
    }
}
