namespace OWA_elections.ValuationTypes
{
    public class BordaCount : ValuationType
    {

        public long NumberOfCandidates { get; private set; }

        public override double GetValue(long position)
        {
            return NumberOfCandidates - 1 - position;
        }

        public BordaCount(long numberOfCandidates)
        {
            NumberOfCandidates = numberOfCandidates;
        }

    }
}
