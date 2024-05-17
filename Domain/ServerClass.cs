using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ServerClass
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        [Browsable(false)]
        public DateTime Date { get; set; }
        public string FormattedDate { get => Date.ToString("yyyy-MM-dd");}
    }
}
