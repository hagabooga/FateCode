using System.Collections;
using System.Collections.Generic;
using Godot;
using static Godot.GD;

namespace Utility
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string x) => string.IsNullOrEmpty(x);

        public static T SafelySetScript<T>(this Object obj, Resource resource) where T : Object
        {
            var godotObjectId = obj.GetInstanceId();
            // Replaces old C# instance with a new one. Old C# instance is disposed.
            obj.SetScript(resource);
            // Get the new C# instance
            return GD.InstanceFromId(godotObjectId) as T;
        }

        public static T SetScriptSafe<T>(this Object obj, string resource) where T : Object
        {
            return SafelySetScript<T>(obj, ResourceLoader.Load(resource));
        }

        public static T AddChildReturn<T>(this Node obj, T instance) where T : Node
        {
            obj.AddChild(instance);
            return instance;
        }

        public static bool IsEmpty(this ICollection collection) => collection.Count == 0;

        public static void ForEach<T>(this IEnumerable<T> x, System.Action<T> action)
        {
            foreach (var item in x)
            {
                action(item);
            }
        }
        public static void ForEach(this IEnumerable x, System.Action<object> action)
        {
            foreach (var item in x)
            {
                action(item);
            }
        }

        public static void SetTextAndEmit(this LineEdit lineEdit, string text)
        {
            lineEdit.Text = text;
            lineEdit.EmitSignal("text_changed", text);
        }

        // public static T RegisterSingletonAndGetInstance<T>(this SimpleInjector.Container container) where T : Node
        // {
        //     container.RegisterSingleton<T>();
        //     return container.GetInstance<T>();
        // }

    }
}