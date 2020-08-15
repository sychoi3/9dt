using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Dtos
{
    public class CreateGameDto
    {
        public IList<string> Players;
        public int Rows;
        public int Columns;
    }
}
