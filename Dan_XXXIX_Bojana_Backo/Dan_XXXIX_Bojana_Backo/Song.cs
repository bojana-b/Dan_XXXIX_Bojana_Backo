using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_XXXIX_Bojana_Backo
{
    /// <summary>
    /// Class with properties for song Autor, Name of the song, Duration
    /// </summary>
    public class Song
    {
        string Author { get; set; }
        string Name { get; set; }
        string Duration { get; set; }

        public Song()
        {

        }

        public Song(string author, string name, string duration)
        {
            Author = author;
            Name = name;
            Duration = duration;
        }

        public override string ToString()
        {
            return "[" + Author + "]: " + "[" + Name + "] " + "[" + Duration + "]";
        }
    }
}
