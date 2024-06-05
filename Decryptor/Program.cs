using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class DecryptProgram
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: DecryptProgram <encryptedFolderPath> <keyFilePath>");
            return;
        }

        string encryptedFolderPath = args[0];
        string keyFilePath = args[1];

        if (!Directory.Exists(encryptedFolderPath))
        {
            Console.WriteLine($"Error: The directory '{encryptedFolderPath}' does not exist.");
            return;
        }

        if (!File.Exists(keyFilePath))
        {
            Console.WriteLine($"Error: The key file '{keyFilePath}' does not exist.");
            return;
        }

        byte[] key = File.ReadAllBytes(keyFilePath);

        string[] encryptedFiles = Directory.GetFiles(encryptedFolderPath, "*.ratus");

        foreach (string encryptedFilePath in encryptedFiles)
        {
            string decryptedFilePath = Path.Combine(encryptedFolderPath, Path.GetFileNameWithoutExtension(encryptedFilePath));
            DecryptFile(encryptedFilePath, decryptedFilePath, key);
        }

        Console.WriteLine("All files decrypted successfully.");
    }

    static void DecryptFile(string inputFile, string outputFile, byte[] key)
    {
        using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open))
        using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
        using (Aes aes = Aes.Create())
        {
            byte[] iv = new byte[aes.BlockSize / 8];
            inputFileStream.Read(iv, 0, iv.Length);

            aes.Key = key;
            aes.IV = iv;

            using (CryptoStream cryptoStream = new CryptoStream(
                inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(outputFileStream);
            }
        }

        Console.WriteLine("Decrypted " + inputFile);
    }
}
