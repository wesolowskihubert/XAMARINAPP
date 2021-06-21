using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OgloszeniaHubertWebApi.Helpers;
using OgloszeniaHubertWebApi.Models;

namespace OgloszeniaHubertWebApi.Controllers
{
    [Authorize]
    public class OgloszeniaUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult Post([FromBody] OgloszeniaUser ogloszeniaUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stream = new MemoryStream(ogloszeniaUser.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = String.Format("{0}.jpg", guid);
            var folder = "~/Content/Users";
            var fullPath = String.Format("{0}/{1}", folder, file);
            var response = FilesHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                ogloszeniaUser.ImagePath = fullPath;
            }
            var user = new OgloszeniaUser()
            {
                UserName = ogloszeniaUser.UserName,
                Email = ogloszeniaUser.Email,
                Phone = ogloszeniaUser.Phone,
                Category = ogloszeniaUser.Category,
                Wojewodztwo = ogloszeniaUser.Wojewodztwo,
                Item = ogloszeniaUser.Item,
                Date = ogloszeniaUser.Date,
                ImagePath = ogloszeniaUser.ImagePath,
                Lat = ogloszeniaUser.Lat,
                Lon = ogloszeniaUser.Lon
            };

            db.OgloszeniaUsers.Add(user);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        public IEnumerable<OgloszeniaUser>Get(string wojewodztwo, string category)
        {
          var ogloszeniaUsers =  db.OgloszeniaUsers.Where(user => user.Wojewodztwo.StartsWith(wojewodztwo) && user.Category.StartsWith(category));
          return ogloszeniaUsers;
        }

        public IEnumerable<OgloszeniaUser> Get()
        {
            var latestUsers = db.OgloszeniaUsers.OrderByDescending(user=>user.Id);
            return latestUsers;
        }
    }
}