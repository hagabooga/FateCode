using Godot;
using System;
using Godot.Collections;
using System.Linq;
using static Godot.GD;
using Utility;

public class Servers : EzServer<Servers>
{
    readonly Dictionary servers = new Dictionary();

    public Servers(ServerOptions<Servers> options) : base(options, null)
    {
    }

    public void DistributeLoginToken(string server, string token, string username)
    {
        var serverId = (int)servers[server];
        RpcId(serverId, "ReceiveLoginToken", token, username);
    }


    protected override void OnNetworkPeerConnected(int serverId)
    {
        Print($"Server {serverId} connected.");
        servers["Server 1"] = serverId;
        Print(servers);
    }
}