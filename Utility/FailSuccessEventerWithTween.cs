using Godot;
using System;
using static Godot.GD;
using Utility;
using System.Collections.Generic;

namespace Utility
{
    public abstract class FailSuccessEventerWithTween : FailSuccessEventer
    {
        protected readonly Tween tween = new Tween();

        public abstract Label Label { get; }

        public override void _Ready()
        {
            base._Ready();
            AddChild(tween);
        }

        protected void AddShortPopupTweenToFailed()
        {
            failed += msg =>
            {
                DisplayShortPopupTween(msg);
            };
        }

        public void DisplayShortPopupTween(string msg)
        {
            Label.Text = msg;
            tween.RemoveAll();
            tween.InterpolateProperty(Label,
                                      "self_modulate",
                                      Colors.White,
                                      Colors.Transparent,
                                      5,
                                      Tween.TransitionType.Quad);
            tween.Start();
        }
    }
}