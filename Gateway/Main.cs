using Godot;
using System;
using Utility;
using static Godot.GD;
using SimpleInjector;
namespace Gateway
{
    public class Main : Node
    {
        public override void _Ready()
        {
            var main = new SimpleInjector.Container();

            main.RegisterInstance(new ClientOptions<Authentication>("localhost", 1911));
            main.RegisterInstance(new ServerOptions<Gateway>(1969, 100));

            X509Certificate certificate = new X509Certificate();
            certificate.Load("res://Certificate/X509Certificate.crt");
            main.RegisterInstance(certificate);

            CryptoKey cryptoKey = new CryptoKey();
            cryptoKey.Load("res://Certificate/X509Key.key");
            main.RegisterInstance(cryptoKey);


            main.RegisterSingleton<Authentication>();
            main.RegisterSingleton<Gateway>();


            AddChild(main.GetInstance<Authentication>());
            AddChild(main.GetInstance<Gateway>());

            // var gateway = this.AddChildReturn(new Gateway(new ServerOptions(1969, 100),
            //                                               certificate,
            //                                               cryptoKey,
            //                                               authentication));



        }
    }
}