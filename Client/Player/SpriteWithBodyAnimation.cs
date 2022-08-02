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
    public class SpriteWithBodyAnimation : EzPrefab
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum Animation
        {
            Idle,
            Walk,
            Hack,
            Slash,
            Cast,
            Die
        }

        public readonly Sprite sprite;
        public readonly AnimationPlayer AnimationPlayer;


        [Export] public Animation CurrentAnimation { get; private set; }
        [Export] public Direction CurrentDirection { get; private set; }

        public void Play(Animation animation, Direction direction, float speedRatio = 5)
        {
            CurrentAnimation = animation;
            CurrentDirection = direction;
            AnimationPlayer.Play(
                animation == Animation.Die ?
                "Die" : $"{animation.ToString().ToLower()}_{direction.ToString().ToLower()}",
                -1,
                speedRatio);
        }

        public static Direction GetDirection(float rad, float cutoff = 55, bool reversed = false)
        {
            var angle = Mathf.Rad2Deg(rad);
            var opp = 180 - cutoff;
            var direction = reversed ? Direction.Down : Direction.Up;
            if (-cutoff < angle && angle < cutoff)
            {
                direction = reversed ? Direction.Right : Direction.Left;
            }
            else if (-opp < angle && angle <= -cutoff)
            {
                direction = reversed ? Direction.Up : Direction.Down;
            }
            else if ((-180 <= angle && angle <= -opp) || (opp < angle && angle <= 180))
            {
                direction = reversed ? Direction.Left : Direction.Right;
            }
            return direction;
        }


    }
}