using System.Collections.Generic;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    class CollectiveSetsAlgorithmOne : Algorithm
    {
        public CollectiveSetsAlgorithmOne(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator, ValuationType valuationType) 
            : base(voters, candidates, owaOperator, valuationType)
        {
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var committee = new HashSet<Candidate>();
            for (var i = 0; i < sizeOfCommittee; i++)
            {
                AddAndTry(committee);
                committee = BestResult;
            }
            resultValue = BestResultScore;
            return BestResult;
        }

        private void AddAndTry(HashSet<Candidate> committee)
        {
            Candidates.ForEach(candidate =>
            {
                if (committee.Contains(candidate)) return;
                committee.Add(candidate);
                CheckResult(committee);
                committee.Remove(candidate);
            });
        }

        public override string ToString()
        {
            return "CollectiveSetsAlgorithmOne";
        }
    }
}
