using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Store.RepositoryLayer;


namespace Store.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        // GET: User
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/User/Index")]
        public IHttpActionResult Index()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            UserDbRepository userDbRepository = new UserDbRepository(connectionString);
            DbActionResult<List<User>> dbActionResult = userDbRepository.GetAllUsers();

            //if (userList.Count == 0)
            //{
            //    return ResponseMessage(new System.Net.Http.HttpResponseMessage()
            //    {
            //        StatusCode = System.Net.HttpStatusCode.NoContent,
            //        Content = new StringContent("No data")
            //    });
            //}
            //else
            //{
               return Ok();
            //}
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/User/GetUsersById")]
        public IHttpActionResult GetUserbyId(Guid userId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            UserDbRepository userDbRepository = new UserDbRepository(connectionString);
            User userInfo = userDbRepository.GetUserById(userId);
            if (userInfo == null)
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Content = new StringContent("No data")
                });
            }
            else
            {
                return Ok();
            }                
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/User/GetUserByName")]
        public IHttpActionResult GetUserbyName(String searchTerm)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            UserDbRepository userDbRepository = new UserDbRepository(connectionString);
            List<User> userList = userDbRepository.GetUsersByName(searchTerm);
            if (userList.Count == 0)
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Content = new StringContent("No data")
                });
            }
            else
            {
                return Ok();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/Add")]
        public IHttpActionResult Add(User user)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            UserDbRepository userDbRepository = new UserDbRepository(connectionString);
            DbActionResult dbActionResult = userDbRepository.AddUser(user);
            if (dbActionResult.Success)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(dbActionResult.Message)
                });
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/Update")]
        public IHttpActionResult Update(User user)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            UserDbRepository userDbRepository = new UserDbRepository(connectionString);
            DbActionResult dbActionResult = userDbRepository.UpdateUser(user);
            if (dbActionResult.Success)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(dbActionResult.Message)
                });
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/User/Delete")]
        public IHttpActionResult Delete(User user)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["StoreDbConnection"].ConnectionString;
            UserDbRepository userDbRepository = new UserDbRepository(connectionString);
            DbActionResult dbActionResult = userDbRepository.DeleteUser(user.UserId);
            if (dbActionResult.Success)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(dbActionResult.Message)
                });
            }
        }
        
    }
}