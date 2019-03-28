using System;

using DiffieHellman;

using Xunit;

namespace DiffieHellmanTests {
    public class UnitTest1 {
        [Fact]
        public void KeyExchangeTest () {
            var alice = new DiffieHellmanParticipant ("Alice");
            var bob = new DiffieHellmanParticipant ("Bob");

            using (var dh = new DiffieHellmanAlgorithm()){
                dh.KeyExchange(alice, bob);
            }

            Assert.True(alice.Trusts(bob));
        }
    }
}
