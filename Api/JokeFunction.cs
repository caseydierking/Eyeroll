using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using BlazorApp.Shared;
using BlazorApp.Api.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BlazorApp.Api
{
    public class JokeFunction

    {
        private readonly EyerollContext eyeroll;

        public JokeFunction(EyerollContext eyeroll)
        {
            this.eyeroll = eyeroll;
        }



        [FunctionName("CreateJoke")]
        public async Task<IActionResult> CreateJoke(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("Creating a new todo list item");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Shared.Models.JokeRequest>(requestBody);
            var submittedJoke = new Models.Joke { Joke1 = input.Joke };

            if (submittedJoke.Joke1 == null || submittedJoke.Joke1 == "") {
                return new BadRequestResult();

            }

            await eyeroll.Jokes.AddAsync(submittedJoke);
            await eyeroll.SaveChangesAsync();
            return new OkObjectResult(submittedJoke);
        }



       
    }
}
