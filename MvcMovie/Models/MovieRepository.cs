using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class MovieRepository : IMovieRepository
    {
        private List<Movie> movies = new List<Movie>();
        private int _nextId = 1;

        
        public MovieRepository()
        {
            Add(new Movie { Genre = "fun", ID = 55, Price = 99, Rating = "5", ReleaseDate = Convert.ToDateTime(2/2/1999), Title = "happy Feet" });
        }

        public IEnumerable<Movie> GetAll()
        {
            return movies;
        }

        public Movie Get(int id)
        {
            return movies.Find(m => m.ID == id);
        }




        public Movie Add(Movie movie)
        {
            if (movie == null)
            { 
                throw new NotImplementedException();
            }

            movie.ID = _nextId;
            movies.Add(movie);
            return movie;
        }

        public void Remove(int id)
        {
            movies.RemoveAll(m => m.ID == id);
        }

        public bool Update(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException("movie");
            }

            int index = movies.FindIndex(m => m.ID == movie.ID);
            if(index == -1)
            {
                return false;
            }

            movies.RemoveAt(index);
            movies.Add(movie);
            return true;
        }

    }


}