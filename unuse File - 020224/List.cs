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

// Import <Domain> to supportbthe Activity object
using Domain;
// import to mediate between the <API> and <Application>
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SQLitePCL;

namespace Application.Activities
{
    public class List
    {
        /*
            We will create a Class name it as Query and derive from the MediatoR package [IRequest]
            We need tell the IResquest what is return object, for this case the return object is a <List> of <Activity> object from the <Domain>

            This Quest Class will function as the Query Handler

            This is our Mediator Query between the <Api> and <Application>

            We will Pass our <Query> and form a request to the <Handler> then return the Data with a list of activities
        */
        public class Query : IRequest<List<Activity>> {
            /*
                If we need to send any data from the <API> such as [Id] of an activity object or other information, then we need to place those data as properties inside this Query class
            */
        }

        /*  
            Creating the Query Handler Class derive from IRequestHandler from the MediatoR package
            There are two type of return object.
            1. Query Class object
            2. List of Activity object
        */
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            /*
                Need a constructor for this <Handler> class
                We need access to the DataContext for the database access
                We need to inject the DataContext into the constructor
            */
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context; 
            }


            /*
                This Handler class need an Interface else there will be an Error 

                This <Handle()> will return a Task<List<Activity>>

                Need to add <async> to this method since it return a <Task>

                The <Handle()> method need a [Query request] and [CancellationToken cancellationToken]
            */
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                // throw new NotImplementedException();
                /*
                    This method will accept a <Query> request and access the Dtaabase with the <DataContext> _context
                    Finally will return with a List of Activities
                */
                return await _context.Activities.ToListAsync();
            }
        }

    }
}