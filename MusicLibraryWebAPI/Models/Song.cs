using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryWebAPI.Models
{
    public class Song
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
    }
}
