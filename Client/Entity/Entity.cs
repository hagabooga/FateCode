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
    public class Entity : Attributes
    {
        public readonly KinematicBody2D KinematicBody2D;
        public readonly Sprite Sprite;
        public readonly AnimationPlayer SpriteAnimationPlayer;
        public readonly CollisionShape2D Collisionbox, HurtboxShape;
        public readonly Area2D Hurtbox;
        public readonly Control EntityUI;
        public readonly TextureProgress HpBar;
        public readonly Tween HpBarTween;
        public readonly PanelContainer DisplayNamePanel;
        public readonly Label DisplayName;

        [Export] public bool CanMove { get; protected set; }

        public string Ming
        {
            get => DisplayName.Text;
            set
            {
                DisplayName.Text = value;
                DisplayNamePanel.RectSize = DisplayNamePanel.RectMinSize;
                DisplayNamePanel.SetAnchorsAndMarginsPreset(Control.LayoutPreset.CenterBottom);
                DisplayNamePanel.MarginLeft -= 7;
                DisplayNamePanel.MarginRight += 7;
                HpBar.RectSize = HpBar.RectMinSize;
                HpBar.SetAnchorsAndMarginsPreset(Control.LayoutPreset.CenterTop);
                HpBar.MarginLeft -= ((int)(HpBar.RectSize.x / 2));
            }
        }

        public int Id { get; set; }

        public override void _Ready()
        {
            base._Ready();
            MaxHealth.currentMaxChange += (current, max) =>
            {
                if (current <= 0)
                {
                    Die();
                }
            };
        }

        protected virtual void Die()
        {

        }

        protected void ChangeColor(Color color)
        {
            DisplayName.AddColorOverride("font_color", color);
        }

        protected void UpdateHpBar()
        {
            var percentage = ((int)(((float)MaxHealth.Current / MaxHealth) * 100));
            HpBarTween.InterpolateProperty(HpBar, "value", HpBar.Value, percentage, 0.1f);
            HpBarTween.Start();
            HpBar.TintProgress = percentage > 60 ?
                new Color("14e114") :
                percentage > 25 ?
                new Color("e1be32") :
                new Color("e11e1e");
        }

    }
}