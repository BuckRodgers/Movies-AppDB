using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; // 2. for the context
using System.ComponentModel.DataAnnotations;
using MvcMovie.Migrations; //for data anotations

namespace MvcMovie.Models
{
    public class Movie  //POCO class
    {

        public int ID { get; set; }

        [StringLength(60, MinimumLength=3)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1,100)]
        [DataType(DataType.Currency)] // not a valadation attribute
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }

    //1.

    //https://danielsaidi.wordpress.com/2013/02/25/entity-framework-code-first-with-auto-migrations-on-appharbor-and-more/
    //making the migrations auto for appharbor
    public class MovieDBContext : DbContext //represents entity framework movie database context
    {
        public static string ConnectionStringName = "MovieDBContext";
        
        public MovieDBContext() 
            : base(ConnectionStringName)
        { 
        }
        
        public DbSet<Movie> Movies { get; set; }



        //This is required for auto migrations
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieDBContext, Configuration>());
            base.OnModelCreating(modelBuilder); 
        }
    }

}