using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DiffieHellman {
    public class DiffieHellmanParticipant {
        public string Name { get; set; }
        public int Base { get; set; }
        public uint Mod { get; set; }

        private int _secret { get; set; }
        private int _mix { get; set; }
        public int PartnerMix { get; set; }
        private int _sharedKey { get; set; }

        public DiffieHellmanParticipant (string name) => Name = name;

        public void ShareGeneratorsWith (DiffieHellmanParticipant partner) {
            partner.Base = Base;
            partner.Mod = Mod;
        }

        public void GenerateSecret () {
            _secret = new Random ().Next (0, int.MaxValue);
        }

        public void Mix () {
            _mix = (int) (Math.Pow (Base, _secret) % Mod);
        }

        public void ShareMixWith (DiffieHellmanParticipant partner) {
            partner.PartnerMix = _mix;
        }

        public void GenerateSharedKey () {
            _sharedKey = (int) (Math.Pow (PartnerMix, _secret) % Mod);
            Log($"SharedKey = {_sharedKey}");
        }

        public bool Trusts (DiffieHellmanParticipant partner) {
            return Enumerable.SequenceEqual (GetSharedSecretHash (), partner.GetSharedSecretHash ());
        }
        public byte[] GetSharedSecretHash () {
            using (var md5 = MD5.Create ()) {
                return md5.ComputeHash (BitConverter.GetBytes (_sharedKey));
            }
        }
        private void Log (string message) {
            File.AppendAllText ($"{Name}.log", $"{DateTime.Now}: {message}");
        }
    }
}
