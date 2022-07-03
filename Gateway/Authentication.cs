using Godot;
using System;
using Utility;
using static Godot.GD;

public class Authentication : EzClient<Authentication>
{
    public delegate void ReceivedAuthenticationResults(int id, Error result, string token);
    public delegate void ReceivedCreateAccountRequestResults(int id, Error result);

    public event ReceivedAuthenticationResults receivedAuthenticationResults;
    public event ReceivedCreateAccountRequestResults receivedCreateAccountRequestResults;


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
    void ReceiveAuthenticationResults(int playerId, Error result, string token)
    {
        Print($"Results received and replying to player login request: {playerId}, {result}, {token}.");
        receivedAuthenticationResults?.Invoke(playerId, result, token);
    }

    [Remote]
    void ReceiveCreateAccountRequestResults(int id, Error result)
    {
        receivedCreateAccountRequestResults?.Invoke(id, result);
    }

}
