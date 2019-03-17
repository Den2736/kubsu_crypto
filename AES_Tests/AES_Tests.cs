using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace AES_Tests
{
    public class AES_Tests
    {
        [Fact]
        public void Test()
        {
            byte[] key = Enumerable.Range(1, 8).Select(x => (byte)255)
                .Concat(Enumerable.Range(1, 8).Select(x => (byte)0))
                .ToArray();

            byte[] data = new byte[]
            {
                0x80, 0x80, 0x80, 0x80,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            };

            var aes = new AES.AES(key);
            var encrypted = aes.Encrypt(data);

            Assert.Equal(data, aes.Decrypt(encrypted));
        }
    }
}
