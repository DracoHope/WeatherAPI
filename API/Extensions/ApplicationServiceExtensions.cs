using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    /*
        This <ApplicationServiceExtensions> class will contain all the Services which is current added into the Program.cs.
        Eventually all these services such as MediatoR, CORS, etc ... will be remove from the Program.cs

        We define this <ApplicationServiceExtensions> as a statis class so that whoever need the Services need not create a new instance of this class. 
    */
    public static class ApplicationServiceExtensions
    {
        /*
            Create the Extension method 
        */
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            /*
                *** Add the <DbContect> Srrvice to the Application

                Adding the <DbContext> to the application as a Service
                1. We need to access the <DbContect> from the <Persistence> project folder from this <API> project. But we are currently is in API project folder. 
                2. We already added the Entity Framework into the <API> project. So we need to "Transitive"(Transfer) these Entity Framework dependency from this application <API> project to the <Persistence> project
                3. We need to execute the <Dotnet restore> command at the solution level to make the Entity framework available from the <API> project to the <Persistence> project.
                4. Open up a new terminal at the main solution folder, which is the <WeatherAPI> folder
                PS D:\App w .Net Core and React\WeatherAPI>
                5. Type in the commans <dotnet restore>
                PS D:\App w .Net Core and React\WeatherAPI> dotnet restore
                Determining projects to restore...
                All projects are up-to-date for restore.
                PS D:\App w .Net Core and React\WeatherAPI> 
                This should make the Entity Framework available for both <API> and <Persistence> project
                6. How to add the <DbContect> Service?
                a. Auto complete help for <builder.Services.AddDbContext>
                b. Add the class name of the database <DataContext>
                c. Add the parameter for the database. using the lamda expression for the option. Just follow the convention starting with "opt =>". Write the implementation code into the curly braces starting with <opt.>
                d. then select the <UseSqlite> from the auto complete dropdown option.
                e. pass in the database connection with the help of the auto complete starting with "builder" => opt.UseSqlite(builder.Configuration.GetConnectionString()
                f. Pass in a variable name for the database connection string. The commonly use variable name is "DefaultConnection"
                g. The completed command code for adding the <DbContext> database Service is as below: 
                builder.Services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
                7. Where do we declare our Database connection string?
                We declare the SQLite database connection string in the <appsettings.Development.json> as a key value pair as show below:
                "ConnectionStrings": {
                "DefaultConnection": "Data Source=weatherapi.db"
                }
                });
                "ConnectionStrings" => indicate it's a database connection string
                "DefaultConnection" => is the variable name we define here as a service for the database connection string
                "Data Source=weatherapi.db" => this is the actual SQLite database connection string. SQLite store data in the form of a file with an extension ".db". We declare a file name <weatherapi.db> for this SQLite database
            */
            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            /*
                Add the CORS Policy as a service
                Basically we added a service to the API project to tell the <API> that don't care about CORS Header and Method as long as the HTTP request comes from  WithOrigins("http://localhost:3000/");

                Beside adding this CORS Policy service here. We still need the Middleware to support this CORS Policy.

                The order to add the Middleware for this CORS Policy is important. We need to add it just before the <app.UseAuthorization()> below 
            */
            services.AddCors(opt => {
            opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
                });

            /*
                ** Code changes at Section 4 - 020224 **
                Section 4: We need to add the Mediator Service 
                The Mediator will enble the communication between the <API> and <Application> for the Databse access.

                The <API> no longer need to have direct access to the database.
                Instead the <API> will send command to the <Application> then the <Application> will sent the <Query> to the Database.
                The <Application> will notify <API> once the data have been received

                The eMediator just need to know where to find the Query Handler. Therefore need to pass in the 
            */
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(QueryHandler.Handler).Assembly));

            /*
                Adding Auto Mapper as a service to the program.c for the project
            */
            services.AddAutoMapper(typeof(Application.Core.MappingProfiles).Assembly);

            return services;

        }
    }
}