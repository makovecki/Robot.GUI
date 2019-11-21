using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace UICore.IoC
{
    public class RegistrationBuilder<TImplementer> : IRegistrationBuilder
    {
        private ContainerBuilder builder;

        public RegistrationBuilder(ContainerBuilder builder)
        {
            this.builder = builder;
        }

        public void As<TService>(bool singleInstance = false) 
        {
            if (singleInstance) builder.RegisterType<TImplementer>().As<TService>().SingleInstance();
            else builder.RegisterType<TImplementer>().As<TService>();
            

            
        }

        public void AsSelf()
        {
            builder.RegisterType<TImplementer>().AsSelf();
        }
    }

  
}
