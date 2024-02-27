/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class CreateActivity
    {

        /*
            This class <public class CreateCommand : IRequest> means we created a Class name CreateCommand which a type of <IRequest> from MediatoR => Take note: the <IRequest> is an interface which will note return anything
            Compare this class with created <QueryActivity> class in the <DetailsActivity> class we will see the differences. 

            But why we do not need to return anythong?
            Because typically we do not return anything with Command.
            It is not necessary to return anything because we are creating a new <Activity> into the database table so it is not nececessary to return anything. But we can return something like string or anything to indicate the status of the creation process if require.

        */
        public class CreateCommand : IRequest
        {
            /*
                This CreateCommandHandler class will get an object of datatype <Activity> and insert into the database table

                Plesae take note:
                The type <Activity> is a class in the <Domain> project which represent the database schema

                This <CreateThisActivity> variable is of <Activity> data type will be insert into the database.
            */
            public Activity CreateThisActivity { get; set; }
        }

        public class CreateActivityInDatabaseHandler : IRequestHandler<CreateCommand>
        {
            private readonly DataContext _context;
            public CreateActivityInDatabaseHandler(DataContext context)
            {
                _context = context; 
            }

            // public async Task CreateActivityInDatabaseHandle(CreateCommand request, CancellationToken cancellationToken)
            // {
            //     // throw new NotImplementedException();
            //     _context.Activities.Add(request.CreateThisActivity);

            //     await _context.SaveChangesAsync();
            // }

            /*
                This is the Interface need to be implement for the <CreateActivityInDatabaseHandler>
            */
            // public async Task CreateActivityInDatabaseHandle(CreateCommand request, CancellationToken cancellationToken)
            // {
            //     // throw new NotImplementedException();
            //     _context.Activities.Add(request.CreateThisActivity);

            //     await _context.SaveChangesAsync();
            // }

            public Task Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.CreateThisActivity);
                await _context.SaveChangesAsync();
            }
        }

    }
}