using System;
using System.Collections.Generic;
using OWA_elections.Algorithms;
using OWA_elections.Data.Read;
using OWA_elections.Data.Write;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections
{
    class Program
    {
        static void Main(string[] args)
        {
            var candidates = Candidate.CreateSetOfCandidates(500);
            var voters = Voter.CreateUrnModelSetOfVoters(candidates, 500);
//            List<Candidate> candidates;
//            HashSet<Voter> voters;

//            DataWriter.WriteData(candidates, voters, "d:\\out3.txt");
            DataReader.ReadData(out candidates, out voters, "d:\\out3.txt");

            var valuationType = new BordaCount(candidates.Count);

            var owaOperator = new BasicOwa(20, 3);
//            var owaOperator = new LinearProgressionOwa(4);
//            var owaOperator = new GeomethricProgressionOwa(4, 0.75);
//            var owaOperator = new HarmonicProgressionOwa(10);

            long sizeOfCommitee = owaOperator.OperatorVector.Count;

            Algorithm algoritm = new BruteForceAlgorithm(voters, candidates, owaOperator, valuationType);

            var tester = new AlgorithmTester(algoritm);
//            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new AverageValueAlgorithm(voters, candidates, owaOperator, valuationType);
            tester.TestAlgorithm(sizeOfCommitee, Console.Out, 3);
            tester.Algorithm = new RandomAlgorithm(voters, candidates, owaOperator, valuationType, 500);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new CollectiveSetsAlgorithmOne(voters, candidates, owaOperator, valuationType);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new CollectiveSetsAlgorithmTwo(voters, candidates, owaOperator, valuationType, 0.5);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new SimulatedAnnealingAlgorithm(voters, candidates, owaOperator, valuationType, 10000, 0.02);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new GeneticAlgorithm(voters, candidates, owaOperator, valuationType, 24, 200);
            tester.TestAlgorithm(sizeOfCommitee);
            Console.ReadLine();
        }
    }
}
