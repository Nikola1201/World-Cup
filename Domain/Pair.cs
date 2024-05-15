using System;
using System.ComponentModel;

namespace Domain
{
    [Serializable]
    public class Pair
    {
        [Browsable(false)]
        public int Id { get; set; }
        public Country HomeTeam { get; set; }
        public Country AwayTeam { get; set; }
        [Browsable(false)]
        public DateTime Date { get; set; }

    }
}
