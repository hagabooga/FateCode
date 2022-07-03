using Godot;
using System;
using Utility;
using static Godot.GD;

namespace MainMenu
{
    public class Controller : EzNode
    {
        readonly Login.Presenter loginPresenter;
        readonly CreateAccount.Presenter createAccountPresenter;
        readonly Gateway gateway;

        public Controller(Login.Presenter loginPresenter,
                         CreateAccount.Presenter createAccountPresenter,
                         Gateway gateway)
        {
            this.loginPresenter = loginPresenter;
            this.createAccountPresenter = createAccountPresenter;
            this.gateway = gateway;

            SetupLogin();
            SetupCreateAccount();
        }


        private void SetupLogin()
        {
            loginPresenter.success += () =>
            {
                gateway.RequestLoginRequest(loginPresenter.Username,
                                            loginPresenter.Password,
                                            loginPresenter.IpAddress);
            };

            loginPresenter.signUpPressed += () =>
            {
                loginPresenter.SetVisible(false);
                createAccountPresenter.SetVisible(true);
            };

            gateway.receivedLoginRequest += (result, token) =>
            {
                Print($"Received login request: {result}, {token}.");
                if (result != Error.Ok)
                {
                    loginPresenter.DisplayShortPopupTween("Incorrect username or password!");
                }
                else
                {
                    // ACTUAL LOGIN HERE
                }
            };
        }

        private void SetupCreateAccount()
        {
            createAccountPresenter.success += () =>
            {
                Print("Requesting new account.");
                gateway.RequestCreateAccount(createAccountPresenter.Username,
                                             createAccountPresenter.Password,
                                             createAccountPresenter.IpAddress);
            };

            createAccountPresenter.goBackToLoginPressed += () =>
            {
                loginPresenter.SetVisible(true);
                createAccountPresenter.SetVisible(false);
            };

            gateway.receivedCreateAccountRequest += (result) =>
            {
                Print($"Received create account request: {result}.");
                if (result != Error.Ok)
                {
                    createAccountPresenter.DisplayShortPopupTween($"Error: {result}");
                }
                else
                {
                    createAccountPresenter.DisplayShortPopupTween("Account successfully created!");
                }
            };
        }
    }
}