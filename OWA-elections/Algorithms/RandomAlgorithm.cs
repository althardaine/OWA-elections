using System;
using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    class RandomAlgorithm : Algorithm
    {

        private readonly int _iterations;

        public RandomAlgorithm(HashSet<Voter> voters, List<Candidate> candidates,
            OwaOperator owaOperator, ValuationType valuationType, int iterations)
            : base(voters, candidates, owaOperator, valuationType)
        {
            _iterations = iterations;
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var random = new Random();
            CreateAndCheckFirstSolution(sizeOfCommittee);

            for(var i = 0; i < _iterations; i++)
            {
                var result = new HashSet<Candidate>(Candidates.OrderBy(x => random.Next()).Take((int)sizeOfCommittee).ToList());
                CheckResult(result);
            }

            resultValue = BestResultScore;
            return BestResult;
        }

        private void CreateAndCheckFirstSolution(long sizeOfCommittee)
        {
            var result = new HashSet<Candidate>(Candidates.Take((int)sizeOfCommittee).ToList());
            CheckResult(result);
        }
        
        public override string ToString()
        {
            return "RandomAlgorithm (number of iterations: " + _iterations + ")";
        }

    }
}
