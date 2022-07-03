using Godot;
using System;
using SimpleInjector;
using Utility;

public class Main : Node
{
    public override void _Ready()
    {
        var main = new SimpleInjector.Container();

        main.RegisterInstance(new ServerOptions<Server>(1909, 100));
        main.RegisterInstance(new ClientOptions<HubConnection>("localhost", 1915));

        main.RegisterSingleton<StateProcessing>();
        main.RegisterSingleton<PlayerVerification>();
        main.RegisterSingleton<HubConnection>();
        main.RegisterSingleton<Server>();

        main.Verify();

        AddChild(main.GetInstance<StateProcessing>());
        AddChild(main.GetInstance<PlayerVerification>());
        AddChild(main.GetInstance<HubConnection>());
        AddChild(main.GetInstance<Server>());
    }
}
