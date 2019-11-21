using System;
using System.Collections.Generic;
using System.Text;

namespace UICore.IoC
{
    public interface IIoCService
    {
        IRegistrationBuilder RegisterType<TImplementer>();
        void RegisterInstance<T>(T instance) where T : class;
        TImplementer Resolve<TImplementer>();
        Action<IIoCService> RegisterComponents { set; }

        TImplementer ResolveTemporaryType<TImplementer>(object? parameter=null) where TImplementer : class;
        void Build();

    }
}
