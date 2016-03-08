using System;
using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    internal class SimulatedAnnealingAlgorithm : Algorithm
    {
        private double _temperature;
        private readonly double _coolingRate;
        private readonly Random _random = new Random();

        public SimulatedAnnealingAlgorithm(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator,
            ValuationType valuationType, int initialTemperature, double coolingRate) : base(voters, candidates, owaOperator, valuationType)
        {
            _temperature = initialTemperature;
            _coolingRate = coolingRate;
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var currentCommittee = new HashSet<Candidate>(Candidates.Take((int) sizeOfCommittee));
            var currentResult = Evaluator.Evaluate(currentCommittee);

            while (_temperature > 1.0)
            {
                var committee = ChangeSolution(currentCommittee);
                var result = CheckResult(committee);
                var probability = GetAcceptanceProbability(currentResult, result, _temperature);
                if (_random.Next(0, 100) < probability)
                {
                    currentCommittee = committee;
                    currentResult = result;
                }
                _temperature *= 1 - _coolingRate;
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

        private HashSet<Candidate> ChangeSolution(HashSet<Candidate> committee)
        {
            var newCommittee = new HashSet<Candidate>(committee);
            var candidateToChange = newCommittee.ToList()[_random.Next(0, newCommittee.Count)];
            newCommittee.Remove(candidateToChange);
            var candidateToInsert = Candidates[_random.Next(0, Candidates.Count)];
            while (newCommittee.Contains(candidateToInsert))
            {
                candidateToInsert = Candidates[_random.Next(0, Candidates.Count)];
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