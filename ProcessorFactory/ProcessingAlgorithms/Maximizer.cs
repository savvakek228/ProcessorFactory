using System.Collections.Generic;
using System.Linq;

namespace ProcessorFactory.ProcessingAlgorithms
{
    public static class Maximizer
    {
        public static byte Max(IEnumerable<byte> sequence)
        {
             return sequence.Max();
        }
    }
}