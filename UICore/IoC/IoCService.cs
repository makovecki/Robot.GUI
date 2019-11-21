using Autofac;
using System;
using UICore.App;
using UICore.App.Themes;
using UICore.Dialogs;
using UICore.Dispatcher;
using UICore.MainWindow;
using UICore.Navigation;

namespace UICore.IoC
{
    public class IoCService : IIoCService
    {
        private ContainerBuilder builder;
        private IContainer container;
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public IoCService()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            builder = new ContainerBuilder();

            builder.RegisterInstance<IIoCService>(this);
            builder.RegisterType<AppService>().As<IAppService>().SingleInstance();
            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>().SingleInstance();
            builder.RegisterInstance<IDispatcher>(new UICore.Dispatcher.Dispatcher());
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<ThemeService>().As<IThemeService>().SingleInstance();
        }

        public Action<IIoCService>? RegisterComponents { private get; set; }

        public void Build()
        {
            RegisterComponents?.Invoke(this);
            container = builder.Build();
        }

        public void RegisterInstance<T>(T instance) where T : class
        {
            builder.RegisterInstance(instance);
        }

        public IRegistrationBuilder RegisterType<TImplementer>() => new RegistrationBuilder<TImplementer>(builder);


        public TImplementer Resolve<TImplementer>()
        {
            return container.Resolve<TImplementer>();
        }

        public TImplementer ResolveTemporaryType<TImplementer>(object? parameter) where TImplementer : class
        {
            using (var scope = container.BeginLifetimeScope(
              b =>
              {
                  b.RegisterType<TImplementer>().AsSelf();

              }))
            {
                if (parameter == null) return scope.Resolve<TImplementer>();
                return scope.Resolve<TImplementer>(new TypedParameter(parameter?.GetType(), parameter));
            }
        }
    }
}
