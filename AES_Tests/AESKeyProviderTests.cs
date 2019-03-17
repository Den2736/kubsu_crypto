using AES;
using System.Linq;
using Xunit;

namespace AES_Tests
{
    public class AESKeyProviderTests
    {
        [Fact]
        public void SessionKeysTest()
        {
            byte[] key = Enumerable.Range(1, 8).Select(x => (byte)255)
                .Concat(Enumerable.Range(1, 8).Select(x => (byte)0))
                .ToArray();

            var keyProvider = new AESKeyProvider(key);

            var round1Expected = new byte[] {
                0x9D, 0x9C, 0x9C, 0x9C,
                0x62, 0x63, 0x63, 0x63,
                0x62, 0x63, 0x63, 0x63,
                0x62, 0x63, 0x63, 0x63
            };
            var round1Actual = keyProvider.SessionKeys[1];

            var round2Expected = new byte[] {
                0x64, 0x36, 0x67, 0x67,
                0x06, 0x55, 0x04, 0x04,
                0x64, 0x36, 0x67, 0x67,
                0x06, 0x55, 0x04, 0x04
            };
            var round2Actual = keyProvider.SessionKeys[2];

            Assert.Equal(round1Expected, round1Actual);
            Assert.Equal(round2Expected, round2Actual);
        }
    }
}
