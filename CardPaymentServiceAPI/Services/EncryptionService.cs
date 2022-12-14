using CardPaymentServiceAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services
{
    public class EncryptionService : IEncryptionService
    {
        private IConfiguration _config;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public EncryptionService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> Encypt(string plaintext, string secretkey, string iv)
        {
            string response = "";
            try
            {
                using (Aes myAes = Aes.Create())
                {

                    myAes.Key = System.Text.Encoding.UTF8.GetBytes(secretkey);
                    myAes.IV = System.Text.Encoding.UTF8.GetBytes(iv);

                    // Encrypt the string to an array of bytes.                     
                    byte[] encrypted = EncryptStringToBytes_Aes(plaintext, myAes.Key, myAes.IV);

                    response = ByteArrayToString(encrypted);
                }
            }
            catch (Exception ex)
            {

                Logger.Error($"Error| Exception Error encryptng data with Error details: \n {ex}\n");
            }
            return response;
        }
        public async Task<string> Encypt(Guid plaintext, string secretkey, string iv)
        {
            string response = "";
            try
            {
                using (Aes myAes = Aes.Create())
                {

                    myAes.Key = System.Text.Encoding.UTF8.GetBytes(secretkey);
                    myAes.IV = System.Text.Encoding.UTF8.GetBytes(iv);

                    // Encrypt the string to an array of bytes.                     
                    byte[] encrypted = EncryptStringToBytes_Aes(plaintext.ToString(), myAes.Key, myAes.IV);

                    response = ByteArrayToString(encrypted);
                }
            }
            catch (Exception ex)
            {

                Logger.Error($"Error| Exception Error encryptng data with Error details: \n {ex}\n");
            }
            return response;
        }

        public async Task<string> Decrypt(string ciphertext, string secretKey, string iv)
        {
            string response = "";
            try
            {  // Create a new instance of the Aes    
               // class.  This generates a new key and initialization                 
               // vector (IV).                 
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key = System.Text.Encoding.UTF8.GetBytes(secretKey);
                    myAes.IV = System.Text.Encoding.UTF8.GetBytes(iv);

                    // Decrypt the bytes to a string. 
                    response = DecryptStringFromBytes_Aes(ciphertext, myAes.Key, myAes.IV);
                }
            }
            catch (Exception ex)
            {

                Logger.Error($"Error| Exception Error decryptng data with Error details: \n {ex}\n");
            }

            return response;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba) hex.AppendFormat("{0:x2}", b); return hex.ToString();
        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {             // Check arguments.             
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // with the specified key and IV.             
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create an encryptor to perform the stream transform.                 
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for encryption.                 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.     
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.            
            return encrypted;

        }

        private static string DecryptStringFromBytes_Aes(string cipherText, byte[] Key, byte[] IV)
        {             // Check arguments.             
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold             
            // the decrypted text.             
            string plaintext = null;

            // Create an Aes object             
            // with the specified key and IV.             
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] cipherbytes = HexadecimalStringToByteArray(cipherText);

                // Create the streams used for decryption.                 
                using (MemoryStream msDecrypt = new MemoryStream(cipherbytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream                             
                            // and place them in a string.                             
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        private static byte[] HexadecimalStringToByteArray(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return output;
        }


    }
}
