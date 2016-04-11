using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    class AverageValueAlgorithm : Algorithm
    {
        public AverageValueAlgorithm(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator, ValuationType valuationType) : base(voters, candidates, owaOperator, valuationType)
        {
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var averageValues = new Dictionary<Candidate, double>();
            Candidates.ForEach(candidate => averageValues[candidate] = 0.0);

            foreach (var voter in Voters)
            {
                foreach (var candidate in voter.RankList.Keys)
                {
                    averageValues[candidate] += CalculateAvarageValue(candidate, voter, sizeOfCommittee);
                }
            }

            var result = from entry in averageValues orderby entry.Value descending select entry;
            var committee = new HashSet<Candidate>();
            foreach (var candidate in result.Take((int) sizeOfCommittee))
            {
                committee.Add(candidate.Key);
            }
            CheckResult(committee);
            resultValue = BestResultScore;
            return committee;
        }

        private double CalculateAvarageValue(Candidate candidate, Voter voter, long sizeOfCommittee)
        {
            var value = ValuationType.GetValue(voter.RankList[candidate]);
            var owaVector = Owa.OperatorVector;
            var listOfPossibleValues = new List<double>();

            // case when the candidate is so far in the ranking he will
            // never be on first places for OWA vector
            if (voter.RankList[candidate] > Candidates.Count - sizeOfCommittee)
            {
                for (var i = owaVector.Count - 1; i >= Candidates.Count - voter.RankList[candidate]; i--)
                {
                    listOfPossibleValues.Add(owaVector[i] * value);
                }
                return listOfPossibleValues.Average();
            }
            // case when the candidate is so high in the ranking he will
            // never be on last places for OWA vector
            if (voter.RankList[candidate] < sizeOfCommittee)
            {
                for (var i = 0; i <= voter.RankList[candidate]; i++)
                {
                    listOfPossibleValues.Add(owaVector[i] * value);
                }
                return listOfPossibleValues.Average();
            }
            // other cases
            listOfPossibleValues.AddRange(owaVector.Select(t => t*value));
            return listOfPossibleValues.Average();
        }

        public override string ToString()
        {
            return "AverageValueAlgorithm";
        }
    }
}
