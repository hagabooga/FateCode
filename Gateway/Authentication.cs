using Godot;
using System;
using Utility;
using static Godot.GD;
namespace Gateway
{
    public class Authentication : EzClient<Authentication>
    {
        public event Handlers.IdResultToken receivedAuthenticationResults;
        public event Handlers.IdResult receivedCreateAccountRequestResults;

        public Authentication(ClientOptions<Authentication> options) : base(options, true, null)
        {
        }

        public override void _Ready()
        {
            base._Ready();
        }

        public void RequestAuthenticatePlayer(int playerId, string username, string password)
        {
            Print($"Requesting authenticate player. {username} {password}.");
            RpcId(1, "ReceiveAuthenticatePlayer", playerId, username, password);
        }

        public void RequestCreateAccountRequest(int playerId, string username, string password)
        {
            Print($"Requesting create account: {username} {password}");
            RpcId(1, "ReceiveCreateAccountRequest", playerId, username.ToLower(), password);
        }

        [Remote]
        public void ReceiveAuthenticationResults(int playerId, Error result, string token)
        {
            Print($"Results received and replying to player login request: {playerId}, {result}, {token}.");
            receivedAuthenticationResults?.Invoke(playerId, result, token);
        }

        [Remote]
        public void ReceiveCreateAccountRequestResults(int id, Error result)
        {
            receivedCreateAccountRequestResults?.Invoke(id, result);
        }

    }
}