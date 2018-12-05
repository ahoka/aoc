using System.Linq;
using AdventOfCode2018;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Day1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(12, Day2.SolveFirst("example2.txt"));
        }


        [TestMethod]
        public void TestHammingDistance()
        {
            Assert.AreEqual(1, Algo.HammingDistance("asdfghjkl", "asdfbhjkl"));
            Assert.AreEqual(0, Algo.HammingDistance("asdfghjkl", "asdfghjkl"));
        }

        [TestMethod]
        public void TestPowerSet()
        {
            var empty = Algo.PowerSet(Enumerable.Empty<int>()).ToArray();
            empty.Length.Should().Be(0);

            var single = Algo.PowerSet(new[] { 14 });
            single.Should().BeEquivalentTo(new[] { new[] { 14 } });

            var two = Algo.PowerSet(new[] { 1, 2 });
            two.Should().BeEquivalentTo(new[] { new[] { 1 },
                                                new[] { 2 },
                                                new[] { 1, 2 } });
        }

        [TestMethod]
        public void TestIsOppositePolarity()
        {
            Assert.IsTrue(Day5.ShouldRemove('b', 'B'));
            Assert.IsTrue(Day5.ShouldRemove('B', 'b'));
            Assert.IsFalse(Day5.ShouldRemove('a', 'a'));
            Assert.IsFalse(Day5.ShouldRemove('A', 'A'));
            Assert.IsFalse(Day5.ShouldRemove('a', 'B'));
        }
    }
}
