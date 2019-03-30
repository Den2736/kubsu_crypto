using System;
using System.IO;
using System.Threading.Tasks;

using BmpHashValidation;

using Xunit;

namespace BmpHashValidationTests {
    public class UnitTest1 {
        [Fact]
        public void ValidExpected() {
            string bmpPath = "test.bmp";
            string hashedbmpPath = "hashed.bmp";
            using(var validator = new BmpHashValidator()) {
                validator.WriteHash(bmpPath, hashedbmpPath);
                Assert.True(validator.HashIsValid(hashedbmpPath));
            }
        }

        [Fact]
        public void Check() {
            string hashedbmpPath = "hashed.bmp";
            using(var validator = new BmpHashValidator()) {
                Assert.True(validator.HashIsValid(hashedbmpPath));
            }
        }
    }
}
