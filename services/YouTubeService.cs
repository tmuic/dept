using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
namespace dept.Services
{
    public class YouTubeServices{

    // requires using Microsoft.Extensions.Configuration;
        private  IConfiguration Configuration;

        public YouTubeServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }
            public IEnumerable<Movie> GetMovie(String title)
            {
                List<Movie> movies=new List<Movie>();
                var bcs=new BaseClientService.Initializer();
                bcs.ApiKey=Configuration["YouTubekey"];
                var service=new YouTubeService(bcs);
                
                var searchListRequest = service.Search.List("snippet");
                Console.Out.Write(title);
                searchListRequest.Q=title+" trailer";
                searchListRequest.MaxResults = 10;
       
              
                var results=searchListRequest.Execute();
 
                
                
                foreach (dynamic result in results.Items){
                    Movie movie=new Movie();
                    movie.Title=  result.Snippet.Title;
                    movie.Url="http://www.youtube.com/v="+result.Id.VideoId;
                    movies.Add(movie);

                }
    

                return movies;
    

        }
    }
}

   