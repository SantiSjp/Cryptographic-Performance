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


        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        static void Main()
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var rsa = new RSAEnc(2048);
            string texto = "RSA e um algoritmo que deve o seu nome a tres professores do MIT: Ronald Rivest, Adi Shamir e Leonard Adleman";

            Console.WriteLine(texto.Length);

            
            var publicKey = rsa.getPublicKey();
            var privateKey = rsa.getPrivateKey();

            var textoCifrado = rsa.Encrypt(texto, publicKey);

            Console.WriteLine("Texto a ser cifrado: ");
            Console.WriteLine(texto);
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Texto cifrado: ");
            Console.WriteLine(textoCifrado);
            Console.WriteLine("------------------------------------------");                   


            stopwatch.Stop();

            Console.WriteLine($"Tempo passado: {stopwatch.Elapsed}");
        }

    }
}
