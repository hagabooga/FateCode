using Godot;
using System;
using Utility;
using static Godot.GD;
using SimpleInjector;

namespace Authentication
{
    public class Main : Node
    {
        public override void _Ready()
        {
            var main = new SimpleInjector.Container();


            main.RegisterInstance(new ServerOptions<Servers>(1915, 100));
            main.RegisterInstance(new ServerOptions<Authentication>(1911, 5));

            main.RegisterSingleton<Servers>();
            main.RegisterSingleton<Accounts>();
            main.RegisterSingleton<Authentication>();

            main.Verify();

            AddChild(main.GetInstance<Servers>());
            AddChild(main.GetInstance<Accounts>());
            AddChild(main.GetInstance<Authentication>());
        }
    }
}