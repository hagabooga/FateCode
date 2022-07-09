using Godot;
using System;
using Utility;
using static Godot.GD;

namespace Presenters
{
    public class MainMenu : EzNode
    {
        readonly Presenters.Login loginPresenter;
        readonly Presenters.CreateAccount createAccountPresenter;
        readonly Client.Entrance gateway;

        public MainMenu(Presenters.Login loginPresenter,
                        Presenters.CreateAccount createAccountPresenter,
                        Client.Entrance gateway)
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
                loginPresenter.SetInteractable(false);

                gateway.RequestLoginRequest(loginPresenter.Username,
                                            loginPresenter.Password,
                                            loginPresenter.IpAddress);
                gateway.connectionFailed += () =>
                {
                    loginPresenter.DisplayShortPopupTween("Connected to server failed!");
                    loginPresenter.SetInteractable(true);
                };
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
                createAccountPresenter.SetInteractable(false);
                gateway.RequestCreateAccount(createAccountPresenter.Username,
                                             createAccountPresenter.Password,
                                             createAccountPresenter.IpAddress);
                gateway.connectionFailed += () =>
                {
                    createAccountPresenter.DisplayShortPopupTween("Connected to server failed!");
                    createAccountPresenter.SetInteractable(true);
                };
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