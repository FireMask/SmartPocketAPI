using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace SmartPocketAPI.Auth;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Generate a random salt
        var salt = GenerateSalt(16); // 16 bytes
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

        // Configure Argon2 parameters
        argon2.Salt = salt;
        argon2.DegreeOfParallelism = 8; // Number of threads
        argon2.MemorySize = 65536;     // 64 MB of memory
        argon2.Iterations = 4;         // Number of iterations

        // Generate the hash
        var hash = argon2.GetBytes(32); // 32-byte hash

        // Return the hash and salt as a Base64 string
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Split the hashed password into salt and hash
        var parts = hashedPassword.Split(':');
        var salt = Convert.FromBase64String(parts[0]);
        var originalHash = Convert.FromBase64String(parts[1]);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 8,
            MemorySize = 65536,
            Iterations = 4
        };

        var newHash = argon2.GetBytes(32);

        // Compare the hashes
        return CryptographicEquals(originalHash, newHash);
    }

    private static byte[] GenerateSalt(int size)
    {
        var salt = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    private static bool CryptographicEquals(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;

        var result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result |= a[i] ^ b[i];
        }
        return result == 0;
    }
}
