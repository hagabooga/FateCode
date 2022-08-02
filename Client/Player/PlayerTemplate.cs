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
    public class PlayerTemplate : Entity
    {
        public readonly SpriteWithBodyAnimation Body, Hair, Eyes;
        public readonly Node2D BodyAnimations;


        List<SpriteWithBodyAnimation> SpriteWithBodyAnimations { get; } = new List<SpriteWithBodyAnimation>();
        [Export] public SpriteWithBodyAnimation.Direction Facing { get; protected set; }

        public override void _Ready()
        {
            base._Ready();
            SpriteWithBodyAnimations.AddRange(new[] { Body, Hair, Eyes });

        }

        protected void PlayAllBodyAnims(SpriteWithBodyAnimation.Animation anim,
                                        SpriteWithBodyAnimation.Direction? dir = null,
                                        float speedRatio = 5,
                                        bool canMove = true)
        {
            CanMove = true;
            foreach (var bodyAnimation in SpriteWithBodyAnimations)
            {
                if (dir == null)
                {
                    bodyAnimation.Play(anim, bodyAnimation.CurrentDirection, speedRatio);
                }
                else
                {
                    bodyAnimation.Play(anim, dir.Value, speedRatio);
                }
            }
        }
    }
}