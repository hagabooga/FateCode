using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using SimpleInjector;
using Utility;
using static Godot.GD;
using System.Reflection;


namespace Client
{
    public class Player : PlayerTemplate
    {
        [Export] public float Speed { get; protected set; } = 0;
        [Export] public float MaxSpeed { get; protected set; } = 150;
        [Export] public float Acceleration { get; protected set; } = 1000;

        public override void _Ready()
        {
            base._Ready();
            MaxHealth = 100;
            Ming = "Hagabooga";
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            if (!CanMove)
            {
                return;
            }
            TurnTowardsMouse();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            MovementLoop(delta);
        }

        private void MovementLoop(float delta)
        {
            if (!CanMove)
            {
                return;
            }
            var moveDirection = Vector2.Zero;
            if (Input.IsActionPressed("ui_left"))
            {
                moveDirection.x--;
            }
            if (Input.IsActionPressed("ui_right"))
            {
                moveDirection.x++;
            }
            if (Input.IsActionPressed("ui_up"))
            {
                moveDirection.y--;
            }
            if (Input.IsActionPressed("ui_down"))
            {
                moveDirection.y++;
            }

            if (moveDirection.Equals(Vector2.Zero))
            {
                Speed = 0;
                PlayAllBodyAnims(SpriteWithBodyAnimation.Animation.Idle, Facing);
                return;
            }

            if (moveDirection.x < 0)
            {
                Facing = SpriteWithBodyAnimation.Direction.Left;
            }
            else if (moveDirection.x > 0)
            {
                Facing = SpriteWithBodyAnimation.Direction.Right;
            }
            if (moveDirection.x == 0 && moveDirection.y < 0)
            {
                Facing = SpriteWithBodyAnimation.Direction.Up;
            }
            else if (moveDirection.x == 0 && moveDirection.y > 0)
            {
                Facing = SpriteWithBodyAnimation.Direction.Down;
            }
            PlayAllBodyAnims(SpriteWithBodyAnimation.Animation.Walk, Facing);
            Speed += Acceleration * delta;
            Speed = Math.Min(Speed, MaxSpeed);
            moveDirection = KinematicBody2D.MoveAndSlide(moveDirection.Normalized() * Speed);
        }



        private float TurnTowardsMouse()
        {
            var rad = BodyAnimations.GlobalPosition.AngleToPoint(BodyAnimations.GetGlobalMousePosition());
            Facing = SpriteWithBodyAnimation.GetDirection(rad);
            return rad + Mathf.Pi;
        }
    }
}