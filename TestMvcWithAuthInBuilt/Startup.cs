using Microsoft.Owin;
using Owin;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartupAttribute(typeof(TestMvcWithAuthInBuilt.Startup))]
namespace TestMvcWithAuthInBuilt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["RakutenJwtServerBaseUrl"];
            //var issuer = ConfigurationManager.AppSettings["RakutenJwtLocalBaseUrl"];
            var audience = ConfigurationManager.AppSettings["DefaultClientId"];
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["DefaultClientSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });

            //app.UseJwtBearerAuthentication(
            //    new JwtBearerAuthenticationOptions
            //    {
            //        AuthenticationMode = AuthenticationMode.Active,
            //        AllowedAudiences = new[] { audience },
            //        IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
            //        {
            //            new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
            //        }
            //        //,
            //        //Provider = new OAuthBearerAuthenticationProvider
            //        //{
            //        //    OnValidateIdentity = context =>
            //        //    {
            //        //        context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("newCustomClaim", "newValue"));
            //        //        return Task.FromResult<object>(null);
            //        //    }
            //        //}
            //    });

        }
    }
}
