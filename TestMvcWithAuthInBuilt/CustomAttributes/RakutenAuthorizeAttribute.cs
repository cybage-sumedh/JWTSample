using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using TestMvcWithAuthInBuilt.Models;
using Thinktecture.IdentityModel.Tokens;

namespace TestMvcWithAuthInBuilt.CustomAttributes
{
    public class RakutenAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            ValidateToken(filterContext.HttpContext.Request);
            base.OnAuthorization(filterContext);
        }

        private void ValidateToken(HttpRequestBase request)
        {
            if (string.IsNullOrEmpty(JwtModel.Instance.Token))
                return;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                if (!tokenHandler.CanValidateToken) return;

                var validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = "286dfef2e74b4f85b88fca8437332baf",
                    ValidIssuer = "http://rakutenjwtauthsrv.azurewebsites.net",
                    IssuerSigningKey = new InMemorySymmetricSecurityKey(TextEncodings.Base64Url.Decode("tZCyNPHN_xf_wrU1Pni4L2FJER_kwJfPt1b2kZ5cghU"))
                };

                var readToken = tokenHandler.ReadToken(JwtModel.Instance.Token);

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(JwtModel.Instance.Token, validationParameters, out validatedToken);

                HttpContext.Current.User = principal;
                System.Threading.Thread.CurrentPrincipal = HttpContext.Current.User;
            }
            catch (System.IdentityModel.Tokens.SecurityTokenExpiredException expiredException)
            {
                return;
            }
            

            //var ctx = request.GetOwinContext();
            //var authManager = ctx.Authentication;
            //authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //authManager.SignIn(new AuthenticationProperties(), principal.Identities.GetEnumerator());
        }

    }

}