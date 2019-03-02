using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LRSR_Tests
{
    public class RingTests
    {
        [Fact]
        public void IndexerTest()
        {
            var r = new Ring("10001");

            Assert.Equal('1', r[0]);
            Assert.Equal('1', r[4]);
            Assert.Equal('1', r[5]);
            Assert.Equal('1', r[10]);
        }

        [Fact]
        public void EqualsTest()
        {
            var r1 = new Ring("1001110");
            var r2 = new Ring("1010011");
            var r3 = new Ring("1010001");

            Assert.True(r1.Equals(r2));
            Assert.False(r1.Equals(r3));
        }
    }
}
