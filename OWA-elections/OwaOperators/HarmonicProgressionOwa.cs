using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    internal class HarmonicProgressionOwa : OwaOperator
    {
        public HarmonicProgressionOwa(int size) : base(CreateOperator(size))
        {
        }

        private static List<double> CreateOperator(int size)
        {
            var op = new List<double> {1.0};
            for (var i = 1; i < size; i++)
            {
                op.Add(1.0/(i + 1));
            }
            return op;
        }
    }
}