using System;

namespace PrimeNumberGenerator {
    public class PrimeNumberGenerator {
        private readonly Random _random = new Random ();

        public uint Generate () {
            var numberBytes = new byte[4];
            _random.NextBytes (numberBytes);
            uint number = BitConverter.ToUInt32 (numberBytes, 0);

            while (!IsPrime (number)) {
                unchecked {
                    number++;
                }
            }
            return number;
        }

        public static bool IsPrime (uint number) {
            if ((number & 1) == 0) return (number == 2);

            var limit = (uint) Math.Sqrt (number);
            for (uint i = 3; i <= limit; i += 2) {
                if ((number % i) == 0) return false;
            }
            return true;
        }
    }
}
