using System.Collections.Generic;
using System.Linq;

namespace ProcessorFactory.ProcessingAlgorithms
{
    public static class Summator
    {
        //TODO пересчитать сначала последовательность, потом % 0хFF
        public static byte Sum(IEnumerable<byte> sequence)
        {
            byte evalX = sequence.ToArray()[0];
            for (int i = 1; i < sequence.Count(); i++)
            {
                evalX += sequence.ToArray()[i];
            }
            return unchecked((byte)(evalX % 0xFF));
        }
    }
}