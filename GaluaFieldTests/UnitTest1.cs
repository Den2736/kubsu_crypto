using GaluaField;
using System;
using Xunit;

namespace GaluaFieldTests
{
    public class UnitTest1
    {
        [Fact]
        public void ModTest()
        {
            var gf = new GF("1011");

            Assert.Equal("101", gf.Mod("11000"));
            Assert.Equal("110", gf.Mod("10000"));
            Assert.Equal("11", gf.Mod("1000"));
            Assert.Equal("100", gf.Mod("100"));
        }

        [Fact]
        public void SumTest()
        {
            var gf = new GF("1011");

            Assert.Equal("1", gf.Add("10", "11"));
            Assert.Equal("110", gf.Add("11011", "10110"));
        }

        [Fact]
        public void MultTest()
        {
            var gf = new GF("1011");

            Assert.Equal("10", gf.Mult("111", "11"));
            Assert.Equal("101", gf.Mult("111", "10"));
            Assert.Equal("110", gf.Mult("100", "100"));
        }
    }
}
