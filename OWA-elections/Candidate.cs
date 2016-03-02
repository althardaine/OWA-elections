using System.Collections.Generic;

namespace OWA_elections
{
    public class Candidate
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public Candidate(long id)
        {
            Id = id;
            Name = id.ToString();
        }

        public Candidate(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public static List<Candidate> CreateSetOfCandidates(int candidatesQuantity) 
        {
            var candidates = new List<Candidate>();
            for (var i = 0; i < candidatesQuantity; i++)
            {
                candidates.Add(new Candidate(i));
            }
            return candidates;
        }

    }
}
