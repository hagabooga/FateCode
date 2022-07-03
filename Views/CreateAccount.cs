using Godot;
using System;
using Utility;

namespace Views
{
    public class CreateAccount : EzPrefab
    {
        public readonly Control Root;
        public readonly Label Title, Result;
        public readonly LineEdit Username, Password, ConfirmPassword, IpAddress;
        public readonly Button CreateAccountButton, GoBackToLogin;
        public readonly VBoxContainer Content;
    }
}