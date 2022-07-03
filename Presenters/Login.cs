using Godot;
using System;
using static Godot.GD;
using Utility;
using System.Collections.Generic;

namespace Presenters
{
    public class Login : FailSuccessEventerWithTween
    {
        public event Action signUpPressed;

        private readonly Views.Login view;
        private readonly Models.Login model;

        public string Username => model.Username;
        public string Password => model.Password;
        public string IpAddress => model.IpAddress;

        public override Label Label => view.Result;

        public Login(Views.Login view, Models.Login model)
        {
            this.view = view;
            this.model = model;

            view.Username.Connect("text_changed", this, nameof(UsernameTextChanged));
            view.Password.Connect("text_changed", this, nameof(PasswordTextChanged));
            view.IpAddress.Connect("text_changed", this, nameof(IpAddressTextChanged));
            view.LoginButton.Connect("pressed", this, nameof(LoginButtonPressed));
            view.SignUp.Connect("pressed", this, nameof(SignUpButtonPressed));

            AddShortPopupTweenToFailed();
        }


        public override void _Ready()
        {
            base._Ready();
        }

        public void SetVisible(bool yes) => view.Root.Visible = yes;

        private void IpAddressTextChanged(string text)
        {
            model.IpAddress = text;
        }

        private void SignUpButtonPressed()
        {
            signUpPressed?.Invoke();
        }

        private void PasswordTextChanged(string text)
        {
            model.Password = text;
        }

        private void UsernameTextChanged(string text)
        {
            model.Username = text;
        }

        private void LoginButtonPressed()
        {
            string result;
            if (!model.IsValidUsername(out result))
            {
                InvokeFailed(result);
            }
            else if (!model.IsValidPassword(out result))
            {
                InvokeFailed(result);
            }
            else if (!model.IsValidIpAddress(out result))
            {
                InvokeFailed(result);
            }
            else
            {
                InvokeSuccess();
            }
        }
    }
}