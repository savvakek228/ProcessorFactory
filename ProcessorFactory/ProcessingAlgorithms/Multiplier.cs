using System.Collections.Generic;
using System.Linq;

namespace ProcessorFactory.ProcessingAlgorithms
{
    public static class Multiplier
    {
        //TODO перемножить последовательность сначала
        public static byte Multiply(IEnumerable<byte> sequence)
        {
            byte evalX = sequence.ToArray()[0];
            for (int i = 1; i < sequence.Count(); i++)
            {
                evalX *= sequence.ToArray()[i];
            }
            return unchecked((byte)(evalX % 0xFF));
        }
    }
}