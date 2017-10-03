using EStoreRESTApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace EStoreRESTApi.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public List<Authouriser> Get()
        {
            try
            {
                return Authouriser.getAllUsersCreditials();
            }
            catch(ArgumentException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        // GET: api/User/5
        public Authouriser Get(string username)
        {
            try
            {
                return Authouriser.getUserCreditials(username);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        // POST: api/User
        public string Post([FromBody] Authouriser authouriser)
        {
            string username = authouriser.Username;
            string password = authouriser.Password;

            string results = null;
            try
            {
                results = Authouriser.craeteCreditials(username, password);
            }
            catch (Exception e)
            {
                results = e.Message;
            }
            return results;
        }

        // PUT: api/User/5
        public string Put([FromBody] Authouriser authouriser)
        {
            string username = authouriser.Username;
            string password = authouriser.Password;
            try
            {
                return Authouriser.resetUserCreditials(username, password);
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        // DELETE: api/User/5
        public string Delete(string username , string password)
        {
            try
            {
                return Authouriser.unsubscribeUser(username, password);
            }
            catch(ArgumentException e)
            {
                return e.Message;
            }
            
        }
    }
}
