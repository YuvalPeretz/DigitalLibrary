using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Models.Classes.Useable
{
    public abstract class Genre
    {
        public static readonly string
            Novel = "רומן",
            Comics = "קומיקס",
            Fantasy = "פנטנזיה",
            Thriller = "מותחן",
            History = "היסטוריה",
            ScienceFiction = "מדע בדיוני";
        public static readonly List<string> Genres = new List<string>()
        {
            "רומן",
            "קומיקס",
            "פנטנזיה",
            "מותחן",
            "היסטוריה",
            "מדע בדיוני",
            "ילדים"
    };
    };
        /*private static readonly Lazy<Genre> genre = new Lazy<Genre>(() => new Genre()); // multi-thread safe (can't be used by more than one class).
        private Genre() { } // private constractor, so can only be constracted inside of the class, therfor having only one instance of the object.
        public static Genre Instance { get { return genre.Value; } } //returns the instance of the Singelton class for the use of other classes.*/
}