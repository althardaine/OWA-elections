using System;
using System.Collections.Generic;
using System.Linq;

namespace OWA_elections
{
    public class Voter
    {

        public long Id { get; private set; }

        public Dictionary<Candidate, long> RankList;

        public Voter(long id, IReadOnlyList<Candidate> candidates, Random random)
        {
            Id = id;
            RankList = new Dictionary<Candidate, long>();
            var order = new List<long>();
            for (var i = 0; i < candidates.Count; i++)
            {
                order.Add(i);
            }
            for (var i = 0; i < candidates.Count; i++)
            {
                var indexToChange = random.Next(0, candidates.Count);
                var tmp = order[i];
                order[i] = order[indexToChange];
                order[indexToChange] = tmp;
            }
            for (var i = 0; i < candidates.Count; i++)
            {
                RankList[candidates[i]] = order[i];
            }
        }

        public Voter(long id, Dictionary<Candidate, long> rankList)
        {
            RankList = rankList;
            Id = id;
        }

        public static HashSet<Voter> CreateSetOfVoters(IReadOnlyList<Candidate> candidates, long numberOfVoters)
        {
            var voters = new HashSet<Voter>();
            var random = new Random();
            for (var i = 0; i < numberOfVoters; i++)
            {
                voters.Add(new Voter(i, candidates, random));
            }
            return voters;
        }

        public override string ToString()
        {
            var sortedRankList = from entry in RankList orderby entry.Value ascending select entry;
            var toReturn = "";
            toReturn += Id + ": ";
            return sortedRankList.Aggregate(toReturn, (current, candidate) => current + (candidate.Key.Id + " "));
        }
    }
}
