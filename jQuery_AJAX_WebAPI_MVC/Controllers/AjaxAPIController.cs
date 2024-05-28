using jQuery_AJAX_WebAPI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jQuery_AJAX_WebAPI_MVC.Controllers
{
    public class AjaxAPIController : ApiController
    {

        /// <summary>
        /// Display DateTime
        /// 2024/05/28
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Route("api/AjaxAPI/AjaxMethod")]
        [HttpPost]
        public PersonModel AjaxMethod(PersonModel person)
        {
            person.DateTime = DateTime.Now.ToString();
            return person;
        }


        // GET: api/AjaxAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AjaxAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AjaxAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AjaxAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AjaxAPI/5
        public void Delete(int id)
        {
        }
    }
}
