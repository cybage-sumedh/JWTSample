using System.Web.Http;
using JsonWebTokensWebApi.Entities;
using JsonWebTokensWebApi.Models;

namespace JsonWebTokensWebApi.Controllers
{
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAudience = AudiencesStore.AddAudience(audienceModel.Name);

            return Ok<Audience>(newAudience);

        }

        [Route("api/audience/GetClient")]
        public IHttpActionResult Get(string clientId)
        {
            var audience = AudiencesStore.FindAudience(clientId);
            return Ok<Audience>(audience);
        }
    }
}