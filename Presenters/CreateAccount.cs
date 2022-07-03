using Godot;
using System;
using static Godot.GD;
using Utility;

namespace Presenters
{
    public class CreateAccount : FailSuccessEventerWithTween
    {
        public event Action goBackToLoginPressed;

        private readonly Views.CreateAccount view;
        private readonly Models.CreateAccount model;


        public string Username => model.Username;
        public string Password => model.Password;
        public string IpAddress => model.IpAddress;

        public override Label Label => view.Result;

        public CreateAccount(Views.CreateAccount view, Models.CreateAccount model)
        {
            this.view = view;
            this.model = model;

            view.Username.Connect("text_changed", this, nameof(UsernameTextChanged));
            view.Password.Connect("text_changed", this, nameof(PasswordTextChanged));
            view.ConfirmPassword.Connect("text_changed", this, nameof(ConfirmPasswordTextChanged));
            view.CreateAccountButton.Connect("pressed", this, nameof(CreateAccountButtonPressed));
            view.GoBackToLogin.Connect("pressed", this, nameof(GoBackToLoginButtonPressed));

            AddShortPopupTweenToFailed();
        }

        public void SetVisible(bool yes) => view.Root.Visible = yes;


        private void GoBackToLoginButtonPressed()
        {
            goBackToLoginPressed?.Invoke();
        }

        private void ConfirmPasswordTextChanged(string text)
        {
            model.ConfirmPassword = text;
        }

        private void PasswordTextChanged(string text)
        {
            model.Password = text;
        }

        private void UsernameTextChanged(string text)
        {
            model.Username = text;
        }

        private void CreateAccountButtonPressed()
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
            else if (!model.IsConfirmPasswordValid(out result))
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