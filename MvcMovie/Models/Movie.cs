using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; // 2. for the context
using System.ComponentModel.DataAnnotations; //for data anotations

namespace MvcMovie.Models
{
    public class Movie  //POCO class
    {

        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

    //1.
    public class MovieDBContext : DbContext //represents entity framework movie database context
    {
        public DbSet<Movie> Movies { get; set; }
    }
}