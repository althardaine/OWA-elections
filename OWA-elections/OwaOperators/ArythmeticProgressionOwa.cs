using System;
using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    class ArythmeticProgressionOwa : OwaOperator
    {
        public ArythmeticProgressionOwa(int size, double firstElement, double r)
            : base(CreateOperator(size, firstElement, r))
        {
        }

        private static List<double> CreateOperator(int size, double firstElement, double r)
        {
            if (firstElement <= 0 || r <= 0) throw new ArgumentException();
            var op = new List<double>();
            for (var i = 0; i < size; i++)
            {
                op.Add(firstElement >= 0 ? firstElement : 0);
                firstElement -= r;
            }
            return op;
        }
    }
}
