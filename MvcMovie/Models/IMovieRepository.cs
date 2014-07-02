using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    interface IMovieRepository
    {

        IEnumerable<Movie> GetAll();
        Movie Get(int id);
        Movie Add(Movie movie);
        void Remove(int id);
        bool Update(Movie movie);

    }
}
