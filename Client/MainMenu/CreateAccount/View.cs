using Godot;
using System;
using Utility;

namespace CreateAccount
{
    public class View : EzPrefab
    {
        public readonly Control Root;
        public readonly Label Title, Result;
        public readonly LineEdit Username, Password, ConfirmPassword, IpAddress;
        public readonly Button CreateAccount, GoBackToLogin;
        public readonly VBoxContainer Content;
    }
}