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
        readonly Client.Entrance entrance;
        readonly Client.GameServer gameServer;

        public MainMenu(Presenters.Login loginPresenter,
                        Presenters.CreateAccount createAccountPresenter,
                        Client.Entrance entrance,
                        Client.GameServer gameServer)
        {
            this.loginPresenter = loginPresenter;
            this.createAccountPresenter = createAccountPresenter;
            this.entrance = entrance;
            this.gameServer = gameServer;

            SetupLogin();
            SetupCreateAccount();
        }


        private void SetupLogin()
        {
            loginPresenter.success += () =>
            {
                loginPresenter.SetInteractable(false);

                entrance.RequestLoginRequest(loginPresenter.Username,
                                            loginPresenter.Password,
                                            loginPresenter.IpAddress);
                entrance.connectionFailed += () =>
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

            entrance.receivedLoginRequest += (result, token) =>
            {
                Print($"Received login request: {result}, {token}.");
                if (result != Error.Ok)
                {
                    loginPresenter.DisplayShortPopupTween("Incorrect username or password!");
                }
                else
                {
                    loginPresenter.DisplayShortPopupTween("Successful login!");
                    // ACTUAL LOGIN HERE
                    gameServer.Token = token;
                    gameServer.ConnectToServer();
                }
            };
        }

        private void SetupCreateAccount()
        {
            createAccountPresenter.success += () =>
            {
                Print("Requesting new account.");
                createAccountPresenter.SetInteractable(false);
                entrance.RequestCreateAccount(createAccountPresenter.Username,
                                             createAccountPresenter.Password,
                                             createAccountPresenter.IpAddress);
                entrance.connectionFailed += () =>
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

            entrance.receivedCreateAccountRequest += (result) =>
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