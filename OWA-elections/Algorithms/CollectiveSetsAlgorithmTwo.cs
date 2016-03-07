using System;
using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    internal class CollectiveSetsAlgorithmTwo : Algorithm
    {

        private readonly double _beta;

        public CollectiveSetsAlgorithmTwo(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator,
            ValuationType valuationType, double beta) : base(voters, candidates, owaOperator, valuationType)
        {
            if (beta > 1.0 || beta < 0.0) throw new ArgumentException(); 
            _beta = beta;
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var numberOfNonzeroOwaElements = Owa.OperatorVector.FindAll(element => element > 0.0).Count;
            var freeSlots = new List<int>();
            for (var i = 0; i < Voters.Count; i++)
            {
                freeSlots.Add(numberOfNonzeroOwaElements);
            }

            // result committee
            var committee = new HashSet<Candidate>();
            var x = (int) (Candidates.Count*(1.0 - _beta));
            var k = sizeOfCommittee;

            for (var i = 0; i < k; i++)
            {
                var candidates = new List<Candidate>(Candidates);
                candidates.RemoveAll(candidate => committee.Contains(candidate));
                var numberOfAppearances = new Dictionary<Candidate, double>();
                candidates.ForEach(candidate => numberOfAppearances[candidate] = 0);
               
                for (var j = 0; j < Voters.Count; j++)
                {
                    candidates.ForEach(candidate =>
                    {
                        if (freeSlots[j] > 0 && Voters.ToList()[j].RankList[candidate] < x)
                        {
                            numberOfAppearances[candidate] += 1;
                        }
                    });
                }

                var a = (from entry in numberOfAppearances orderby entry.Value descending select entry).First().Key;
                for (var j = 0; j < Voters.Count; j++)
                {
                        if (freeSlots[j] > 0 && Voters.ToList()[j].RankList[a] < x)
                        {
                            freeSlots[j] -= 1;
                        }
                }
                committee.Add(a);
            }
            CheckResult(committee);
            resultValue = BestResultScore;
            return committee;
        }

        public override string ToString()
        {
            return "CollectiveSetsAlgorithmTwo";
        }
    }
}