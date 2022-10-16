using Godot;
using System;

namespace Server;


public partial class Player : Client.BasePlayer
{
    protected override void RemoteSetPosition(Vector2 authorityPosition)
    {
    }
}
