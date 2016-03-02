using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OWA_elections.Data.Read
{
    public static class DataReader
    {

        public static void ReadData(out List<Candidate> candidates, out HashSet<Voter> voters, string inputFile)
        {
            using (var file = new StreamReader(inputFile))
            {
                candidates = new List<Candidate>();
                var numberOfCandidatesAsString = file.ReadLine();
                if (numberOfCandidatesAsString == null) throw new InvalidDataException();
                var numberOfCandidates = long.Parse(numberOfCandidatesAsString);
                for (var i = 0; i < numberOfCandidates; i++)
                {
                    var data = file.ReadLine();
                    if (data == null) throw new InvalidDataException();
                    var splitted = data.Split(',');
                    if (splitted.Length != 2) throw new InvalidDataException();
                    candidates.Add(new Candidate(long.Parse(splitted[0]), splitted[1]));
                }

                voters = new HashSet<Voter>();
                var numberOfVotersAsString = file.ReadLine();
                if (numberOfVotersAsString == null) throw new InvalidDataException();
                var numberOfVoters = long.Parse(numberOfVotersAsString);
                for (var i = 0; i < numberOfVoters; i++)
                {
                    var data = file.ReadLine();
                    if (data == null) throw new InvalidDataException();
                    var splitted = data.Split(',');
                    if (splitted.Length != numberOfCandidates) throw new InvalidDataException();
                    var rankList = new Dictionary<Candidate, long>();
                    for (var j = 0; j < candidates.Count; j++)
                    {
                        var candidate = FindCandidate(candidates, long.Parse(splitted[j]));
                        rankList[candidate] = j;
                    }
                    voters.Add(new Voter(i, rankList));
                }
            }
        }

        private static Candidate FindCandidate(IEnumerable<Candidate> candidates, long candidateId)
        {
            return candidates.FirstOrDefault(candidate => candidate.Id == candidateId);
        }
    }
}
