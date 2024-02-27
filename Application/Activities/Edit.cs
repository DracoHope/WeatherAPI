/*
using System;
using System.Collections.Generic;
// using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
*/

/*
    Section 4 - Add a Updaing Request
    1. Create a Edit class to sent uopdating Request
    2. Using The Auto Mapper Package for updating the Activity Object value
    3. Need to install the Auto Mapper package using the Nu Get Galley
    4. Install the Auto Mapper package into the <Application> project folder which this <Edit.cs> class is


    How to use the Auto Mapper function?
    1. Create a new folder under the <Application> Project
    2. Name the new folder as <Core>. This <Core> folder will contain all the special features require for this <Application> such as the Auto Mapper function.

    We be using the the Auto Mapper to track one <Activity> to another <Activity> object
*/
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        /*
            The parameter <Command> is actually define gthe type of object to be pass into this <Handler>
        */
        public class Handler : IRequestHandler<Command>
        {
            /*
                We be using the the Auto Mapper to track one <Activity> to another <Activity> object
                We need to inject the Auto Mapper into this class by the constructor
            */
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                // throw new NotImplementedException();
                /*
                    We be updating a specific <Activity>
                    Therfore we get that specific <Activity> from the Database and assign it to a variable first.
                    We then try to edit the respective [value] from the [key]
                */
                // Get the specifc <Activity> with the Id and assgn it to a variable first
                var tempActivity = await _context.Activities.FindAsync(request.Activity.Id);
                /* 
                    Updating the Title of this specific <Activity>
                    <request.Activity.Title ?? tempActivity.Title;> This syntax mean we trying to update the Title, but the user may or may not have sent this request. Then the "??" means will assign a Title if the the request NULL.
                    We can use the same approach to all the prperties in the <Activity> but there ia a better way to do it using the Auto Mapper package

                    We be using the the Auto Mapper to track one <Activity> to another <Activity> object

                */
                // tempActivity.Title = request.Activity.Title ?? tempActivity.Title;

                // This means the Auto Mapper will take the object from <Activity> and map it with the variable object tempActivity
                _mapper.Map(request.Activity, tempActivity);
                /*
                    After doen with the above code for mapping of <Activity> object, we need to add the Auto Mapper to the Program,cs as a service
                */

                await _context.SaveChangesAsync();
            }
        }
    }
}