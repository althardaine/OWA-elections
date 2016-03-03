using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    class HalfOnesOwa : OwaOperator
    {
        public HalfOnesOwa(int size) : base(CreateOperator(size))
        {
        }

        private static List<double> CreateOperator(int size)
        {
            var op = new List<double>();
            for (var i = 0; i < size; i++)
            {
                op.Add(i < size/2 ? 1 : 0);
            }
            return op;
        }
    }
}
