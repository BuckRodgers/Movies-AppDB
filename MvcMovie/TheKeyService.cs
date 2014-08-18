using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie
{
    public class TheKeyService
    {
        private readonly string _apiKey;

        public TheKeyService(IConfig config)
        {
            _apiKey = config.Get("ThePasswordToLoadNewWordList");
        }

        public string get()
        {
            return _apiKey;
        }
    }
}