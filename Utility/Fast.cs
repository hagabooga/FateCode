using Godot;
using static Godot.GD;

namespace Utility
{
    public static class Fast
    {
        public static string GenerateSalt()
        {
            Randomize();
            return Randi().ToString().SHA256Text();
        }

        public static string GenerateHashPassword(string password, string salt)
        {
            var rounds = ((int)Mathf.Pow(2, 18));
            for (int i = 0; i < rounds; i++)
            {
                password = (password + salt).SHA256Text();
            }
            return password;
        }
    }
}