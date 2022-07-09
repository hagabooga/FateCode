using Godot;
using SimpleInjector;
using Utility;
using static Godot.GD;
using System.Reflection;
using System.Linq;

namespace Client
{
    public class Main : EzPrefab
    {
        public static readonly MethodInfo RegisterSingletonMethod = typeof(SimpleInjector.Container)
              .GetMethods()
              .Where(x => x.Name == "RegisterSingleton")
              .ToArray()[4];

        public readonly Views.MainMenu MainMenuView;

        public SimpleInjector.Container Container { get; private set; }

        public override void _Ready()
        {
            base._Ready();

            Container = new SimpleInjector.Container();
            Container.RegisterInstance(MainMenuView);
            Container.RegisterInstance(MainMenuView.LoginView);
            Container.RegisterInstance(MainMenuView.CreateAccountView);

            X509Certificate certificate = new X509Certificate();
            certificate.Load("res://Certificate/X509Certificate.crt");
            Container.RegisterInstance(certificate);

            Container.RegisterInstance<ClientOptions<Entrance>>(new ClientOptions<Entrance>("localhost", 1969));
            Container.RegisterSingleton<Models.Login>();
            Container.RegisterSingleton<Models.CreateAccount>();

            var typesToAddAsChild = new[] {
                typeof(Entrance),
                typeof(Presenters.Login),
                typeof(Presenters.CreateAccount),
                typeof(Presenters.MainMenu),
            };

            typesToAddAsChild.ForEach(x =>
            {
                RegisterSingletonMethod.MakeGenericMethod(x).Invoke(Container, new object[] { });
            });

            Container.Verify();

            typesToAddAsChild.ForEach(x =>
            {
                AddChild((Node)Container.GetInstance(x));
            });
        }
    }
}