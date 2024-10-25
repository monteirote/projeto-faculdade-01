using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Projeto01
{
    public class Settings {

        public static string Secret = "dfDF43$43982392394329843433d33f333434fetr453454fef2934b23929348fdf_23FDfdf$D#d43";

        public static string GenerateHash(string password) {

            byte[] salt = Encoding.ASCII.GetBytes("123468973165");

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return hashed;
        }
    }
}
