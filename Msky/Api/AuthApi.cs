using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Msky.Entities;
using Newtonsoft.Json;

namespace Msky.Api
{
    public class AuthApi : ApiBase
    {
        internal AuthApi(Credential credential) : base(credential) { }

        public AuthSessionApi Session => new AuthSessionApi(Credential);
    }
}
