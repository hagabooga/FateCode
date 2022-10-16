using Godot;
using System;

namespace Client;


public abstract class BasePlayer
{
    [RPC]
    protected abstract void RemoteSetPosition(Vector2 authorityPosition);
}
