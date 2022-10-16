namespace Client;
using Godot;
using System;

public partial class Player : BasePlayer
{
    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector2.Zero;
        if (Input.IsKeyPressed(Key.W))
        {
            direction.y += 1;
        }
        if (Input.IsKeyPressed(Key.S))
        {
            direction.y -= 1;
        }
        if (Input.IsKeyPressed(Key.A))
        {
            direction.x -= 1;
        }
        if (Input.IsKeyPressed(Key.D))
        {
            direction.x += 1;
        }
        Velocity = 100 * direction.Normalized();
        MoveAndSlide();
    }



    protected override void RemoteSetPosition(Vector2 authorityPosition)
    {
        throw new NotImplementedException();
    }
}