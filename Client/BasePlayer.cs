using Godot;
using System;

namespace Client;


public abstract partial class BasePlayer : CharacterBody2D
{
    [RPC]
    protected abstract void RemoteSetPosition(Vector2 authorityPosition);
}
