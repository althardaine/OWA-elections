using System.Collections.Generic;
using System.Linq;

namespace OWA_elections.OwaOperators
{
    public class OwaOperator
    {

        public List<double> OperatorVector { get; private set; }

        public OwaOperator(List<double> operatorVector)
        {
            OperatorVector = operatorVector;
        }

        public double Apply(List<double> vector)
        {
//            return OperatorVector.Select((t, i) => t*vector[i]).Sum();
            return vector.Select((t, i) => t*OperatorVector[i]).Sum();
        }
    }
}
