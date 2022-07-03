using Godot;
using System;
using Utility;
using static Godot.GD;

namespace MainMenu
{
    public class View : EzPrefab
    {
        public readonly Login.View LoginView;
        public readonly CreateAccount.View CreateAccountView;

        public override void _Ready()
        {
            base._Ready();
            CreateAccountView.Root.Visible = false;
        }
    }
}