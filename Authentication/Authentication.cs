using Godot;
using System;
using Utility;
using static Godot.GD;

namespace Authentication
{
    public class Authentication : EzServer<Authentication>
    {
        readonly Servers servers;
        readonly Accounts accounts;
        public Authentication(ServerOptions<Authentication> options,
                              Servers servers,
                              Accounts accounts) :
                              base(options, null, null)
        {
            this.servers = servers;
            this.accounts = accounts;
        }

        public override void _Ready()
        {
            base._Ready();
        }

        [Remote]
        public void ReceiveAuthenticatePlayer(int playerId, string username, string password)
        {
            Print("Authentication request recieved");
            var gatewayId = Multiplayer.GetRpcSenderId();
            var result = Error.Ok;
            string token = null;

            Print("Starting authentication...");
            if (!accounts.Exists(username))
            {
                Print($"User {username} is not recognized.");
                result = Error.Failed;
            }
            else
            {
                var account = accounts[username];
                var hashPassword = Fast.GenerateHashPassword(password, account["salt"].ToString());
                string accountPassword = account["password"].ToString();
                Print($"{hashPassword} == {accountPassword}");
                if (accountPassword != hashPassword)
                {
                    Print($"{username} entered incorrect password.");
                    result = Error.Failed;
                }
                else
                {
                    Print("Successful Authentication");
                    Randomize();
                    var hashed = Randi().ToString().SHA256Text();
                    var timeStamp = OS.GetUnixTime().ToString();
                    token = hashed + timeStamp;
                    Print($"Token to be sent: {token}");
                    var server = "Server 0"; // Replace with load balancers
                    servers.DistributeLoginToken(server, token, username);
                }
            }
            Print($"{username}: authentication results now sending to gateway server.");
            RpcId(gatewayId, nameof(Gateway.Authentication.ReceiveAuthenticationResults), playerId, result, token);
        }

        [Remote]
        public void ReceiveCreateAccountRequest(int playerId, string username, string password)
        {
            var authenticationId = Multiplayer.GetRpcSenderId();
            var result = Error.Ok;
            if (accounts.Exists(username))
            {
                result = Error.AlreadyExists;
            }
            else
            {
                var salt = Fast.GenerateSalt();
                Print(salt);
                var hashedPassword = Fast.GenerateHashPassword(password, salt);
                accounts.Create(username, password, salt);
            }
            RpcId(authenticationId, "ReceiveCreateAccountRequestResults", playerId, result);
        }

    }
}