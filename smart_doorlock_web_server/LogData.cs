using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smart_doorlock_web_server
{
    public class LogData
    {
        public DateTime AccessDateTime { get; set; }
        public string Type { get; set; }
        public string AccessID { get; set; }
    }
}
