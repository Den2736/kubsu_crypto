using System;
using PrimeGenerator = PrimeNumberGenerator.PrimeNumberGenerator;

namespace DiffieHellman {
    public class DiffieHellmanAlgorithm : IDisposable {

        public void KeyExchange (DiffieHellmanParticipant alice, DiffieHellmanParticipant bob) {
            
            alice.Base = new Random().Next(0, int.MaxValue);
            alice.Mod = new PrimeGenerator().Generate();
            alice.ShareGeneratorsWith(bob);

            alice.GenerateSecret();
            bob.GenerateSecret();

            alice.Mix();
            bob.Mix();

            alice.ShareMixWith(bob);
            bob.ShareMixWith(alice);

            alice.GenerateSharedKey();
            bob.GenerateSharedKey();
        }

        public void Dispose () { }
    }
}
