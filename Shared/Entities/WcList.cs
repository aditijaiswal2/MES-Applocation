using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Entities
{
    public class WcList
    {
        public int Id { get; set; }
        public string tla { get; set; }
        public string name { get; set; }
        public int order { get; set; }
    }
}
