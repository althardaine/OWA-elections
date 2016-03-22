using System;
using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    internal class SimulatedAnnealingAlgorithm : Algorithm
    {
        private readonly double _initialTemperature;
        private readonly double _coolingRate;
        private static readonly Random Random = new Random();

        public SimulatedAnnealingAlgorithm(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator,
            ValuationType valuationType, int initialInitialTemperature, double coolingRate) : base(voters, candidates, owaOperator, valuationType)
        {
            _initialTemperature = initialInitialTemperature;
            _coolingRate = coolingRate;
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var temperature = _initialTemperature;
            var currentCommittee = new HashSet<Candidate>(Candidates.Take((int) sizeOfCommittee));
            var currentResult = Evaluator.Evaluate(currentCommittee);

            while (temperature > 1.0)
            {
                var committee = ChangeSolution(currentCommittee);
                var result = CheckResult(committee);
                var probability = GetAcceptanceProbability(currentResult, result, temperature);
                if (Random.Next(0, 100) < probability)
                {
                    currentCommittee = committee;
                    currentResult = result;
                }
                temperature *= 1 - _coolingRate;
            }

            CheckResult(currentCommittee);
            resultValue = BestResultScore;
            return BestResult;
        }

        private static int GetAcceptanceProbability(double energy, double newEnergy, double temperature)
        {
            if (newEnergy > energy) return 100;
            return (int) (Math.Exp((newEnergy - energy)/temperature) * 100);
        }

        private HashSet<Candidate> ChangeSolution(IEnumerable<Candidate> committee)
        {
            var newCommittee = new HashSet<Candidate>(committee);
            var candidateToChange = newCommittee.ToList()[Random.Next(0, newCommittee.Count)];
            newCommittee.Remove(candidateToChange);
            var candidateToInsert = Candidates[Random.Next(0, Candidates.Count)];
            while (newCommittee.Contains(candidateToInsert))
            {
                candidateToInsert = Candidates[Random.Next(0, Candidates.Count)];
            }
            newCommittee.Add(candidateToInsert);
            return newCommittee;
        }

        public override string ToString()
        {
            return "SimulatedAnnealingAlgorithm";
        }

    }
}