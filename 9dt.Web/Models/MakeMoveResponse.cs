using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Models
{
    public class MakeMoveResponse
    {
        public string move { get; set; } // "{gameId}/moves/{move_number}"
        public MakeMoveResponse(string move)
        {
            this.move = move;
        }
        
    }
}
