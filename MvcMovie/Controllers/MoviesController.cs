﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Drive.v2;
using MvcMovie.Models;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;


namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        
       // private DriveService service;
       // private UserCredential credential;
       

        BaseClientService service = new DriveService(new BaseClientService.Initializer()
        {
         //   HttpClientInitializer = credential,
            ApplicationName = "Drive API Sample",
        });
        // GET: /Movies/
        /*public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }*/


        
         //public ActionResult Index(string searchString )

        //NB: {controller}/{action}/{id} so edit is below
        //http://localhost:22403/Movies/Index/ghost
        //public ActionResult Index(string id)

        /*
        //http://localhost:22403/Movies/Index?searchString=ghost
         public ActionResult Index(string searchString )
        {
            //string searchString = id;
            //LINQ (Language intergrated query)
            var movies = from m in db.Movies
                         select m; //SQL query - defined but not run against database

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            } //s=> s.title is a lambda expression - method based linqs. 
            // the query is executed in the view, not now.

            return View(movies);
        }
        */

        public ActionResult Index(string movieGenre, string searchString)
         {
             var GenreList = new List<string>();

             var GenQry = from d in db.Movies
                          orderby d.Genre
                          select d.Genre;

             GenreList.AddRange(GenQry.Distinct());
             ViewBag.movieGenre = new SelectList(GenreList);

             var movies = from m in db.Movies
                          select m;

             if (!String.IsNullOrEmpty(searchString))
             {
                 movies = movies.Where(s => s.Title.Contains(searchString));
             }

             if (!string.IsNullOrEmpty(movieGenre))
             {
                 movies = movies.Where(x => x.Genre == movieGenre);
             }

             return View(movies);


         }


        // GET: /Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: /Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //
        //
        [HttpPost] //attribute restricted to post requests
        [ValidateAntiForgeryToken] //also in view
        public ActionResult Create([Bind(Include="ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid) //the valadation annotations it checks in the model class
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: /Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: /Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: /Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //could use this instead.
        //public ActionResult Delete(FormCollection fcNotUsed, int id = 0)
        //not using a delete link due to security holes
        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")] //note- Delete, not DeleteConfirmed. (Changing names may confuse route table
        [ValidateAntiForgeryToken] // POST, and unique signature for security
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult CVSfileAdded(int? id)
        {

            Config cfg = new Config();
            TheKeyService tks = new TheKeyService(cfg);
            


            int key = Convert.ToInt32(tks.get());

#warning change 22 to Variable configuration in Appharbour
        
            if (id == key) //ToBeSubbedByAppHarbor
            {
                //download file here
                using (WebClient Client = new WebClient())
                {
                    //TODO: ensure no passwords are visable befoe commit
                    //XXX
#warning This may not be ready for commit
#warning May want to set NOCHECKIN on svn or git rule

//#error this may not be ready for commit
                    //NOCHECKIN

                    String downloadUrl = "https://drive.google.com/uc?export=download&id=0B2KuoLJjG3sCNkJUaHBkaUpPY3c"; // shared so no problem.
                    String storedFile = AppDomain.CurrentDomain.BaseDirectory + "/SharedFiles/test.cvs";
                    // note uc?export=download&id=
                    // this enables me to fetch the info from the file.
              

                     

                    var stream = service.HttpClient.GetStreamAsync(downloadUrl);
                    var result = stream.Result;
                    using (var fileStream = System.IO.File.Create(storedFile))//AppDomain.CurrentDomain.BaseDirectory + "/SharedFiles/test.cvs"))
                    {
                        result.CopyTo(fileStream);
                    }

                    /*

                    try
                    {
                        Client.DownloadFile("https://drive.google.com/file/d/0B2KuoLJjG3sCNkJUaHBkaUpPY3c/edit?usp=sharing", "98MCW.cvs");
                    }
                    catch(WebException e)
                    {
                        throw e;
                    }
                    catch (ArgumentNullException e)
                    {
                        throw e;
                    }
                    */

                }
                //var textFromFile = (new WebClient()).DownloadString("https://drive.google.com/file/d/0B2KuoLJjG3sCNkJUaHBkaUpPY3c/edit?usp=sharing");

                return View();
            }
            else
            {
                //not right code
                return View("~/Views/HelloWorld/Index.cshtml");
            }
        }

/*
         public static System.IO.Stream DownloadFile(
      IAuthenticator authenticator, File file) {
    if (!String.IsNullOrEmpty(file.DownloadUrl)) {
      try {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            new Uri(file.DownloadUrl));
        authenticator.ApplyAuthenticationToRequest(request);
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        if (response.StatusCode == HttpStatusCode.OK) {
          return response.GetResponseStream();
        } else {
          Console.WriteLine(
              "An error occurred: " + response.StatusDescription);
          return null;
        }
      } catch (Exception e) {
        Console.WriteLine("An error occurred: " + e.Message);
        return null;
      }
    } else {
      // The file doesn't have any content stored on Drive.
      return null;
    }
        */
        
        //to read a cvs file from google
        //https://gist.github.com/dhlavaty/6121814
        // add output=cvs to end of link

        //    WebClientEx wc = new WebClientEx(new CookieContainer());
        //    wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        //    wc.Headers.Add("DNT", "1");
        //   wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        //    wc.Headers.Add("Accept-Encoding", "deflate");
        //    wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
 
        //    var outputCSVdata = wc.DownloadString(url);
        //    Console.Write(outputCSVdata);
 
        
         
    }
}
