using _9dt.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Dtos
{
    public class GameStateDto
    {
        public IList<string> Players { get; set; }
        public GameStatus State { get; set; }
        public string Winner { get; set; }
    }
}
