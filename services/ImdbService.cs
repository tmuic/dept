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

namespace dept.Services
{
    public class ImdbService
    {
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;

    public ImdbService(IConfiguration configuration)
    {
        Configuration = configuration;
    }
        public IEnumerable<Movie> GetMovie(String title)
        {
            List<Movie> movies=new List<Movie>();
            Movie movie=new Movie();
        var handler = new HttpClientHandler() 
        { 
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

            string url="https://imdb-api.com/en/API/SearchMovie/"+Configuration["IMDBKey"]+"/"+title;
            using(var client = new HttpClient(handler))
            {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    Stream receiveStream = response.Content.ReadAsStream();
                    StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);
                    var rsp= readStream.ReadToEnd();
                 dynamic stuff = JsonConvert.DeserializeObject(rsp);
                    //foreach (dynamic result in stuff)
                   // {
                            foreach (dynamic movieRsp in stuff.results)
                            {
                                movie.Title=movieRsp.title;
                                movie.Url="http://www.imdb.com/title/"+movieRsp.id;
                            }
                    //}
                    
  
            
            }
            movies.Add(movie);
            return movies;

 
        }
    }
}
