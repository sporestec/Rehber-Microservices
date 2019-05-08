using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehber.Data.DBContexts;
using Rehber.Model.DataModels;

namespace Rehber.Services.ImagesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        private readonly RehberImageServisContext _context;

        public UserImagesController()
        {
            _context = new RehberImageServisContext();
        }

        // GET: api/UserImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserImages>>> GetUserImages()
        {
            return await _context.UserImages.ToListAsync();
        }

        // GET: api/UserImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserImages>> GetUserImages(int id)
        {
            var userImages = await _context.UserImages.FindAsync(id);

            if (userImages == null)
            {
                return NotFound();
            }

            return userImages;
        }

        // PUT: api/UserImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserImages(int id, UserImages userImages)
        {
            if (id != userImages.FotId)
            {
                return BadRequest();
            }

            _context.Entry(userImages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserImagesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserImages
        [HttpPost]
        public  IActionResult PostUserImages([FromBody]UserImages userImages)
        {
            _context.UserImages.Add(userImages);
             _context.SaveChanges();

            return Ok(userImages);
        }

        // DELETE: api/UserImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserImages>> DeleteUserImages(int id)
        {
            var userImages = await _context.UserImages.FindAsync(id);
            if (userImages == null)
            {
                return NotFound();
            }

            _context.UserImages.Remove(userImages);
            await _context.SaveChangesAsync();

            return userImages;
        }

        private bool UserImagesExists(int id)
        {
            return _context.UserImages.Any(e => e.FotId == id);
        }
    }
}
