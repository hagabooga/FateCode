using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Models
{
    public class CreateAccount : Login
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