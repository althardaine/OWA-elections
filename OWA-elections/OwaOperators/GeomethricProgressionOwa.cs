using System;
using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    internal class GeomethricProgressionOwa : OwaOperator
    {
        public GeomethricProgressionOwa(int size, double p)
            : base(CreateOperator(size, p))
        {
        }

        private static List<double> CreateOperator(int size, double p)
        {
            if (p >= 1 || p <= 0) throw new ArgumentException();
            var op = new List<double> {1.0};
            for (var i = 1; i < size; i++)
            {
                op.Add(Math.Pow(p, i));
            }
            return op;
        }
    }
}