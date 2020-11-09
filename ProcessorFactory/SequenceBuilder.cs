using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.FSharp.Collections;

namespace ProcessorFactory
{
    public class SequenceBuilder
    {

        private IEnumerable<byte> _mainSequence;

        public static byte Process(string block, int window)
        {
            return 0;//TODO
        }

        //TODO оптимизация, As Parallel глянуть
        public IEnumerable<byte> ParseSequenceToBytes(string sequence) =>
            sequence.Split(' ').Select(byte.Parse);

        //TODO оптимизация
        public byte ProcessBlock(IEnumerable<byte> block)
        {
            var bytes = block as byte[] ?? block.ToArray();
            return bytes.First() switch
            {
                1 => unchecked((byte) bytes.Sum(b => b % 0xFF)),
                2 => unchecked((byte) bytes.Aggregate(0, (current, b) => current * (b % 0xFF))),
                3 => bytes.Max(),
                4 => bytes.Min(),
                _ => throw new ArgumentException("No device found by control byte")
            };
        }
        
        //TODO оптимизация
        public byte CountMedian(IEnumerable<byte> sequence, int window)
        {
            var listToSort = sequence.TakeLast(window);
            var toCount = Sorts.QuickSort(listToSort as FSharpList<byte>);
            return toCount[toCount.Length / 2];
        }
    }
}
