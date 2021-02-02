﻿using BugTrack.Models;
using System;
using System.Web.Mvc;
using Octokit;
using System.Threading.Tasks;
using System.Web.Security;

namespace BugTrack.Controllers.Api
{
    public class GithubController : Controller
    {
        // GET: Github
        const string clientId = "bde32ecd12eb3276d528";
        private const string clientSecret = "6c8160f2a9e80b9121a55b7169a44dee87d8eb21 ";
        readonly GitHubClient client =
            new GitHubClient(new ProductHeaderValue("Haack-GitHub-Oauth-Demo"), new Uri("https://github.com/"));

        // This URL uses the GitHub API to get a list of the current user's
        // repositories which include public and private repositories.
        public async Task<ActionResult> Index()
        {
            var accessToken = Session["OAuthToken"] as string;
            if (accessToken != null)
            {
                // This allows the client to make requests to the GitHub API on the user's behalf
                // without ever having the user's OAuth credentials.
                client.Credentials = new Credentials(accessToken);
            }

            try
            {
                // The following requests retrieves all of the user's repositories and
                // requires that the user be logged in to work.
                var repositories = await client.Repository.GetAllForCurrent();
                var model = new GithubIndexViewModel(repositories);

                return View(model);
            }
            catch (AuthorizationException)
            {
                // Either the accessToken is null or it's invalid. This redirects
                // to the GitHub OAuth login page. That page will redirect back to the
                // Authorize action.
                return Redirect(GetOauthLoginUrl());
            }
        }

        // This is the Callback URL that the GitHub OAuth Login page will redirect back to.
        public async Task<ActionResult> Authorize(string code, string state)
        {
            if (!String.IsNullOrEmpty(code))
            {
                var expectedState = Session["CSRF:State"] as string;
                if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
                Session["CSRF:State"] = null;

                var token = await client.Oauth.CreateAccessToken(
                    new OauthTokenRequest(clientId, clientSecret, code)
                    {
                        RedirectUri = new Uri("http://localhost:58292/home/authorize")
                    });
                Session["OAuthToken"] = token.AccessToken;
            }

            return RedirectToAction("Index");
        }

        private string GetOauthLoginUrl()
        {
            string csrf = Membership.GeneratePassword(24, 1);
            Session["CSRF:State"] = csrf;

            // 1. Redirect users to request GitHub access
            var request = new OauthLoginRequest(clientId)
            {
                Scopes = { "user", "notifications" },
                State = csrf
            };
            var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);
            return oauthLoginUrl.ToString();
        }

        public async Task<ActionResult> Emojis()
        {
            var emojis = await client.Miscellaneous.GetAllEmojis();

            return View(emojis);
        }
    }
}