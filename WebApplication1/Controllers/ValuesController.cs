using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        [Route("login")]
        public IHttpActionResult PostLogin(string username, string password)
        {
            if (username == "Will" && password == "1234")
            {
                FormsAuthentication.RedirectFromLoginPage(username, false);
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("logout")]
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        [Authorize]
        [Route("geo")]
        [HttpGet][HttpPost]
        [ValidateModel]
        [EnableCors("http://localhost", "*", "Get,Put")]
        public IHttpActionResult Get([FromUri]GeoPoint point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(point);
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
