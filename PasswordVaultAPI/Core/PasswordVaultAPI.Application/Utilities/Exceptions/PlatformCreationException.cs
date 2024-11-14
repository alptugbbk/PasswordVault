using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Utilities.Excepitons
{
    public class PlatformCreationException : Exception
    {

        public PlatformCreationException() : base("RequestPlatformDto cannot be null.") { }


    }
}
