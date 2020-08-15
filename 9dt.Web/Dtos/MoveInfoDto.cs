using _9dt.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Dtos
{
    public class MoveInfoDto
    {
        public MoveType Type { get; set; }
        public string Player { get; set; }
        public int Column { get; set; }
    }
}
