﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin.Security.Providers.GitHub;
using BugTrack.Models;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Threading.Tasks;

using System;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Policy;
using System.Configuration;

namespace BugTrack
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");


            app.UseFacebookAuthentication(
               appId: "749755935641780",
               appSecret: "259470a186200b1596ba1930c4234b8a");



            //app.UseGitHubAuthentication(
            //    clientId: "bde32ecd12eb3276d528",
            //    clientSecret: "959511fd67c9a471edb35113108d11dd16e8d307 "
            //    );


            //app.UseGitHubAuthentication(
            //    clientId: ConfigurationManager.AppSettings["GitHub:ClientId"],
            //    clientSecret: ConfigurationManager.AppSettings["GitHub:ClientSecret"]);

            var githubOptions = new GitHubAuthenticationOptions
            {
                ClientId = "bde32ecd12eb3276d528",
                ClientSecret = "23e5e1c7618b6c1d1bfbf79957245ca4993ba292",
                Provider = new GitHubAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        if (!String.IsNullOrEmpty(context.AccessToken))
                        {
                            // do something with AccessToken
                            Console.Write(context.AccessToken);
                        }
                        if (!String.IsNullOrEmpty(context.Name))
                        {
                            // do something with TeamId
                            Console.Write(context.Name);
                        }
                        if (!String.IsNullOrEmpty(context.UserName))
                        {
                            // do something with TeamName
                            Console.Write(context.UserName);
                        }
                        if (!String.IsNullOrEmpty(context.Id))
                        {
                            // do something with UserId
                            Console.Write(context.Id);
                        }
                        if (context.User != null)
                        {
                            // do something with BotUserId
                            Console.Write(context.User);
                        }
                        return Task.FromResult<object>(context);
                    }
                }
            };

            githubOptions.Scope.Add("Email");
            githubOptions.Scope.Add("Username");
            githubOptions.Scope.Add("public_repo");
            githubOptions.Scope.Add("notifications");
            githubOptions.Scope.Add("read:user");

            //githubOptions.Provider.ReturnEndpoint("");

            app.UseGitHubAuthentication(githubOptions);
        }



    }
}