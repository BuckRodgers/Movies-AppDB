using MvcMovie.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class MovieApiController : ApiController  //derived from ApiController
    {

        private MovieDBContext db = new MovieDBContext();

        // Localhost/api/MovieApi/
        // GET api/<controller>
        //public IEnumerable<Movie> Get()
        public string Get()
        {
            //Movie myDeserializedObj = (Movie)JavaScriptConvert.DeserializeObject(Request["jsonString"], typeof(Movie));

            //List<Movie> myDeserializedObjList = (List<Movie>)Newtonsoft.Json.JsonConvert.DeserializeObject(Request["jsonString"], typeof(List<Movie>));
            
            
            var GenreList = new List<string>();

            var GenQry = from d in db.Movies
                         orderby d.Genre
                         select d.Genre;

            GenreList.AddRange(GenQry.Distinct());
     

            var movies = from m in db.Movies
                         select m;

            String json = JsonConvert.SerializeObject(movies);
           

            //return movies;
            return json;
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // [HttpPost]
        // POST api/<controller>
        // [ValidateAntiForgeryToken]
        
        
        
        public void Post(JObject jsonmovie)//String s)//[Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {

            /*
            string title = movie["Title"].ToString();
            int id = movie["Id"].ToObject<int>();
            DateTime ReleaseDate = movie["ReleaseDate"].ToObject<DateTime>();
            string genre = movie["genre"].ToString();
            int price = movie["Price"].ToObject<int>();
            int rating = movie["Rating"].ToObject<int>();

            */

            string s_movie = jsonmovie.ToString();

            Movie movie = JsonConvert.DeserializeObject<Movie>(s_movie);
            
            if (ModelState.IsValid) //the valadation annotations it checks in the model class
            {
                try
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                
                }
                
                System.Diagnostics.Debug.WriteLine("added to db"); 
            }

            System.Diagnostics.Debug.WriteLine("ended post"); 
            /*
            return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    movie = movie
                });
             * 
             * */
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}