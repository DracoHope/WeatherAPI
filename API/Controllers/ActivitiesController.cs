/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/

using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        // An underscore "_" is use to define a private readonly variable 
        private readonly DataContext _context;

        /*
            Need to get the <DataContext> into this Class by using the "Denpency Injection" method

            The <DataContext> is actually the Persistence Database

            Dependency injection means (injecting)passing in the database <DataContext> object into this <ActivitiesController> Class object

            Whenever a HTTP request coming in, The application will know to sent this request to this <DataContext> class and the database <DataContext> object is injected into this constructor to create the make query to the specific database. 

            Need to create the require endpoint for the HTTP reuest in the <ActivitiesController> class
            
            Testing URL
            http://localhost:5000/Activities
            List all records in SQLite Database
            http://localhost:5000/Activities/103AC1A9-E0B1-4DF0-9C94-8358600DF0DD
            Just get the record for id = 103AC1A9-E0B1-4DF0-9C94-8358600DF0DD
            Using Swagger
            http://localhost:5000/swagger
            or
            http://localhost:5000/swagger/index.html
            or Default Get Weather Database
            http://localhost:5000/api/WeatherForecast
        */
        public ActivitiesController(DataContext context)
        {
            // Assigning the Database object to the private variable
            _context = context;
        }

        /*
            Creating Endpoint for the HTTP request
        */

        [HttpGet] //api/activities => root Endpoint for this API controller
        // Follow by Async method for the HTTP request
        // This <GetActivities()> method will return a List of Activities
        public async Task <ActionResult<List<Activity>>> GetActivities(){
            return await _context.Activities.ToListAsync();
        }

        /* 
            The "{id}" is the <root parameter> for the root Endpoint => id can be any variable to be use for query the database

            The final Endpoint for this HTTP request will be <api/Activities/id>
            Example: api/Activities/abc1234

            Recall that we have a Primary Key which is of "Guid" data type in gthe database table
            The "id" seem to referring to this Primary Key 
        */
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id){
            return await _context.Activities.FindAsync(id);
        }

    }
}