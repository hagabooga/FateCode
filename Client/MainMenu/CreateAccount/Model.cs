using Godot;
using System;
using static Godot.GD;
using Utility;

namespace CreateAccount
{
    public class Model : Login.Model
    {
        public string ConfirmPassword { get; set; }

        public bool IsConfirmPasswordValid(out string result)
        {
            bool ok = true;
            result = "";
            if (ConfirmPassword.IsNullOrEmpty())
            {
                result = "Please confirm your password!";
                ok = false;
            }
            else if (ConfirmPassword != Password)
            {
                result = "Passwords do not match!";
                ok = false;
            }
            return ok;
        }
    }
}