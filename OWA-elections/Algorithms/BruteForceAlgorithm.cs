using System.Collections.Generic;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    public class BruteForceAlgorithm : Algorithm
    {

        public BruteForceAlgorithm(HashSet<Voter> voters, List<Candidate> candidates,
            OwaOperator owaOperator, ValuationType valuationType)
            : base(voters, candidates, owaOperator, valuationType)
        {
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            Subset(sizeOfCommittee, 0, 0, new bool[Candidates.Count]);

            resultValue = BestResultScore;
            return BestResult;
        }

        private void Subset(long k, int start, int currLen, IList<bool> used)
        {
            var result = new HashSet<Candidate>();

		    if (currLen == k) {
			    for (var i = 0; i < Candidates.Count; i++) {
				    if (used[i])
				    {
				        result.Add(Candidates[i]);
				    }
			    }
                CheckResult(result);
			    return;
		    }
		    if (start == Candidates.Count) {
			    return;
		    }
		    used[start] = true;
            Subset(k, start + 1, currLen + 1, used);
            used[start] = false;
            Subset(k, start + 1, currLen, used);
	    }

        public override string ToString()
        {
            return "BruteForceAlgorithm";
        }
    }
}
