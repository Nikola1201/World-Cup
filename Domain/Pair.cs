using System;

namespace Domain
{
    [Serializable]
    public class Pair
    {
        public int Id { get; set; }
        public Country HomeTeam { get; set; }
        public Country AwayTeam { get; set; }
        public DateTime Date { get; set; }

    }
}
