using System.Collections.Generic;

namespace OWA_elections.OwaOperators
{
    class BasicOwa : OwaOperator
    {
        public BasicOwa(int size, int maxValue) : base(CreateOperator(size, maxValue))
        {
        }

        private static List<double> CreateOperator(int size, int maxValue)
        {
            var op = new List<double>();
            for (var i = 0; i < size; i++)
            {
                op.Add(maxValue >= 0 ? maxValue : 0);
                maxValue -= 1;
            }
            return op;
        }
    }
}
