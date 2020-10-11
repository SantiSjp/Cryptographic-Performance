using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Cryptographic_Performance
{
    class Program
    {
  
        static void Main()
        {
            try
             {
                /* ---------------------------------------------------------------------------
                                            ANALISE EM RSA 
                   ---------------------------------------------------------------------------
                */

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var rsa = new RSAEnc(4096);
                string texto = "RSA e um algoritmo que deve o seu nome a tres professores do MIT: Ronald Rivest, Adi Shamir e Leonard Adleman";


                var publicKey = rsa.getPublicKey();
                var privateKey = rsa.getPrivateKey();

                var textoCifrado = rsa.Encrypt(texto, publicKey);

                Console.WriteLine("Texto a ser cifrado em RSA: ");
                Console.WriteLine(texto);
                Console.WriteLine("------------------------------------------");

                Console.WriteLine("Texto cifrado em RSA: ");
                Console.WriteLine(textoCifrado);
                Console.WriteLine("------------------------------------------");

                stopwatch.Stop();

                Console.WriteLine($"Tempo passado do RSA: {stopwatch.Elapsed}");

                var stopwatch2 = new Stopwatch();
                stopwatch2.Start();

                
                /* ---------------------------------------------------------------------------
                                            ANALISE EM AES 
                   ---------------------------------------------------------------------------
                */

                //string original = "Here is some data to encrypt!";
                string texto2 = "RSA e um algoritmo que deve o seu nome a tres professores do MIT: Ronald Rivest, Adi Shamir e Leonard Adleman";

                var aes = new AESEnc();

                // Create a new instance of the Aes 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (var random = new RNGCryptoServiceProvider())
                {
                    var key = new byte[32];
                    random.GetBytes(key);

                    // Encrypt the string to an array of bytes. 
                    byte[] encrypted = aes.EncryptStringToBytes_Aes(texto2, key);

                    // Decrypt the bytes to a string. 
                    string roundtrip = aes.DecryptStringFromBytes_Aes(encrypted, key);

                    //Display the original data and the decrypted data.                   
                    Console.WriteLine("Texto a ser cifrado em AES: ");
                    Console.WriteLine(texto2);
                    Console.WriteLine("------------------------------------------");

                    Console.WriteLine("Texto cifrado em AES: ");
                    Console.WriteLine(Convert.ToBase64String(encrypted));
                    Console.WriteLine("------------------------------------------");

                    stopwatch2.Stop();

                    Console.WriteLine($"Tempo passado do AES: {stopwatch2.Elapsed}");

                }

             }
             catch (Exception e)
             {
                 Console.WriteLine("Error: {0}", e.Message);
             }

        }

    }
}
