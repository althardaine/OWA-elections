using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    class LinearProgressionOwa : OwaOperator
    {
        public LinearProgressionOwa(int size) : base(CreateOperator(size))
        {
        }

        private static List<double> CreateOperator(int size)
        {
            var op = new List<double> {1.0};
            for (var i = 1.0; i < size; i++)
            {
                op.Add(1 - (i/size));
            }
            return op;
        }
    }
}
