using Godot;
using System;
using Utility;
using static Godot.GD;

namespace Client
{
    public class Entrance : EzClient<Entrance>
    {
        public event Handlers.ResultToken receivedLoginRequest;
        public event Handlers.Result receivedCreateAccountRequest;

        public Entrance(ClientOptions<Entrance> options,
                       X509Certificate certificate) :
                       base(options,
                            false,
                            certificate)
        {
        }

        public void RequestLoginRequest(string username, string password, string ipAddress)
        {
            CreateClient(ipAddress);
            connectedToServer += () =>
            {
                RpcId(1, nameof(Gateway.Entrance.ReceiveLoginRequest), username, password.SHA256Text());
            };
        }

        public void RequestCreateAccount(string username, string password, string ipAddress)
        {
            CreateClient(ipAddress);
            connectedToServer += () =>
            {
                RpcId(1, nameof(Gateway.Entrance.ReceiveCreateAccountRequest), username, password.SHA256Text());
            };
        }

        [Remote]
        void ReceiveLoginRequest(Error result, string token)
        {
            receivedLoginRequest?.Invoke(result, token);
        }

        [Remote]
        void ReceiveCreateAccountRequest(Error result)
        {
            receivedCreateAccountRequest?.Invoke(result);
        }
    }
}