using Godot;
using System;
using SimpleInjector;
using Godot.Collections;
using static Godot.GD;
using System.Threading.Tasks;

namespace Server;

public sealed partial class Main : Node
{
    [Export] public PackedScene PlayerScene { get; private set; }
    [Export] public Array<int> ConnectedPeerIds { get; private set; }

    readonly ENetMultiplayerPeer multiplayerPeer = new();

    public override void _Ready()
    {
        multiplayerPeer.CreateServer(4949);
        multiplayerPeer.PeerConnected += async id =>
        {
            Print(id);
            await Task.Delay(1000);
            // RpcId(id, nameof(Main.ad))
        };

        Multiplayer.MultiplayerPeer = multiplayerPeer;
    }

    void AddPlayer(int peerId)
    {
        ConnectedPeerIds.Add(peerId);
        var player = PlayerScene.Instantiate();
        player.SetMultiplayerAuthority(peerId);
        AddChild(player);
    }
}
