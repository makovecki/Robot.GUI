using System;
using System.Collections.Generic;
using System.Text;

namespace UICore.Exceptions.Model
{
    public class CoreException
    {
        public CoreException(Exception ex, string description)
        {
            Exception = ex;
            Description = description;
        }

        public Exception Exception { get; private set; }
        public string Description { get; set; }
    }
}
