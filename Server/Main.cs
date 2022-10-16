using Godot;
using System;
using SimpleInjector;
using Godot.Collections;
<<<<<<< HEAD
using static Godot.GD;
=======
using System.Threading.Tasks;

>>>>>>> 08327620dd1f8c429205a026222df93c83515a4d
namespace Server;

public sealed partial class Main : Node
{
    [Export] public PackedScene PlayerScene { get; private set; }
    [Export] public Array<int> ConnectedPeerIds { get; private set; }

    readonly ENetMultiplayerPeer multiplayerPeer = new();

    public override void _Ready()
    {
        multiplayerPeer.CreateServer(4949);
<<<<<<< HEAD
        multiplayerPeer.PeerConnected += id =>
        {
            Print(id);
=======
        multiplayerPeer.PeerConnected += async id =>
        {
            await Task.Delay(1000);
            // RpcId(id, nameof(Main.ad))
>>>>>>> 08327620dd1f8c429205a026222df93c83515a4d
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
