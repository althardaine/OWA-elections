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
            List<Candidate> candidates;
//            var voters = Voter.CreateImpartialCultureSetOfVoters(candidates, 1500);
            HashSet<Voter> voters;

//            DataWriter.WriteData(candidates, voters, "d:\\out2.txt");
            DataReader.ReadData(out candidates, out voters, "d:\\out2.txt");

            var valuationType = new BordaCount(candidates.Count);

//            var owaOperator = new BasicOwa(4, 3);
//            var owaOperator = new LinearProgressionOwa(4);
//            var owaOperator = new GeomethricProgressionOwa(4, 0.75);
            var owaOperator = new HarmonicProgressionOwa(4);

            long sizeOfCommitee = owaOperator.OperatorVector.Count;

            Algorithm algoritm = new BruteForceAlgorithm(voters, candidates, owaOperator, valuationType);

            var tester = new AlgorithmTester(algoritm);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new AverageValueAlgorithm(voters, candidates, owaOperator, valuationType);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new RandomAlgorithm(voters, candidates, owaOperator, valuationType, 100);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new CollectiveSetsAlgorithmOne(voters, candidates, owaOperator, valuationType);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new CollectiveSetsAlgorithmTwo(voters, candidates, owaOperator, valuationType, 0.5);
            tester.TestAlgorithm(sizeOfCommitee);
            Console.ReadLine();
        }
    }
}
