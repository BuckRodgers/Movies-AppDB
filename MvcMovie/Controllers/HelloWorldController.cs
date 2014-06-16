using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }


        //
        // GET: /HelloWorld/
        /*
        public ActionResult Index()
        {
            return View();
        }
         */
   
        /*
        public string Index()    
        {
            
            return "this is my <b>default</b> action...";
        }
        */
        // Get /HelloWorld/Welcome
        
        //HelloWorld/Welcome?name=ryan&numTimes=5
        /*
        public string Welcome(String name, int numTimes = 1)
        {
            //protection against javascript maliciuos input
            return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
            //return "this is the welcome action method....";
        }
         * */
	}
}