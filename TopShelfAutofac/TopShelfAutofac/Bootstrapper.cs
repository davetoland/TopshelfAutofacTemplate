using Autofac;
using Microsoft.Extensions.Configuration;
using Topshelf;
using Topshelf.Autofac;
using TopShelfAutofac.Interfaces;
using TopShelfAutofac.Types;

namespace TopShelfAutofac
{
    public class Bootstrap
    {
        public IConfiguration Configuration { get; set; }
        public IContainer IocContainer { get; set; }

        public static Bootstrap Create()
        {
            var bootstrap = new Bootstrap();
            bootstrap.InitialiseConfiguration();
            bootstrap.InitialiseContainer();
            return bootstrap;
        }

        public void Run()
        {
            var topShelfHost = HostFactory.New(host =>
            {
                host.UseAutofacContainer(IocContainer);
                host.Service<GenericService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((svc, hostControl) => svc.Start(hostControl));
                    s.WhenStopped((svc, hostControl) => svc.Stop(hostControl));
                });

                // Service start mode.
                host.StartAutomaticallyDelayed();
                //host.StartAutomatically();
                //host.StartManually();

                // Service descriptors.
                host.SetDescription(Configuration.GetSection("Defaults")["Description"]);
                host.SetDisplayName(Configuration.GetSection("Defaults")["DisplayName"]);
                host.SetServiceName(Configuration.GetSection("Defaults")["ServiceName"]);

                // Service user.
                host.RunAsLocalSystem();
                //host.RunAsLocalService();
                //host.RunAsNetworkService();
                //host.RunAsPrompt();
            });

            topShelfHost.Run();
        }

        private void InitialiseConfiguration()
        {
            // Note: the 'development' version is optional, but if it's
            // present it'll be used. However it will only overwrite those 
            // values it defines, values present only in the base version
            // will still be available as normal.
            // Cloak the 'development' version from source control,
            // and each dev can have their own custom copy of it.
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.development.json", true, true)
                .Build();
        }

        private void InitialiseContainer()
        {
            var builder = new ContainerBuilder();

            // Variety of IoC registration options...
            builder.RegisterInstance(Configuration);
            builder.RegisterType<GenericService>().SingleInstance();
            builder.RegisterType(typeof(SomeType)).AsSelf();
            builder.RegisterType<SomeOtherType>().As(typeof(ISomeOtherType));
            builder.RegisterType<SomeFinalType>().As<IRandomGenerator>();

            IocContainer = builder.Build();
        }
    }
}
