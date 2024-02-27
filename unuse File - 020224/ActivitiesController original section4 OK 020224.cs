/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/

using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        /*
            In section 4: The API no longer need direct access to the Database, hence the DataContext will be removed.

            Instead this <API> will sent command to <Application> for database request. 
            The <Application> will then rpocess the necessary Query request and access the Database.
            This <API> will be notify by the Mediator from the <Application> when the data have been found.

            We need need to inject the Mediator into this <API> so that it can communicate with the <Application> 
        */
        // An underscore "_" is use to define a private readonly variable 
        // private readonly DataContext _context;

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



        // public ActivitiesController(DataContext context)
        private readonly IMediator _mediator;
        public ActivitiesController(IMediator mediator)
        {
            // Assigning the Database object to the private variable
            // No longer need this DataContext from Section 4 onward because <API> no longer need to have direct access to gtghe Database
            // _context = context;
            _mediator = mediator;
        }

        /*
            Creating Endpoint for the HTTP request
        */

        [HttpGet] //api/activities => root Endpoint for this API controller
        // Follow by Async method for the HTTP request
        // This <GetActivities()> method will return a List of Activities
        public async Task <ActionResult<List<Activity>>> GetActivities(){
            /*
                this former command is for direct access to the Dtabase which is no longer in use at Section 4
                Instead we wil be using the <Application/QueryHandler> class for accessing the database
            */
            // return await _context.Activities.ToListAsync();
            /*
                ** Code changes at Section 4 - 020224 **
                In order to sent Query from <API> to <Application> we need to use the MediatoR and create a new instance of the <QueryHandler> class in the <Application> project folder
                The <QueryHandler> will have 2 method been created, we need to sent the query to the <Query> method in the <QueryHandler> class

                We also need to register the MediatoR Service with the Project <API/Program.cs>. We always need to add Services where necessary to this <API/Program.cs> in order them to work with the Projects.

                We can do a HTTP request through Postman for testing after adding the MediatoR to this Project. Basically we have change the approach of accessing the database. 
                Previously the <API/ActivitiesController.cs> will direct access the Dtabase. 
                But now we have created a need framework which include an additonal <Application/QueryHandler.cs> class. Using the <MediatoR> package, we enable the <API/ActivitiesController.cs> just to sent a query only to the <Application/QueryHandler.cs>. The <API/ActivitiesController.cs> no longer need to direct access to the database
                The <Application/QueryHandler.cs> will direct access the database and will return the result of a list of activities back to the <API/ActivitiesController.cs> 
            */
            return await _mediator.Send(new QueryHandler.Query());
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
            // return await _context.Activities.FindAsync(id);
            // We will be coming back for this later as of section 4 we not require this for now. hence just return "Ok()"
            return Ok();
        }

    }
}