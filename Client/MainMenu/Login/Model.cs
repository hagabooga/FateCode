using Godot;
using System;
using static Godot.GD;
using Utility;


namespace Login
{
    public class Model
    {
        private const int MinimumPasswordLength = 7;
        private string _ipAddress;

        public string Username { get; set; }
        public string Password { get; set; }
        public string IpAddress
        {
            get => _ipAddress.IsNullOrEmpty() ? "localhost" : _ipAddress;
            set => _ipAddress = value;
        }

        public bool IsValidUsername(out string result)
        {
            bool ok = true;
            result = "";
            if (Username.IsNullOrEmpty())
            {
                ok = false;
                result = "Please provide a valid username!";
            }
            return ok;
        }

        public bool IsValidPassword(out string result)
        {
            bool ok = true;
            result = "";
            if (Password.IsNullOrEmpty())
            {
                ok = false;
                result = "Please provide a valid password!";
            }
            else if (Password.Length < MinimumPasswordLength)
            {
                ok = false;
                result = "Password must contain at least 7 characters.";
            }
            return ok;
        }

        public bool IsValidIpAddress(out string result)
        {
            bool ok = true;
            result = "";
            if (!IpAddress.IsValidIPAddress())
            {
                if (IpAddress == "localhost")
                {
                    return ok;
                }
                ok = false;
                result = "Please enter a valid IP address!";
            }
            return ok;
        }
    }
}