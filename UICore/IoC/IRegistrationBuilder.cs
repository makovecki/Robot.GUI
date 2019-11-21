using System;
using System.Collections.Generic;
using System.Text;

namespace UICore.IoC
{
    public interface IRegistrationBuilder
    {
        void As<TService>(bool singleInstace = false) ;
        void AsSelf();

        
    }

    
}
