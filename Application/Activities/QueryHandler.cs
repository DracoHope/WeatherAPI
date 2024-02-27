/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/

/*
    The class is created for the Activities logic.
    This Class will server as the Query Handler
    TRhis class will return a List of Activities

    We need to have 2 handler
    1. Command Handler
    2. Query Handler

    The <API> will sent the request to <Application>. The <Application> will Handler the request from the <API>. BUt how will the <Application> notify the <API>?
    The Mediator is require to enable the <API> abd <Application> to communicate, receive request and notify each other. 

    There is a Middleware known as ghe Mediator between the <API> and <Application>

    We be using Nu Get to find the <MediatoR v12.11 or later if available> package by Jimmy Bogard. Will install this package to the <Application> Project Folder. This will serve as the Mediator layer between the <API> and <Application> 

*/

/*
    The main function of this <List> class is to be the Query Handler and will return a List of Activities


    If we need to send any data from the <API> such as [Id] of an activity object or other information, then we need to place those data as properties inside this class
*/
/*
    The main functio of this Class is to return a List of Activities
*/
// Import <Domain> to supportbthe Activity object
using Domain;
// import to mediate between the <API> and <Application>
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using SQLitePCL;

/*
    ** Class cret=ated at Section 4 - 020224 **
    This class will function as a Query Handler between the <API> and <Application>
*/

namespace Application.Activities
{
    public class QueryHandler
    {
        /*
            We will create a Class name it as Query and derive from the MediatoR package [IRequest]
            We need tell the IResquest what is return object, for this case the return object is a <List> of <Activity> object from the <Domain>

            This Quest Class will function as the Query Handler

            This is our Mediator Query between the <Api> and <Application>

            We will Pass our <Query> and form a request to the <Handler> then return the Data with a list of activities from this < IRequest<List<Activity>>> interface from the MediatoR package and return the List of <Activities> to the <API>

            After completed the code for this <QueryHandler> class for section 4, we will proceed to the <API> project for the <API/controller/ActivitiesController.cs> coding. Basically the <API/controller/ActivitiesController.cs> will no longer need to direct access to the Dtabase anymore. Instead the <API/controller/ActivitiesController.cs> only need to sent a <Query> to this <Application/QueryHandler.cs> class. This <QueryHandler> class will process the Query and sent the request to the Database. Will notitify and return a List of <Activities> back to the <API/controller/ActivitiesController.cs> 
        */
        public class Query : IRequest<List<Activity>> {
            /*
                If we need to send any data from the <API> such as [Id] of an activity object or other information, then we need to place those data as properties inside this Query class

                Since we just need to return a List of <Activities> for section 4, therefore we do not need any additional properties for now.
            */
        }

        /*  
            Creating the Query Handler Class derive from IRequestHandler from the MediatoR package
            There are two type of return object.
            1. Query Class object
            2. List of Activity object

            Take note:
            <IRequestHandler> is from the MediatoR package
        */
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            /*
                Need a constructor for this <Handler> class
                We need access to the DataContext for the database access
                We need to inject the DataContext into the constructor

                We can inject the Database context through this constructor. So that this Query Handler can access the the Database and return the List of <Activities>

                We can insert a 
            */
            private readonly DataContext _context;
            private readonly ILogger<QueryHandler> _logger;
            public Handler(DataContext context, ILogger<QueryHandler> logger)
            {
                _context = context; 
                _logger = logger;
            }


            /*
                This Handler class need an Interface else there will be an Error 

                This <Handle()> will return a Task<List<Activity>>

                Need to add <async> to this method since it return a <Task>

                The <Handle()> method need a [Query request] and [CancellationToken cancellationToken]

                What is the CancellationToken about?
                CancellationToken is use to cancel the database access operation when user terminate the operation or for some reason the process need to terminated maybe the user end browser is off. 
                For example if the database take very long to retriving a large list of data from the database which took too much of a time. The user might just cancel the process or close the browser since the data have not been display for quite sometime.
            */
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    /*
                        Section 4 - 270224
                        ThrowIfCancellationRequested() method will be executed when the user close the broswer or the request is suddenly cancel for any reason.
                        Wheneven the request is cancel for any reason, the Try catch will throw an Exception
                        Application\Activities\QueryHandler.cs

                        We need to pass the cancellationToken from <Application\Activities\QueryHandler.cs> to the Controller <Application\Activities\QueryHandler.cs> in order the Request cancellation to work properly. We will pass the cancellationToken to the <Activitives Controller> whenever the Request is been cancel
                        API\Controllers\ActivitiesController.cs 
                        We will pass the cancellationToken to the Get Activities Method
                         [HttpGet] //api/activities => root Endpoint for this API controller
                        // Follow by Async method for the HTTP request
                        // This <GetActivities()> method will return a List of Activities
                        public async Task <ActionResult<List<Activity>>> GetActivities()
                    */
                    for(var i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        // If the request is not cancel, then will keep execute the following Delay() method and Log the task
                        await Task.Delay(1000, cancellationToken);
                        // Logged some information
                        _logger.LogInformation($"Task {i} has completed");
                    }
                }
                catch (System.Exception)
                {
                    // throw;
                    // Throw Exception whenever the Request been cancel for any reason such as Broswer is close or user system shut down
                    _logger.LogInformation("Task was cancelled");
                }
                /*
                    This method will accept a <Query> request and access the Dtaabase with the <DataContext> _context
                    Finally will return with a List of Activities
                */
                // Will return result when the <for> loop is completed without any Request been cancel 
                return await _context.Activities.ToListAsync();
            }
        }

    }
}