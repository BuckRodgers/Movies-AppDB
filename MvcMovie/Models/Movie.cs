using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; // 2 for the context

namespace MvcMovie.Models
{
    public class Movie  //POCO class
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

    public class MovieDBContext : DbContext //represents entity framework movie database context
    {
        public DbSet<Movie> Movies { get; set; }
    }
}