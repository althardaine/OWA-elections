using System;
using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    internal class GeneticAlgorithm : Algorithm
    {
        private static readonly Random Random = new Random();
        private readonly int _numberOfIterations;
        private readonly int _populationSize;

        public GeneticAlgorithm(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator,
            ValuationType valuationType, int populationSize, int numberOfIterations)
            : base(voters, candidates, owaOperator, valuationType)
        {
            _populationSize = populationSize;
            _numberOfIterations = numberOfIterations;
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            var population = new List<Tuple<HashSet<Candidate>, double>>();
            //create first population (completely random)
            CreatePopulation(_populationSize, sizeOfCommittee).ForEach(specimen =>
                population.Add(new Tuple<HashSet<Candidate>, double>(specimen, CheckResult(specimen))));

            for (var i = 0; i < _numberOfIterations; i++)
            {
                var population1 = population.Take(_populationSize).ToList();
                var population2 = Enumerable.Reverse(population).Take(_populationSize).Reverse().ToList();
                population1.Zip(population2, (a, b) => Mutate(Crossover(a.Item1, b.Item1, sizeOfCommittee)))
                    .ToList()
                    .ForEach(specimen =>
                        population.Add(new Tuple<HashSet<Candidate>, double>(specimen, CheckResult(specimen))));

                CreatePopulation(_populationSize/4, sizeOfCommittee).ForEach(specimen =>
                    population.Add(new Tuple<HashSet<Candidate>, double>(specimen, CheckResult(specimen))));

                population = population.OrderByDescending(x => x.Item2).Take(_populationSize).ToList();
            }

            resultValue = BestResultScore;
            return BestResult;
        }

        private static HashSet<Candidate> Crossover(HashSet<Candidate> parent1, HashSet<Candidate> parent2,
            long sizeOfCommittee)
        {
            var child = new HashSet<Candidate>(parent1.Intersect(parent2));
            var exclusive = parent1.Except(parent2).ToList();
            exclusive.Take((int) sizeOfCommittee - child.Count).ToList().ForEach(candidate => child.Add(candidate));
            return child;
        }

        private HashSet<Candidate> Mutate(HashSet<Candidate> committee)
        {
            if (Random.Next(0, 100) < 90)
            {
                return committee;
            }

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

        private List<HashSet<Candidate>> CreatePopulation(int populationSize, long sizeOfCommittee)
        {
            var population = new List<HashSet<Candidate>>();
            for (var i = 0; i < populationSize; i++)
            {
                population.Add(CreateRandomSolution(sizeOfCommittee));
            }
            return population;
        }

        private HashSet<Candidate> CreateRandomSolution(long sizeOfCommittee)
        {
            return new HashSet<Candidate>(Candidates.OrderBy(i => Random.Next()).Take((int) sizeOfCommittee));
        }
    }
}