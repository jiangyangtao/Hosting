using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using Yangtao.Hosting.Encryption.RsaAlgorithm;

namespace Test.RSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            //var rsaKey = RsaSecretKeyCreator.Generate(RSAExtensions.RSAKeyType.Pkcs1);
            //Console.WriteLine(rsaKey.PublicKey);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(rsaKey.PrivateKey);

            var publicKey = "MIIBCgKCAQEA0O9f4XijWg1rRYoHJBrpxhffqGI7xtXTtApZauEScB3xS1UmjouiaGKm5BcklDuicO7HoMxCnhC4dJDcgKfqOCObW4AU3Aun50sR0qXe0mb7F68xhkP4M1+req1ZRzg6k1ofkvPqy2tCgxXBU2Fnvv+YYTLKKo3QZn46nu6kzcsutypcPfSpUWngAENZt0p3CN82eZOGGLXJg9MO6vk1FL04FDfktrsVeh1o1lNAl6FZxD3o/S0Eg1OZezMnV/balNUFCDsEaSCjy5LbMUFtqT2aq4ff1e6aHaUin5ze5jLFEygkk2oREJ67n4m/lG4xqegzAebBvPKuBGoN2FJHXQIDAQAB";
            var privateKey = "MIIEoQIBAAKCAQEA0O9f4XijWg1rRYoHJBrpxhffqGI7xtXTtApZauEScB3xS1UmjouiaGKm5BcklDuicO7HoMxCnhC4dJDcgKfqOCObW4AU3Aun50sR0qXe0mb7F68xhkP4M1+req1ZRzg6k1ofkvPqy2tCgxXBU2Fnvv+YYTLKKo3QZn46nu6kzcsutypcPfSpUWngAENZt0p3CN82eZOGGLXJg9MO6vk1FL04FDfktrsVeh1o1lNAl6FZxD3o/S0Eg1OZezMnV/balNUFCDsEaSCjy5LbMUFtqT2aq4ff1e6aHaUin5ze5jLFEygkk2oREJ67n4m/lG4xqegzAebBvPKuBGoN2FJHXQIDAQABAoIBAQCmS4Ai82QAzuFsjbm1UP50Ppgza0xsq9A+YmZdHRsRxaNB9Fol5pmzP6HZtVnV6ckW3dZh83GqYWCO1qXKeNuBJ0YGA+GWamiPPT7ITGEXUgPUDAr89KheK21OzR5cAzMMCRVMX3unwI1FzFD9Tm/Go6Ti6Avk5s7SZPG/ge4GJJui0gSpfjaz0Auqat9lW8vGwoLEXgxWh5avYyIE0OO3Ir82zunMUQs5nVWh474s47akl54jW9zk3OxyMPJ0Oq/wpEct9oNg5n3s0YaSE4YVxGGzHlper52fEk6Up+SnNMLd2+dl+zMT+2MQ8uMetVfoWRGShdk1/As2XEvHPw35AoGBAOJc8jaqO0C7OfaWicGLMSTCLLm/RLKLo8zAxgvLZm3LYAd6pK15I/fnRCYQw6kphkDgicO4CGMbZXB7gR9gYwcKQwvk426s/vajvgednL+QDh7z9viDo9HnxqQp0dz0SmvuKeFcPLRuQ1BeI/KZf/kKoCLVjCj5qpY+TPZqJvQTAoGBAOxKSlPosYGMK+3gdhp7kdxjbQ3dyYcEJLMe5WO/g4C45+vkn9NpAB1u16ee6QVaA4/QP3ldedQUicAOFooYoXWXA+nLjZpt+wdGmJdetRnJ3RCDiI2VFlD6LlNk/IVPqzntjRKwGiXHax5l2lm6Su+ilaVXsUeFJxZHqXkjBuTPAoGAV9kufxyNpk+C7Tn6+EvmpJde+C9Mn+YliZ7+vTEQ7WdSO1TTeCddWCY+gm9bH9lnquH5VSWky6GkOoUT1XN4uxC2eHU59ofY4ysk4pu+a3GiqUyQ+l1MRHgo1SkNsyxzfXOOeVFslbF9GkGOveXojmQbRamOnZBXLbu77p2/xFUCgYB7l1QPVYva7d1gW+KKcY3Wj67P7OznERS9F/D13otC/fRY30l1w9sKihqEk0rgwLdSNqOssGbzthZi/Ttu2flBz059sDmNx+7gXF2d9yyUo5BcfeAj5hI5ItKuTgkDFavC7ey3FKNolUkDKlUuT5G4i/rn89lHRm8uaSdMtDR74wJ/eBFZX9XqZJ5/rdDXIXeGqjEknqdO3tFSjWpVDI/2sWsE4IUeZMgeBrgRXBfFvQCHsCXolc4g1XajW0CNsJUAp9Vk+esvLyjHlWrAW+bPX5z/XuX4ytxzY5HxkJbuJu/k5Q+AJs+a3Pz39CsW5kQZDDKH/XvmMQLQzLeosQngSQ==";

            var data = "1111111111111111";

            var rsaPublic = new RsaPublic(publicKey);
            var r = rsaPublic.Encrypt(data);

            Console.WriteLine(r);

            var rsaPrivate = new RsaPrivate(privateKey);
            var signData = rsaPrivate.Decrypt(r);

            Console.WriteLine(signData);

         
     
        }
    }
}