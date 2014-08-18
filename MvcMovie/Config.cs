using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MvcMovie
{
    public interface IConfig
    {
        string Get(string key);
    }

    public class Config : IConfig
    {
        public string Get(string key)
        {
            var fromConfig = WebConfigurationManager.AppSettings[key];
            if (String.Equals(fromConfig, "{NotTheRealKey}", StringComparison.InvariantCultureIgnoreCase))
            {
                //get from file or environment variable
                
               // string[] words = reader.ReadToEnd().Split(' ');
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(@"D:\My Documents\GitHub\KeysnotUploadedtoGit\MvcMovie.txt");
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] tokens = lines[i].Split();//split(); // default for split() will split on white space
                        foreach (string tok in tokens)
                        {
                            if (tokens[0] == "ThePasswordToLoadNewWordList")
                                return tokens[1]; //Convert.ToInt32()
                            else
                                return fromConfig;
                            // process tok string here
                        }
                       

                    }
                    return fromConfig;

                }catch(Exception e)
                {
                    Console.Out.WriteLine(e.ToString());
                    return fromConfig;
                }

                    
                
            
                //return Environment.GetEnvironmentVariable(key); from environment

            }
            else
            {
                return fromConfig;
            }
        }
    }
}