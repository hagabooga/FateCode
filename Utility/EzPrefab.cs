using System.Linq;
using System.Reflection;
using Godot;
using static Godot.GD;

namespace Utility
{
    public abstract class EzPrefab : Node
    {
        static readonly MethodInfo FindNodeMethod = typeof(Node)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.Name == "FindNode")
            .First();

        public Node Self { get; private set; }

        public override void _Ready()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                string path = $"{GetPath()}/{field.Name}";
                var parameters = new object[] { field.Name, true, true };
                object value = FindNodeMethod.Invoke(this, parameters);
                field.SetValue(this, value);
            }

            Node parent = GetParent();
            Self = parent.GetChild(GetIndex());
        }
    }
}