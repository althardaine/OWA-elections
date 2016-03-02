using System;
using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    class GeomethricProgressionOwa : OwaOperator
    {
        public GeomethricProgressionOwa(int size, double firstElement, double p)
            : base(CreateOperator(size, firstElement, p))
        {
        }

        private static List<double> CreateOperator(int size, double firstElement, double p)
        {
            if (firstElement <= 0 || p <= 0) throw new ArgumentException();
            var op = new List<double>();
            for (var i = 0; i < size; i++)
            {
                op.Add(firstElement >= 0 ? firstElement : 0);
                firstElement /= p;
            }
            return op;
        }
    }
}
