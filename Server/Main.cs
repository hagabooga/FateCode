using Godot;
using System;
using SimpleInjector;
using Godot.Collections;

namespace Server;

public sealed partial class Main : Node
{

    [Export] public PackedScene PlayerScene { get; private set; }

    [Export] public Array<int> ConnectedPeerIds { get; private set; } = new();
    readonly ENetMultiplayerPeer multiplayerPeer = new();

    public override void _Ready()
    {
        multiplayerPeer.CreateServer(4949);
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
