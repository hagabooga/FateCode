using Godot;
using System;

public partial class Main : Node2D
{
    [Export] public PackedScene PlayerScene { get; private set; }

    readonly ENetMultiplayerPeer multiplayerPeer = new();

    public override void _Ready()
    {
        multiplayerPeer.CreateClient("localhost", 4949);
        multiplayerPeer.PeerConnected += id =>
        {

        };


        Multiplayer.MultiplayerPeer = multiplayerPeer;
    }

}
