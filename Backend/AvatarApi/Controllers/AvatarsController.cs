using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AvatarApi.Models;

namespace AvatarApi.Controllers
{
    public class AvatarsController : ApiController
    {
        private avatar_dbEntities db = new avatar_dbEntities();

        // GET: api/Avatars
        public IQueryable<Avatar> GetAvatars()
        {
            return db.Avatars;
        }

        // GET: api/Avatars/5
        [ResponseType(typeof(Avatar))]
        public IHttpActionResult GetAvatar(int id)
        {
            Avatar avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return NotFound();
            }

            return Ok(avatar);
        }

        // PUT: api/Avatars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAvatar(int id, Avatar avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != avatar.id)
            {
                return BadRequest();
            }

            db.Entry(avatar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvatarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Avatars
        [ResponseType(typeof(Avatar))]
        public IHttpActionResult PostAvatar(Avatar avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Avatars.Add(avatar);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AvatarExists(avatar.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = avatar.id }, avatar);
        }

        // DELETE: api/Avatars/5
        [ResponseType(typeof(Avatar))]
        public IHttpActionResult DeleteAvatar(int id)
        {
            Avatar avatar = db.Avatars.Find(id);
            if (avatar == null)
            {
                return NotFound();
            }

            db.Avatars.Remove(avatar);
            db.SaveChanges();

            return Ok(avatar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvatarExists(int id)
        {
            return db.Avatars.Count(e => e.id == id) > 0;
        }
    }
}