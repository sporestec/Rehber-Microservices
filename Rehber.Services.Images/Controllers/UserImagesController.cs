using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehber.Data.Contexts;
using Rehber.Model.DataModels;

namespace Rehber.Services.ImagesApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        private readonly RehberImageServiceDbContext _context;

        public UserImagesController(RehberImageServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/UserImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserImages>>> GetUserImage()
        {
            return await _context.UserImages.ToListAsync();
        }

        // GET: api/UserImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserImages>> GetUserImage(int id)
        {
            var userImage = await _context.UserImages.Where(x => x.UserId == id).SingleAsync();

            if (userImage == null)
            {
                return NotFound();
            }

            return userImage;
        }

        // PUT: api/UserImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserImage(int id, UserImages userImage)
        {
            if (id != userImage.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(userImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserImageExists(id))
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
        public async Task<ActionResult<UserImages>> PostUserImage(UserImages userImage)
        {
            _context.UserImages.Add(userImage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserImageExists(userImage.ImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserImage", new { id = userImage.ImageId }, userImage);
        }

        // DELETE: api/UserImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserImages>> DeleteUserImage(int id)
        {
            var userImage = await _context.UserImages.FindAsync(id);
            if (userImage == null)
            {
                return NotFound();
            }

            _context.UserImages.Remove(userImage);
            await _context.SaveChangesAsync();

            return userImage;
        }

        private bool UserImageExists(int id)
        {
            return _context.UserImages.Any(e => e.ImageId == id);
        }
    }
}
