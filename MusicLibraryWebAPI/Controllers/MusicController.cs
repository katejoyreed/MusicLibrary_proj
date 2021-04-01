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
            var songs = from s in _context.Songs
                        select new SongDTO()
                        {
                            id = s.id,
                            Title = s.Title,
                            Album = s.Album,
                            Artist = s.Artist,
                            ReleaseDate = s.ReleaseDate
                        };

            return Ok(songs);
        }

        // GET api/<MusicController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var songsById = _context.Songs.Select(s => new SongDTO {
                id = s.id,
                Title = s.Title,
                Album = s.Album,
                Artist = s.Artist,
                ReleaseDate = s.ReleaseDate
            }).Where(x => x.id == id).FirstOrDefault();
            return Ok(songsById);
        }

        // POST api/<MusicController>
        [HttpPost]
        public IActionResult Post([FromBody] SongDTO songDTO)
        {
            var song = new Song() 
            { 
                id = songDTO.id, 
                Title = songDTO.Title, 
                Album = songDTO.Album, 
                Artist = songDTO.Artist, 
                ReleaseDate = songDTO.ReleaseDate
            };
            _context.Songs.Add(song);
            _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = song.id }, song);
        }

        // PUT api/<MusicController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SongDTO songDTO)
        {
            var songConerted = _context.Songs.Select(s => new SongDTO
            {
                id = s.id,
                Title = s.Title,
                Album = s.Album,
                Artist = s.Artist,
                ReleaseDate = s.ReleaseDate
            }).Where(x => x.id == id).FirstOrDefault();
            Song song = _context.Songs.Where(x => x.id == songDTO.id).FirstOrDefault();
            _context.Entry(song).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChangesAsync();
            return Ok(songDTO);
        }

        // DELETE api/<MusicController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Song song = _context.Songs.Where(x => x.id == id).FirstOrDefault();
            _context.Songs.Remove(song);
            _context.SaveChangesAsync();
            return Ok(song);
        }
    }
}
