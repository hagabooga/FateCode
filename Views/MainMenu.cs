using Godot;
using System;
using Utility;
using static Godot.GD;

namespace Views
{
    public class MainMenu : EzPrefab
    {
        public readonly Views.Login LoginView;
        public readonly Views.CreateAccount CreateAccountView;

        public override void _Ready()
        {
            base._Ready();
            CreateAccountView.Root.Visible = false;
        }
    }
}