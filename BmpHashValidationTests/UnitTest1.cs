using System;
using System.IO;
using System.Threading.Tasks;

using BmpHashValidation;

using Xunit;

namespace BmpHashValidationTests {
    public class UnitTest1 {
        [Fact]
        public void ValidExpected () {
            string bmpPath = "test.bmp";
            using (var validator = new BmpHashValidator ()) {
                validator.WriteHashInto (bmpPath);
                Assert.True (validator.HashIsValid (bmpPath));
            }
        }

        [Fact]
        public async Task InvalidExpected () {
            string bmpPath = "test.bmp";
            using (var validator = new BmpHashValidator ()) {
                validator.WriteHashInto (bmpPath);

                var data = await File.ReadAllBytesAsync (bmpPath);
                data[54] = (byte) (~ data[54]);
                await File.WriteAllBytesAsync (bmpPath, data);

                Assert.False (validator.HashIsValid (bmpPath));
            }
        }
    }
}
