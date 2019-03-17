using System;
using System.Collections.Generic;
using System.Linq;

namespace AES
{
    public static class SubBytesTransformationTable
    {
        private static readonly Dictionary<byte, Dictionary<byte, byte>> _data;

        static SubBytesTransformationTable()
        {
            _data = new Dictionary<byte, Dictionary<byte, byte>>
            {
                [0] = new Dictionary<byte, byte>()
                {
                    [0] = 0x63,
                    [1] = 0x7C,
                    [2] = 0x77,
                    [3] = 0x7B,
                    [4] = 0xF2,
                    [5] = 0x6B,
                    [6] = 0x6F,
                    [7] = 0xC5,
                    [8] = 0x30,
                    [9] = 0x01,
                    [0xA] = 0x67,
                    [0xB] = 0x2B,
                    [0xC] = 0xFE,
                    [0xD] = 0xD7,
                    [0xE] = 0xAB,
                    [0xF] = 0x76
                },
                [1] = new Dictionary<byte, byte>()
                {
                    [0] = 0xCA,
                    [1] = 0x82,
                    [2] = 0xC9,
                    [3] = 0x7D,
                    [4] = 0xFA,
                    [5] = 0x59,
                    [6] = 0x47,
                    [7] = 0xF0,
                    [8] = 0xAD,
                    [9] = 0xD4,
                    [0xA] = 0xA2,
                    [0xB] = 0xAF,
                    [0xC] = 0x9C,
                    [0xD] = 0xA4,
                    [0xE] = 0x72,
                    [0xF] = 0xC0
                },
                [2] = new Dictionary<byte, byte>()
                {
                    [0] = 0xB7,
                    [1] = 0xFD,
                    [2] = 0x93,
                    [3] = 0x26,
                    [4] = 0x36,
                    [5] = 0x3F,
                    [6] = 0xF7,
                    [7] = 0xCC,
                    [8] = 0x34,
                    [9] = 0xA5,
                    [0xA] = 0xE5,
                    [0xB] = 0xF1,
                    [0xC] = 0x71,
                    [0xD] = 0xD8,
                    [0xE] = 0x31,
                    [0xF] = 0x15
                },
                [3] = new Dictionary<byte, byte>()
                {
                    [0] = 0x04,
                    [1] = 0xC7,
                    [2] = 0x23,
                    [3] = 0xC3,
                    [4] = 0x18,
                    [5] = 0x96,
                    [6] = 0x05,
                    [7] = 0x9A,
                    [8] = 0x07,
                    [9] = 0x12,
                    [0xA] = 0x80,
                    [0xB] = 0xE2,
                    [0xC] = 0xEB,
                    [0xD] = 0x27,
                    [0xE] = 0xB2,
                    [0xF] = 0x75
                },
                [4] = new Dictionary<byte, byte>()
                {
                    [0] = 0x09,
                    [1] = 0x83,
                    [2] = 0x2C,
                    [3] = 0x1A,
                    [4] = 0x1B,
                    [5] = 0x6E,
                    [6] = 0x5A,
                    [7] = 0xA0,
                    [8] = 0x52,
                    [9] = 0x3B,
                    [0xA] = 0xD6,
                    [0xB] = 0xB3,
                    [0xC] = 0x29,
                    [0xD] = 0xE3,
                    [0xE] = 0x2F,
                    [0xF] = 0x84
                },
                [5] = new Dictionary<byte, byte>()
                {
                    [0] = 0x53,
                    [1] = 0xD1,
                    [2] = 0x00,
                    [3] = 0xED,
                    [4] = 0x20,
                    [5] = 0xFC,
                    [6] = 0xB1,
                    [7] = 0x5B,
                    [8] = 0x6A,
                    [9] = 0xCB,
                    [0xA] = 0xBE,
                    [0xB] = 0x39,
                    [0xC] = 0x4A,
                    [0xD] = 0x4C,
                    [0xE] = 0x58,
                    [0xF] = 0xCF
                },
                [6] = new Dictionary<byte, byte>()
                {
                    [0] = 0xD0,
                    [1] = 0xEF,
                    [2] = 0xAA,
                    [3] = 0xFB,
                    [4] = 0x43,
                    [5] = 0x4D,
                    [6] = 0x33,
                    [7] = 0x85,
                    [8] = 0x45,
                    [9] = 0xF9,
                    [0xA] = 0x02,
                    [0xB] = 0x7F,
                    [0xC] = 0x50,
                    [0xD] = 0x3C,
                    [0xE] = 0x9F,
                    [0xF] = 0xA8
                },
                [7] = new Dictionary<byte, byte>()
                {
                    [0] = 0x51,
                    [1] = 0xA3,
                    [2] = 0x40,
                    [3] = 0x8F,
                    [4] = 0x92,
                    [5] = 0x9D,
                    [6] = 0x38,
                    [7] = 0xF5,
                    [8] = 0xBC,
                    [9] = 0xB6,
                    [0xA] = 0xDA,
                    [0xB] = 0x21,
                    [0xC] = 0x10,
                    [0xD] = 0xFF,
                    [0xE] = 0xF3,
                    [0xF] = 0xD2
                },
                [8] = new Dictionary<byte, byte>()
                {
                    [0] = 0xCD,
                    [1] = 0x0C,
                    [2] = 0x13,
                    [3] = 0xEC,
                    [4] = 0x5F,
                    [5] = 0x97,
                    [6] = 0x44,
                    [7] = 0x17,
                    [8] = 0xC3,
                    [9] = 0xA7,
                    [0xA] = 0x7E,
                    [0xB] = 0x3D,
                    [0xC] = 0x64,
                    [0xD] = 0x5D,
                    [0xE] = 0x19,
                    [0xF] = 0x73
                },
                [9] = new Dictionary<byte, byte>()
                {
                    [0] = 0x60,
                    [1] = 0x81,
                    [2] = 0x4F,
                    [3] = 0xDC,
                    [4] = 0x22,
                    [5] = 0x2A,
                    [6] = 0x90,
                    [7] = 0x88,
                    [8] = 0x46,
                    [9] = 0xEE,
                    [0xA] = 0xB8,
                    [0xB] = 0x14,
                    [0xC] = 0xDE,
                    [0xD] = 0x5E,
                    [0xE] = 0x0B,
                    [0xF] = 0xDE
                },
                [0xA] = new Dictionary<byte, byte>()
                {
                    [0] = 0xE0,
                    [1] = 0x32,
                    [2] = 0x3A,
                    [3] = 0x0A,
                    [4] = 0x49,
                    [5] = 0x06,
                    [6] = 0x24,
                    [7] = 0x5C,
                    [8] = 0xC2,
                    [9] = 0xD3,
                    [0xA] = 0xAC,
                    [0xB] = 0x62,
                    [0xC] = 0x91,
                    [0xD] = 0x95,
                    [0xE] = 0xE4,
                    [0xF] = 0x79
                },
                [0xB] = new Dictionary<byte, byte>()
                {
                    [0] = 0xE7,
                    [1] = 0xCB,
                    [2] = 0x37,
                    [3] = 0x6D,
                    [4] = 0x8D,
                    [5] = 0xD5,
                    [6] = 0x4E,
                    [7] = 0xA9,
                    [8] = 0x6C,
                    [9] = 0x56,
                    [0xA] = 0xF4,
                    [0xB] = 0xEA,
                    [0xC] = 0x65,
                    [0xD] = 0x7A,
                    [0xE] = 0xAE,
                    [0xF] = 0x08
                },
                [0xC] = new Dictionary<byte, byte>()
                {
                    [0] = 0xBA,
                    [1] = 0x78,
                    [2] = 0x25,
                    [3] = 0x2E,
                    [4] = 0x1C,
                    [5] = 0xA6,
                    [6] = 0xB4,
                    [7] = 0xC6,
                    [8] = 0xE8,
                    [9] = 0xDD,
                    [0xA] = 0x74,
                    [0xB] = 0x1F,
                    [0xC] = 0x4B,
                    [0xD] = 0xBD,
                    [0xE] = 0x8B,
                    [0xF] = 0x8A
                },
                [0xD] = new Dictionary<byte, byte>()
                {
                    [0] = 0x70,
                    [1] = 0x3E,
                    [2] = 0xB5,
                    [3] = 0x66,
                    [4] = 0x48,
                    [5] = 0x03,
                    [6] = 0xF7,
                    [7] = 0x0E,
                    [8] = 0x61,
                    [9] = 0x35,
                    [0xA] = 0x57,
                    [0xB] = 0xB9,
                    [0xC] = 0x86,
                    [0xD] = 0xC1,
                    [0xE] = 0x1D,
                    [0xF] = 0x9E
                },
                [0xE] = new Dictionary<byte, byte>()
                {
                    [0] = 0xE1,
                    [1] = 0xF8,
                    [2] = 0x98,
                    [3] = 0x11,
                    [4] = 0x69,
                    [5] = 0xD9,
                    [6] = 0x8E,
                    [7] = 0x94,
                    [8] = 0x9B,
                    [9] = 0x1E,
                    [0xA] = 0x87,
                    [0xB] = 0xE9,
                    [0xC] = 0xCE,
                    [0xD] = 0x55,
                    [0xE] = 0x28,
                    [0xF] = 0xDF
                },
                [0xF] = new Dictionary<byte, byte>()
                {
                    [0] = 0x8C,
                    [1] = 0xA1,
                    [2] = 0x89,
                    [3] = 0x0D,
                    [4] = 0xBF,
                    [5] = 0xE6,
                    [6] = 0x42,
                    [7] = 0x68,
                    [8] = 0x41,
                    [9] = 0x99,
                    [0xA] = 0x2D,
                    [0xB] = 0x0F,
                    [0xC] = 0xB0,
                    [0xD] = 0x54,
                    [0xE] = 0xBB,
                    [0xF] = 0x16
                }
            };
        }

        public static byte TransformationFor(byte b)
        {
            string inBase16 = Convert.ToString(b, 16);
            if (inBase16.Length == 1) inBase16 = new string(inBase16.Prepend('0').ToArray());

            byte i = Convert.ToByte(inBase16.First().ToString(), 16);
            byte j = Convert.ToByte(inBase16.Last().ToString(), 16);

            return _data[i][j];
        }

        public static byte InverseTransformationFor(byte b)
        {
            for (byte i = 0; i < _data.Count; i++)
            {
                for (byte j = 0; j < _data.Count; j++)
                {
                    if (_data[i][j] == b)
                    {
                        return Convert.ToByte(Convert.ToString(i, 16) + Convert.ToString(j, 16), 16);
                    }
                }
            }

            return 0;
        }
    }
}
