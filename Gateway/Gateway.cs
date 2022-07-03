using Godot;
using System;
using Utility;
using static Godot.GD;

public class Gateway : EzServer<Gateway>
{
    private readonly Authentication authentication;

    public Gateway(ServerOptions<Gateway> options,
                   X509Certificate certificate,
                   CryptoKey cryptoKey,
                   Authentication authentication) : base(options, certificate, cryptoKey)
    {
        this.authentication = authentication;
        this.authentication.receivedAuthenticationResults += ReturnLoginRequest;
        this.authentication.receivedCreateAccountRequestResults += ReturnCreateAccountRequest;
    }


    public override void _Ready()
    {
        base._Ready();
    }

    private void ReturnCreateAccountRequest(int id, Error result)
    {
        RpcId(id, "ReceiveCreateAccountRequest", result);
        network.DisconnectPeer(id);
    }

    private void ReturnLoginRequest(int id, Error result, string token)
    {
        RpcId(id, "ReceiveLoginRequest", result, token);
        network.DisconnectPeer(id);
    }

    [Remote]
    void ReceiveLoginRequest(string username, string password)
    {
        Print("Login request received.");
        var playerId = CustomMultiplayer.GetRpcSenderId();
        authentication.RequestAuthenticatePlayer(playerId, username, password);
    }

    [Remote]
    void ReceiveCreateAccountRequest(string username, string password)
    {
        Print("Create account request received.");
        var playerId = CustomMultiplayer.GetRpcSenderId();
        authentication.RequestCreateAccountRequest(playerId, username, password);
    }

}
