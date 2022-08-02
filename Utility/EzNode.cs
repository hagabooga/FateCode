using Godot;

namespace Utility
{
    public abstract class EzNode : Node
    {
        public override void _Ready()
        {
            Name = GetType().Name;
        }
    }
}