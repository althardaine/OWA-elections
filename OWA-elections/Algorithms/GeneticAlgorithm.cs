using System;
using System.Collections.Generic;
using System.Linq;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections.Algorithms
{
    class GeneticAlgorithm : Algorithm
    {
        public GeneticAlgorithm(HashSet<Voter> voters, List<Candidate> candidates, OwaOperator owaOperator, ValuationType valuationType) : base(voters, candidates, owaOperator, valuationType)
        {
        }

        public override HashSet<Candidate> Execute(long sizeOfCommittee, out double resultValue)
        {
            const int populationSize = 100;
            const int numberOfIterations = 100;
            var random = new Random();
            
            var population = new List<Tuple<HashSet<Candidate>, double>>();
            //create first population (completely random)
            CreatePopulation(populationSize, sizeOfCommittee, random).ForEach(specimen =>
                population.Add(new Tuple<HashSet<Candidate>, double>(specimen, CheckResult(specimen))));

            for (var i = 0; i < numberOfIterations; i++)
            {
                var population1 = population.Take(populationSize).ToList();
                var population2 = Enumerable.Reverse(population).Take(populationSize).Reverse().ToList();
                population1.Zip(population2, (a, b) => Crossover(a.Item1, b.Item1, sizeOfCommittee))
                    .ToList()
                    .ForEach(specimen =>
                        population.Add(new Tuple<HashSet<Candidate>, double>(specimen, CheckResult(specimen))));

                CreatePopulation(populationSize/4, sizeOfCommittee, random).ForEach(specimen =>
                    population.Add(new Tuple<HashSet<Candidate>, double>(specimen, CheckResult(specimen))));

                population = population.OrderByDescending(x => x.Item2).Take(populationSize).ToList();
            }

            resultValue = BestResultScore;
            return BestResult;
        }

        private static HashSet<Candidate> Crossover(HashSet<Candidate> parent1, HashSet<Candidate> parent2, long sizeOfCommittee)
        {
            var child = new HashSet<Candidate>(parent1.Intersect(parent2));
            var exclusive = parent1.Except(parent2).ToList();
            exclusive.Take((int) sizeOfCommittee - child.Count).ToList().ForEach(candidate => child.Add(candidate));
            return child;
        }

        private List<HashSet<Candidate>> CreatePopulation(int populationSize, long sizeOfCommittee, Random random)
        {
            var population = new List<HashSet<Candidate>>();
            for (var i = 0; i < populationSize; i++)
            {
                population.Add(CreateRandomSolution(sizeOfCommittee, random));
            }
            return population;
        }

        private HashSet<Candidate> CreateRandomSolution(long sizeOfCommittee, Random random)
        {
            return new HashSet<Candidate>(Candidates.OrderBy(i => random.Next()).Take((int)sizeOfCommittee));
        }

    }
}
