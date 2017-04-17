using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api")]
    public class ContactController : ApiController
    {
        private static readonly IContactRepository _contacts = new ContactRepository();

        /// <summary>
        /// Return list of contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/public/contacts")]
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            IEnumerable<Contact> listContact = _contacts.AllContacts();

            if (listContact.ToList().Count == 0)
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Não existem contatos cadastrados");
            else
                response = Request.CreateResponse(HttpStatusCode.OK, listContact);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/public/add")]
        public Task<HttpResponseMessage> Post([FromBody]Contact value)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            Contact contact = _contacts.Add(value);

            if(contact != null)
                response = Request.CreateResponse(HttpStatusCode.Created, contact);
            
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("v1/public/upd")]
        public Task<HttpResponseMessage> Put([FromBody]Contact value)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (!_contacts.Update(value))
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            else
                response = Request.CreateResponse(HttpStatusCode.OK);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v1/public/del/{id}")]
        public Task<HttpResponseMessage> Delete(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (!_contacts.Delete(id))
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Registro deletado");
            else
                response = Request.CreateResponse(HttpStatusCode.OK);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
