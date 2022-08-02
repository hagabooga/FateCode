using Godot;
using System;
using SimpleInjector;
using Utility;

namespace Server
{
    public class Main : Node
    {
        public override void _Ready()
        {
            var main = new SimpleInjector.Container();

            main.RegisterInstance(new ServerOptions<GameServer>(1909, 100));
            main.RegisterInstance(new ClientOptions<HubConnection>("localhost", 1915));

            main.RegisterSingleton<StateProcessing>();
            main.RegisterSingleton<PlayerVerification>();
            main.RegisterSingleton<HubConnection>();
            main.RegisterSingleton<GameServer>();

            main.Verify();

            AddChild(main.GetInstance<StateProcessing>());
            AddChild(main.GetInstance<PlayerVerification>());
            AddChild(main.GetInstance<HubConnection>());
            AddChild(main.GetInstance<GameServer>());

            main.GetInstance<PlayerVerification>().ReceiveToken("tests");

        }
    }
}