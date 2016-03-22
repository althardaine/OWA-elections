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
            var positions = order.OrderBy(i => random.Next()).ToList();
            for (var i = 0; i < candidates.Count; i++)
            {
                RankList[candidates[i]] = positions[i];
            }
        }

        public Voter(long id)
        {
            Id = id;
            RankList = new Dictionary<Candidate, long>();
        }

        public Voter(long id, Dictionary<Candidate, long> rankList)
        {
            RankList = rankList;
            Id = id;
        }

        public static HashSet<Voter> CreateImpartialCultureSetOfVoters(IReadOnlyList<Candidate> candidates, long numberOfVoters)
        {
            var voters = new HashSet<Voter>();
            var random = new Random();
            for (var i = 0; i < numberOfVoters; i++)
            {
                voters.Add(new Voter(i, candidates, random));
            }
            return voters;
        }

        public static HashSet<Voter> CreateSquareDistributionSetOfVoters(IReadOnlyList<Candidate> candidates)
        {
            var voters = new HashSet<Voter>();
            var id = 0;
            foreach (var candidate in candidates)
            {
                var distanceMap = new Dictionary<Candidate, double>();
                foreach (var c in candidates)
                {
                    var distance = Math.Sqrt(Math.Pow(candidate.X - c.X, 2) + Math.Pow(candidate.Y - c.Y, 2));
                    distanceMap[c] = distance;
                }
                var sorted = (from entry in distanceMap orderby entry.Value ascending select entry).ToList();
                var rankList = new Dictionary<Candidate, long>();
                var i = 0;
                sorted.ForEach(pair =>
                {
                    rankList[pair.Key] = i;
                    i += 1;
                });
                voters.Add(new Voter(id, rankList));
                id += 1;
            }
            return voters;
        }

        public static HashSet<Voter> CreateUrnModelSetOfVoters(IReadOnlyList<Candidate> candidates, long numberOfvoters)
        {
            var random = new Random();
            var probabilitySum = candidates.Count;
            var probabilityTable = new List<int>();
            for (var i = 0; i < candidates.Count; i++)
            {
                probabilityTable.Add(1);
            }
            var voters = new HashSet<Voter>();
            for (var i = 0; i < numberOfvoters; i++)
            {
                voters.Add(new Voter(i));
            }

            for (var i = 0; i < candidates.Count; i++)
            {
                foreach (var voter in voters)
                {
                    var selectedNumber = random.Next(0, probabilitySum);
                    var candidateId = FindCandidateNumber(selectedNumber, probabilityTable);
                    var candidate = FindCandidate(candidateId, candidates);
                    while (voter.RankList.ContainsKey(candidate))
                    {
                        selectedNumber = random.Next(0, probabilitySum);
                        candidateId = FindCandidateNumber(selectedNumber, probabilityTable);
                        candidate = FindCandidate(candidateId, candidates);
                    }

                    probabilityTable[candidateId] += 1;
                    probabilitySum += 1;
                    voter.RankList[candidate] = i;
                }
            }

            return voters;
        }

        private static Candidate FindCandidate(int candidateId, IEnumerable<Candidate> candidates)
        {
            return candidates.FirstOrDefault(candidate => candidate.Id == candidateId);
        }

        private static int FindCandidateNumber(int selectedNumber, IReadOnlyList<int> probabilityTable)
        {
            var sum = 0;
            for (var i = 0; i < probabilityTable.Count; i++)
            {
                sum += probabilityTable[i];
                if (selectedNumber < sum)
                {
                    return i;
                }
            }
            return probabilityTable.Count - 1;
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
