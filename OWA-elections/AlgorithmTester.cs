using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using OWA_elections.Algorithms;

namespace OWA_elections
{
    class AlgorithmTester
    {

        public Algorithm Algorithm { get; set; }

        public AlgorithmTester(Algorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public void TestAlgorithm(long sizeOfCommittee)
        {
            TestAlgorithm(sizeOfCommittee, Console.Out);
        }

        public void TestAlgorithm(long sizeOfCommittee, string outputPath)
        {
            TestAlgorithm(sizeOfCommittee, new StreamWriter(outputPath));
        }

        public void TestAlgorithm(long sizeOfCommittee, TextWriter output, int numberOfTries)
        {
            output.WriteLine(Algorithm.ToString());
            for (var i = 0; i < numberOfTries; i++)
            {
                var watch = Stopwatch.StartNew();

                double resultValue;
                var result = Algorithm.Execute(sizeOfCommittee, out resultValue);

                watch.Stop();
                var elapsedMilliseconds = watch.ElapsedMilliseconds;


                var enumerableResult = result.OrderBy(candidate => candidate.Id);

                Console.Write(string.Join(",", enumerableResult));
                output.WriteLine(";{0};{1}", resultValue, elapsedMilliseconds);
            }
            output.Flush();
        }

        private void TestAlgorithm(long sizeOfCommittee, TextWriter output)
        {
            double resultValue;
            var watch = Stopwatch.StartNew();

            var result = Algorithm.Execute(sizeOfCommittee, out resultValue);

            watch.Stop();
            var elapsedMilliseconds = watch.ElapsedMilliseconds;

            output.WriteLine(Algorithm.ToString());
            output.WriteLine("Best committee:");

            var enumerableResult = result.OrderBy(candidate => candidate.Id);

            foreach (var candidate in enumerableResult)
            {
                output.Write(candidate.Id + " ");
            }
            output.WriteLine();
            output.WriteLine("With score:");
            output.WriteLine(resultValue);
            output.WriteLine("Time [ms]: " + elapsedMilliseconds);
            output.WriteLine();
            output.Flush();
        }

    }
}
