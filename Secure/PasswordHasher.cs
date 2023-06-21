using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    private const int SaltSize = 16; // Size of the salt in bytes
    private const int HashSize = 32; // Size of the hash in bytes
    private const int Iterations = 10000; // Number of iterations for the key derivation function

    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] hash;

        using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
        {
            salt = deriveBytes.Salt;
            hash = deriveBytes.GetBytes(HashSize);
        }

        // Combine the salt and hash into a single string
        byte[] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        // Convert the combined salt and hash to a Base64-encoded string
        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Convert the Base64-encoded string back to a byte array
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        // Extract the salt from the byte array
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // Compute the hash of the password using the extracted salt
        using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
        {
            byte[] hash = deriveBytes.GetBytes(HashSize);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false; // Passwords don't match
                }
            }
        }

        return true; // Passwords match
    }
}
