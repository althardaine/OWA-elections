using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections
{

    public class ResultEvaluator
    {
        public HashSet<Voter> Voters { get; private set; }
        public List<Candidate> Candidates { get; private set; }
        public OwaOperator Owa { get; private set; }
        public ValuationType ValuationType { get; private set; }

        public ResultEvaluator(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator, ValuationType valuationType)
        {
            Voters = voters;
            Candidates = candidates;
            Owa = owaOperator;
            ValuationType = valuationType;
        }

        public double Evaluate(HashSet<Candidate> result)
        {
            var value = 0.0;
            foreach (var voter in Voters)
            {
                var valuesInResult = result.Select(candidate => voter.RankList[candidate]).Select(position => ValuationType.GetValue(position)).ToList();
                valuesInResult = valuesInResult.OrderByDescending(d => d).ToList();
                value += Owa.Apply(valuesInResult);
            }
            return value;
        }

    }

}
