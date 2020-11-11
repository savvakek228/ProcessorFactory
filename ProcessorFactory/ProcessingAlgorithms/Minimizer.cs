using System.Collections.Generic;
using System.Linq;

namespace ProcessorFactory.ProcessingAlgorithms
{
    public static class Minimizer
    {
        public static byte Min(IEnumerable<byte> sequence)
        {
            return sequence.Min();
        }
    }
}