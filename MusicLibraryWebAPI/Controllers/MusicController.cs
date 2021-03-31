using Microsoft.AspNetCore.Mvc;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private ApplicationDbContext _context;
        public MusicController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<MusicController>
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var songs = _context.Songs.ToList();
            return Ok(songs);
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var songsById = _context.Songs.Where(x => x.id == id).FirstOrDefault();
            return Ok(songsById);
        }

        // POST api/<MusicController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            _context.Songs.Add(song);
            _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = song.id }, song);
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
