using System.Collections.Generic;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{

    public abstract class Algorithm
    {

        protected HashSet<Candidate> BestResult;
        protected double BestResultScore;
        public HashSet<Voter> Voters { get; private set; }
        public List<Candidate> Candidates { get; private set; }
        public OwaOperator Owa { get; private set; }
        public ValuationType ValuationType { get; private set; }
        public ResultEvaluator Evaluator { get; private set; }

        protected Algorithm(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator, ValuationType valuationType)
        {
            Voters = voters;
            Candidates = candidates;
            Owa = owaOperator;
            ValuationType = valuationType;
            Evaluator =  new ResultEvaluator(voters, candidates, owaOperator, valuationType);
        }

        public abstract HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue);

        protected double CheckResult(HashSet<Candidate> result)
        {
            var value = Evaluator.Evaluate(result);
            if (!(value > BestResultScore)) return value;
            BestResultScore = value;
            BestResult = new HashSet<Candidate>(result);
            return BestResultScore;
        }

    }

}
