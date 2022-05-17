using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker
{

    internal class CodingSession
    {
        public List<int> ListOfIDs { get; set; }
        public List<DateTime> ListOfStartTimes { get; set; }
        public List<DateTime> ListOfFinalTimes { get; set; }
        public List<double> ListOfDurations { get; set; }


    }
}
