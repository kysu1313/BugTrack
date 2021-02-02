using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Linq;
using Owin;
using System;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartupAttribute(typeof(BugTrack.Startup))]
namespace BugTrack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
