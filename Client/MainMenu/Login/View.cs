using Godot;
using System;
using static Godot.GD;
using Utility;


namespace Login
{
    public class View : EzPrefab
    {
        public readonly Control Root;
        public readonly Label Title, Result;
        public readonly LineEdit Username, Password, IpAddress;
        public readonly Button Login, SignUp;
        public readonly VBoxContainer Content;

        public void Clear()
        {
            Login.Text = "";
            Password.Text = "";
        }
    }
}