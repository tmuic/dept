using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using dept.Services;
namespace dept.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("movie")]
        public IEnumerable<Movie> Get()
        {
            ImdbService imdb=new ImdbService(_configuration);
        
            return imdb.GetMovie("test");
        }
    }
}
