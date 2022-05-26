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
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string FinalTime { get; set; }
        public double Duration { get; set; }


    }
}
