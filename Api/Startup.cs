using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using BlazorApp;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
[assembly: WebJobsStartup(typeof(BlazorApp.Api.Startup))]

namespace BlazorApp.Api
{
    public class Startup : IWebJobsStartup
    {

        public void Configure(IWebJobsBuilder builder)
        {

            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");

            builder.Services.AddDbContext<EyerollContext>(options =>
               options.UseSqlServer(connectionString));
        }
    }
}