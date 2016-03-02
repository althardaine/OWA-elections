using System;
using OWA_elections.Algorithms;
using OWA_elections.Data.Read;
using OWA_elections.OwaOperators;
using OWA_elections.ValuationTypes;

namespace OWA_elections
{
    class Program
    {
        static void Main(string[] args)
        {
            var candidates = Candidate.CreateSetOfCandidates(20);
            var voters = Voter.CreateSetOfVoters(candidates, 1500);

//            DataWriter.WriteData(candidates, voters, "d:\\out.txt");
            DataReader.ReadData(out candidates, out voters, "d:\\out.txt");

            var valuationType = new BordaCount(candidates.Count);

//            var owaOperator = new BasicOwa(4, 3);
            var owaOperator = new ArythmeticProgressionOwa(4, 3 , 1);
//            var owaOperator = new GeomethricProgressionOwa(4, 1, 2);

            long sizeOfCommitee = owaOperator.OperatorVector.Count;

            Algorithm algoritm = new BruteForceAlgorithm(voters, candidates, owaOperator, valuationType);

            var tester = new AlgorithmTester(algoritm);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new AverageValueAlgorithm(voters, candidates, owaOperator, valuationType);
            tester.TestAlgorithm(sizeOfCommitee);
            tester.Algorithm = new RandomAlgorithm(voters, candidates, owaOperator, valuationType, 100);
            tester.TestAlgorithm(sizeOfCommitee);
            Console.ReadLine();
        }
    }
}
