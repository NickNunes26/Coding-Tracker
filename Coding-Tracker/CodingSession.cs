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
        public List<int> ListOfIDs { get; set; } = new List<int>();
        public List<DateTime> ListOfStartTimes { get; set; } = new List<DateTime>();
        public List<DateTime> ListOfFinalTimes { get; set; } = new List<DateTime>();
        public List<double> ListOfDurations { get; set; } = new List<double>();


    }
}
