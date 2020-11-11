using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using ProcessorFactory;
using ProcessorFactory.ProcessingAlgorithms;

namespace ProcessorFactoryTests
{
    public class Tests
    {
        private SequenceBuilder _seqBuilder;
        [SetUp]
        public void Setup()//TODO вынести сюда все что нужно
        {
            _seqBuilder = new SequenceBuilder();
        }

        [Test]
        public void ParseSimpleTest()
        {
            string input = "3 42 42 42 42";
            Assert.IsTrue(_seqBuilder.ParseSequenceToBytes(input).Any());
        }

        [Test]
        public void ParseWithOverflowTest()
        {
            string input = "4 456 33 44 33";
            Assert.IsNull(_seqBuilder.ParseSequenceToBytes(input));
        }

        [Test]
        public void ProcessNullSequenceTest()
        {
            IEnumerable<byte> sequence = null;
            Assert.IsTrue(_seqBuilder.ProcessBlock(sequence) == 0);
        }

        [Test]
        public void ProcessUnhandledSequenceTest()
        {
            IEnumerable<byte> sequence = new[] {(byte) 6, (byte) 0x4A, unchecked((byte) 1011)};
            Assert.IsTrue(_seqBuilder.ProcessBlock(sequence) == 0);
        }

        [Test]
        public void ProcessSumTest()
        {
            IEnumerable<byte> sequence = new[] {(byte) 1, (byte) 44, (byte) 255};
            Assert.AreEqual(_seqBuilder.ProcessBlock(sequence),44);
        }

        [Test]
        public void ProcessMultiplyTest()
        {
            IEnumerable<byte> sequence = new[] {(byte) 2, (byte) 44, (byte) 255};
            Assert.AreEqual(_seqBuilder.ProcessBlock(sequence),168);
        }

        [Test]
        public void ProcessMaxTest()
        {
            IEnumerable<byte> sequence = new[] {(byte) 3, (byte) 44, (byte) 255};
            Assert.AreEqual(255,_seqBuilder.ProcessBlock(sequence));
        }

        [Test]
        public void ProcessMinTest()
        {
            IEnumerable<byte> sequence = new[] {(byte) 4, (byte) 44, (byte) 255};
            Assert.AreEqual(4,_seqBuilder.ProcessBlock(sequence));
        }


        [Test]
        public void CountMedianTest()
        {
            IEnumerable<byte> sequence = new[] {(byte) 5, (byte) 6, (byte) 7};
            Assert.AreEqual(_seqBuilder.CountMedian(sequence,sequence.Count()),6);
        }

        [Test]
        public void CountMedianWithArrayMask()
        {
            IEnumerable<byte> sequence = new[] {(byte) 5, (byte) 6, (byte) 7, (byte) 9, (byte) 150, (byte) 3};
            var mask = new[]
            {
                3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39, 41, 43, 45, 47, 49, 51, 53, 55,
                57, 59, 61, 63, 65
            };
            Assert.IsTrue(_seqBuilder.CountMedian(sequence,mask[new Random().Next(mask.Length)]) != 0);
        }

        [Test]
        public void End2EndTest()
        {
            
        }
    }
}