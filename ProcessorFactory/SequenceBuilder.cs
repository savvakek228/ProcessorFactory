using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Utilities;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.FSharp.Collections;
using ProcessorFactory.ProcessingAlgorithms;

namespace ProcessorFactory
{
    public class SequenceBuilder
    {
        private IEnumerable<byte> _mainSequence;

        public byte Process(string block, int window)
        {
            _mainSequence.ToList().Add(ProcessBlock(ParseSequenceToBytes(block)));
            while (!(_mainSequence.Count() < window))
            {
                return 0;
            }
            return CountMedian(_mainSequence, window);
        }

        //TODO оптимизация, As Parallel глянуть
        public IEnumerable<byte> ParseSequenceToBytes(string sequence)
        {
            try
            {
                var result = sequence.Split(' ').Select(byte.Parse).ToList();
                return result;
            }
            catch (OverflowException)
            {
                Debug.WriteLine("input was not in a correct format");
                return null;
            }
        }

        //TODO оптимизация
        public byte ProcessBlock(IEnumerable<byte> block)
        {
            IEnumerable<byte> bytes;
            try
            {
                bytes = block as byte[] ?? block.ToArray();
            }
            catch (ArgumentNullException)
            {
                Debug.WriteLine("block is empty");
                return 0;
            }
            switch (bytes.First())
            {
                case 1:
                    return Summator.Sum(bytes);
                case 2:
                    return Multiplier.Multiply(bytes);
                case 3:
                    return Maximizer.Max(bytes);
                case 4:
                    return Minimizer.Min(bytes);
                default:
                    Debug.WriteLine("No control byte found for this sequence");
                    break;
            }
            return 0;
        }
        
        //TODO оптимизация
        public byte CountMedian(IEnumerable<byte> sequence, int window)
        {
            var listToSort = sequence.TakeLast(window);
            var toCount = Sorts.QuickSort(ListModule.OfSeq(listToSort));
            return toCount[toCount.Length / 2];
        }
    }
}
