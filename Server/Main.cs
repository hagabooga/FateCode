using Godot;
using System;
using SimpleInjector;
using Godot.Collections;
using static Godot.GD;
namespace Server;

public sealed partial class Main : Node
{
    [Export] public PackedScene PlayerScene { get; private set; }
    [Export] public Array<int> ConnectedPeerIds { get; private set; }

    readonly ENetMultiplayerPeer multiplayerPeer = new();

    public override void _Ready()
    {
        multiplayerPeer.CreateServer(4949);
        multiplayerPeer.PeerConnected += id =>
        {
            Print(id);
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
