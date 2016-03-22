using System;
using System.Collections.Generic;

namespace OWA_elections
{
    public class Candidate
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

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

        public Candidate(long id, string name, double x, double y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }

        public static List<Candidate> CreateSetOfCandidates(int candidatesQuantity) 
        {
            var random = new Random();
            var candidates = new List<Candidate>();
            for (var i = 0; i < candidatesQuantity; i++)
            {
                candidates.Add(new Candidate(i, i.ToString(), random.NextDouble(), random.NextDouble()));
            }
            return candidates;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
