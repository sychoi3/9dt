using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Models
{
    public class GameStateResource
    {
        public IList<string> players { get; set; }
        public string state { get; set; }    // "DONE/IN_PROGRESS"
        public string winner { get; set; }

        // in case of draw, winner will be null, state will be DONE.
        // in case game is still in progess, key should not exist.
    }
}
