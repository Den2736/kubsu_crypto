using System;
using System.Collections.Generic;
using System.Text;

namespace AES
{
    partial class AES
    {
        private int RoundsCount => 4;
    }

    partial class AESKeyProvider
    {
        private int RoundsCount => 4;
    }
}
