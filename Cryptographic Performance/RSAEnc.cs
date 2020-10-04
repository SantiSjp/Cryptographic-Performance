using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cryptographic_Performance
{
    public class RSAEnc
    {
        public int EncBits { get; set; }

        public RSAEnc(int encBits)
        {
            EncBits = encBits;
        }

        public string getPublicKey()
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(EncBits);
            var publicKey = cryptoServiceProvider.ExportParameters(false);
            string publicKeyString = GetKeyString(publicKey);

            return publicKeyString;
        }

        public string getPrivateKey()
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(EncBits);
            var privateKey = cryptoServiceProvider.ExportParameters(true);
            string privateKeyString = GetKeyString(privateKey);

            return privateKeyString;
        }


        public string Encrypt(string textToEncrypt, string keyString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(keyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }


        public string Decrypt(string textToDecrypt, string keyString)
        {
            var bytesToDescrypt = Encoding.UTF8.GetBytes(textToDecrypt);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {

                    // server decrypting data with private key                    
                    rsa.FromXmlString(keyString);

                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }


        public string GetKeyString(RSAParameters Key)
        {

            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, Key);
            return stringWriter.ToString();
        }

    }
}
