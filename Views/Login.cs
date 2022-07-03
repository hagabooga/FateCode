using Godot;
using System;
using static Godot.GD;
using Utility;


namespace Views
{
    public class Login : EzPrefab
    {
        public readonly Control Root;
        public readonly Label Title, Result;
        public readonly LineEdit Username, Password, IpAddress;
        public readonly Button LoginButton, SignUp;
        public readonly VBoxContainer Content;

        public void Clear()
        {
            LoginButton.Text = "";
            Password.Text = "";
        }
    }
}