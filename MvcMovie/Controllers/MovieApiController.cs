using MvcMovie.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace MvcMovie.Controllers
{
    //get CSV from https://drive.google.com/file/d/0B2KuoLJjG3sCNkJUaHBkaUpPY3c/edit?usp=sharing

    //post password
    //if password is ok carry on
    //PASSWORD must be edited out before going into GIT / Appharbor

    //1. Check file is updated (filename == newfilename)
    //2. if yes dowload file to folder
    //3. if not do nothing



    public class MovieApiController : ApiController  //derived from ApiController
    {
        // api/movieapi/
        // Localhost/api/MovieApi/
        // GET api/<controller>


        private MovieDBContext db = new MovieDBContext();
        private int update = 0;

       
        
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Get()
        {
        
            
            var GenreList = new List<string>();

            var GenQry = from d in db.Movies
                         orderby d.Genre
                         select d.Genre;

            GenreList.AddRange(GenQry.Distinct());
     

            var movies = from m in db.Movies
                         select m;

            string json = JsonConvert.SerializeObject(movies);
            return json;
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Update()
        {
            

            var GenreList = new List<string>();

            var GenQry = from d in db.Movies
                         orderby d.Genre
                         select d.Genre;

            GenreList.AddRange(GenQry.Distinct());


            var movies = from m in db.Movies
                         select m;

            
            string json = JsonConvert.SerializeObject(movies);

            return json;
         
        }


        /*
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var movies = from m in db.Movies
                         select m;

           /* string json = JsonConvert.SerializeObject(movies);
            byte[] resultBytes = Encoding.UTF8.GetBytes(json);

            response.Content = new StreamContent(new MemoryStream(resultBytes));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json; charset=utf-8");

            return response;

            */
        /*
 
            var s = new JavaScriptSerializer();
            string jsonClient = s.Serialize(movies);
            byte[] resultBytes = Encoding.UTF8.GetBytes(jsonClient);

            response.Content = new StreamContent(new MemoryStream(resultBytes));

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json; charset=utf-8");
            //WebOperationContext.Current.OutgoingResponse.ContentType =
            //    "application/json; charset=utf-8";

            return response;//new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
           
        }*/

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
        /*
        public int Update()
        {
            return update;
        }
         * */
    }
}