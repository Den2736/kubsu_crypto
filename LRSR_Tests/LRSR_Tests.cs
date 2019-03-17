using LRSR;
using System;
using Xunit;

namespace LRSR_Tests
{
    public class LRSR_Tests
    {
        [Fact]
        public void OutputTest()
        {
            var register = new LRSR.LRSR();

            var rExpected = new Ring("1001110");
            var rActual = new Ring(register.Output("1101"));

            Assert.True(rExpected.Equals(rActual));
        }

        [Fact]
        public void IrreducibleTest()
        {
            var register = new LRSR.LRSR();
            // TODO rename: it's NonPrimitive
            Assert.True(register.IsIrreducible("100101"));
            Assert.True(register.IsIrreducible("1101"));
            Assert.True(register.IsIrreducible("11001"));
            Assert.True(register.IsIrreducible("100011101"));
            Assert.False(register.IsIrreducible("100011011"));
        }
    }
}
