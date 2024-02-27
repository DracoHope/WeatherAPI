/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/

/*
    Section 4: This clss will return the details of a specific Activity with a given id

*/
using Domain;
using MediatR;
using Persistence;
using static Application.Activities.QueryHandler;

namespace Application.Activities
{
    public class DetailsActivity
    {
        /*
            This class is going to fetching data from database similar to the QueryHandler in the same project folder

            This class will need to have DataContext to access the database and the specific id of interest

        public class QueryActivity : IRequest<Activity>
            This class <public class QueryActivity : IRequest<Activity> means we created a Class name QueryActivity which a type of <IRequest> from MediatoR => Tale note: the <IRequest> is an interface which will return a data of type <Activity>
        */
        public class QueryActivity : IRequest<Activity>
        {
            /*
                We need to specify the id of the Activity of  interested 
                Take note:
                The specific Id datatype must be the same as in the SQLite Database which is of Guid Datatype

                Just type "prop" the VS code will automatically give option to create the Getter and Setter
            */
            public Guid SearchId { get; set; }
        }

        public class SearchActivityHandler : IRequestHandler<QueryActivity, Activity>
        {
            public DataContext _context;
            public SearchActivityHandler(DataContext context)
            {
                _context = context;
            }

            /*
                This is the Interface for this task

                What is the CancellationToken about?
                CancellationToken is use to cancel the database access operation when user terminate the operation or for some reason the process need to terminated maybe the user end browser is off. 
                For example if the database take very long to retriving a large list of data from the database which took too much of a time. The user might just cancel the process or close the browser since the data have not been display for quite sometime.
            */
            public async Task<Activity> Handle(QueryActivity request, CancellationToken cancellationToken)
            {
                // throw new NotImplementedException();
                return await _context.Activities.FindAsync(request.SearchId);
            }
        }

    }
}