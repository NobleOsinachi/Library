using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Genre //: LibraryAsset
    {
        public Genre(string name)
        {
            this.Name = name;
        }
        public Genre(GenreId id)
        {
            this.Id = (int)id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum GenreId : byte
    {
        /*["Unknown", "Action", "Adventure", "Animation", "Children", "Comedy", "Crime", "Documentary", "Drama", "Family", "Fantasy", 
        "FilmNoir", "Horror", "Love", "Musical", "Mystery", "Romance", "SciFi", "Thriller", "War", "Western"]*/
        Unknown = 1,
        Action,
        Adventure,
        Animation,
        Children,
        Comedy,
        Crime,
        Documentary,
        Drama,
        Family,
        Fantasy,
        Fiction,
        FilmNoir,
        Horror,
        Love,
        Musical,
        Mystery,
        NonFiction,
        Romance,
        SciFi,
        Thriller,
        War,
        Western,
    }
}
