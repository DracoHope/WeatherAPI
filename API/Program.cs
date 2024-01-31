using Microsoft.EntityFrameworkCore;
using Persistence;

/*
    Starting Application Command
    1. dotnet watch => This command uses the "Hot Reload" to monitor any changes of any files and apply those changes. But it is not always the case as in Dotnet v7.0
    2.dotnet watch --no-hot-reload => Instead we be using this command to apply the "File Watcher" which is more rliable  then the "Hot Reload"
    PS D:\App w .Net Core and React\WeatherAPI\API> dotnet watch --no-hot-reload
    dotnet watch 🚀 Started
    Building...
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (14ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        SELECT COUNT(*) FROM "sqlite_master" WHERE "name" = '__EFMigrationsHistory' AND "type" = 'table';
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        SELECT COUNT(*) FROM "sqlite_master" WHERE "name" = '__EFMigrationsHistory' AND "type" = 'table';
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        SELECT "MigrationId", "ProductVersion"
        FROM "__EFMigrationsHistory"
        ORDER BY "MigrationId";
    info: Microsoft.EntityFrameworkCore.Migrations[20405]
        No migrations were applied. The database is already up to date.
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        SELECT EXISTS (
            SELECT 1
            FROM "Activities" AS "a")
    info: Microsoft.Hosting.Lifetime[14]
        Now listening on: http://localhost:5000
    info: Microsoft.Hosting.Lifetime[0]
        Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
        Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
        Content root path: D:\App w .Net Core and React\WeatherAPI\API
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Building this API project app
var app = builder.Build();

// Configure the HTTP request pipeline.
/* 
    Basically the below will define the necessary HTTP Middleware for this API project
    The pipeline is just to indicate the HTTP request will be in and out from this "pipeline"

*/
if (app.Environment.IsDevelopment())
{   
    /* 
        Swagger is actually very similar to Postman which can use to test web pager and make API call to retrive information

        This set of Middleware will only apply during the development only as indicated in the if condition

        Swagger will only run in development mode only

        Swagger 
        This API project actually do not have its own web page because it's just provide Weather APi request and return teh result
        By using Swagger, we can get some information of this API application
        
        From the <launchSettings.json> we set the "applicationUrl": "http://localhost:5000"
        But when we try to access "applicationUrl": "http://localhost:5000", we got error which is <Page not Found Error> since this API app doesn't return a web page. By using Swagger, we can access some information of this API app API End Point and execute/test the HTTP request for the API. 
        Swagger is very similar Postman which can sent HTTP to the application server
        "applicationUrl": "http://localhost:5000"/swagger
        This will return the default web page by Swagger and display some information from this API app 
    */
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Not using this middleware for this API project
// app.UseHttpsRedirection();

// This UseAuthorization Middleware will take care of the Authorization for authentication. But we do not have any credentia authorization setup for this API project for now, so not useful for now
app.UseAuthorization();

/* 
    The main function of this <MapControllers> Middleware is to map the controller routing for the HTTP request to it's designated End Point.
    This API app server as a Weather API provider. It will return the weather information when there is any HTTP request to it's <GetWeatherForecast> End Point which is registered in the <WeatherForecastController.cs> file inside the controller folder

    This MapControllers will handle the mapping of route in the Program.cs for this API project

    This MapControllers will handle the controller route indicated in the WeatherForecastController.cs which is in the <controller> folder.

    The program class registers those End Point with the application whenever a HTTP request coming in

    Whenever a HTTP request come in, the MapControllers will know direct them to the desinated End Point in the controller
*/
app.MapControllers();

/*
    "Database Creation" process will be coded here
    The "Database Creation" process will create the database if it doesn;t exist or update the database base on the current Migration which have been done completed befor this is possible.

    We need the dependency from the "DataContext Services" which we have added to this <API> project at the top as shoe below:

    builder.Services.AddDbContext<DataContext>(opt => {
        opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    We be using the dependency injection method such that this <Program.cs> can access to it. But we do not the <Denpency Injection> in this <Program.cs> but we still need to get access to the <DataContext> Service.

    23. Start coding the "Database Creation"
    using var scope = app.Services.CreateScope();
    <using var scope = app.Services.CreateScope();> means we are creating a scope using the <CreateScope()> for accessing the Services. If the process completed, this particular method <CreateScope()> anything within this this "scope" will going to be dispose or destroy clean up the memory for this "scope" execution. This is a good practice for a programmmer to dispose off any unuse object. Actually the the Garbage Collector will do the same task for us if we just ignore it so this is not really a problem.
    Basically we are creating a "scope" for accessing a service 

    24. Actually all the listed services are going to be "scoped" to the HTTP request that comes into this <API> application. 
    Whenever the <API> application received a HTTP request, it will create services and get access to a services then can access to the databse to make a query against it. 
    Basically when this <API> application received a HTTP request, it will create an instance of the <DataContext> service and then get acess to query the database.
    When the HTTP request have been done, then this particular instance of <DataContext> will be dispose from the memory of the system.

    In summary we will create a "scope" <app.Services.CreateScope()> to access to the a Service
    
});
*/
//Create a "scope" to sccess the services
using var scope = app.Services.CreateScope();
// Accessing the services with this paricular scope
var services = scope.ServiceProvider;
/*
    Start "Database Creation" code here

    We be using the <Try catch> syntax method for creating the database. This will help to catch any exception when the "Databse Creation" process fail.


*/
try
{
    // Get the Service provided by the <DataContext> to access the Database
    var context = services.GetRequiredService<DataContext>();
    /*
        This <Migrate()> method will have the same function as the <dotnet ef> database command
        Function description:
        Applies any pending migration for the context to the databse. Will create the databse if it does not already exist.

    */
    /*
        This method is working but we will try to use the Migration with the async await version which is => MigrateAsync()
    */
    // context.Database.Migrate();
    /*
        We be using another async await vrsion of MigrateAsync() method instead pf the normal MigrateAsync() 
    */
    await context.Database.MigrateAsync();

    /*
        After the <Migration> have successfully completed, we should already have the database and table been created. 
        We can now Seeding or populate mock data into the table for testing purpose. 
        We can called the static method from the <Seed> calss for this process.

        
        Take note:
        This <SeedData()> method is actually define as an <async Task> function, therefore we need to use the <await> keyword before we start call ing this method.
    */
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    /*
        <ILogger<Program>>
        Using the <ILogger> to log the error for the <Program> Class object
    */
    var logger = services.GetRequiredService<ILogger<Program>>();
    // Using the "logger" object to display the error message if happen
    logger.LogError(ex, "An error occured during migration!");
}


// Start tgo run this API project application
app.Run();
