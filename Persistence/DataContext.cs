/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
// using System.Diagnostics;
using Domain;
using Microsoft.EntityFrameworkCore;

/*
    1. Create this <DataContext> class in the Persistence Folder
    2. We need to derive this <DataContext> class from the <DDbContext> from the Entity Framework
    3. Generate the constructor for this class
    4. Add in the database connection into the option field in the <DataContext> class constructor
    5. Create the <Db set> and these represent the tables in the database. These <Db set> are the prop or properties for this <Dbcontext> class. Basically its the properties or variable with Getter and Setter with it.
    6. How to create properties with the auto complete?
    6a. Type <prop> with the Class
    6b. Select <prop> from the dropdown
        this will auto generate the default propertirs as <public int MyProperty { get; set; }>
    6c. Edit the auto generate default property according by changing the datatype, property name, Getter and Setter accordingly

*/

namespace Persistence
{
    /*
        This <Persistence> project is the SQLite database using the Entity Framework.
        We created a Class name "DataContext"
        We need to "Derive" from DbContext Class from the Entity FrameworkCore
        By using auto complete function, we can find <DbContext> and will automatically inset the import for <using Microsoft.EntityFrameworkCore;>

        Basically the <DbContext> are use to query and save instances of our entities.
        DbContext is a combination of the Unit of Work and Repository patterns.
        We do not actually need to know what is "Unit of Work and Repository patterns" because the <DbContext> is already using it in background

        What is Unit of Work and Repository patterns?

        
        We need to tell our application we be using this <DataContext> class. But How?

        Telling the application we be using the <DataContext> class
        We need to indicate our <DataContext> in our <API> folder in the main <Program.cs> file.
        We want to add this <DataContext> class as a service to pur application.
        We can add this under the <// Add services to the container.> comment in the program.cs file. But the order of adding this service is not really important in the program.cs actually. only just for simplicity of it we just add in under the comment if really to.
    */
    public class DataContext : DbContext
    {
        /*
            We need to pass in the Database connection string into the <option> parameter
            We need to generate constructor for this <DataContext> class
            1. Hightlight and right click on the full class name
            2. select <Generate constructor 'DataContext(options)'>
            3. The below constructor will be created with an <DbContextOptions options> which will be useful.
            4. The <DbContextOptions options> is use to pass in the "Database Connection String" to it.
        */

        /*
            We need to pass in the Database connection string into the <option> parameter

            7. Where do we declare our Database connection string?
            We declare the SQLite database connection string in the <appsettings.Development.json> as a key value pair as show below:
            "ConectionStrings": {
            "DefaultConnection": "Data Source=weatherapi.db"
            }
            });
            "ConnectionStrings" => indicate it's a database connection string
            "DefaultConnection" => is the variable name we define here as a service for the database connection string
            "Data Source=weatherapi.db" => this is the actual SQLite database connection string. SQLite store data in the form of a file with an extension ".db". We declare a file name <weatherapi.db> for this SQLite database
        */
        public DataContext(DbContextOptions options) : base(options)
        {
            // We can leave the content here empty for now
        }

        /*
            *** <DbSet> Represent the Table in the Database
            1. Create the <Db set> and these represent the tables in the database. These <Db set> are the prop or properties for this <Dbcontext> class. Basically its the properties or variable with Getter and Setter with it.
            2. How to create properties with the auto complete?
            2a. Type <prop> with the Class
            2b. Select <prop> from the dropdown
                this will auto generate the default propertirs as <public int MyProperty { get; set; }>
            2c. Edit the auto generate default property according by changing the datatype, property name, Getter and Setter accordingly
            3. We need to create <Dbset> datartyoe for our tables
            Change the default datatype to <Dbset> and indicate the Entity (Model or Domain we are working with. Fpor this case we recall that our <Domain> contain a <Activity> Entity class which represent the database table) \
            4. We also need to indicate the Entity(Model) we be working with. For this case we need to work with the <Activity> Entity
            DbSet<Entity to work with> => DbSet<Activity>
            5. Basically we will be creating a table with all the properties stated in the <Activity> class in the SQLite Database. The table name will be variable name for this <DbSet> variable => we name it as <Activities>
            Hence a table name <Activities> will be created with all the properties stated in the <Activity> class.
            6. Please take note:
            The name "Activity" is a keyword in the Dotnet Framework. So we must be sure and tell the program we be using the <Activity> class in our <Domain> folder specifically indicate <using Domain>
        */

        public DbSet<Activity> Activities { get; set; }

        /*
            Finally after creating the follow:
            1. <Persistence> project create <DataContext> Calss
            2. Derive this <DataContext> class from the <DbContect> actually from the Entity Framework 
            3. Install the SQLite package to <Persistence> project
            4. Create the <Dbset> for the SQLite Database table associate with the <Domain> project Entity(Model)
            5. Add this Database service into the application in the <API> project by coding into the <program.cs>
            But before this is possible, we need to share the Entity Framework between the <API> project and the <Persistence> project by executing the <dotnet restor> command at the command prompt at teh highest level which is the <weatherAPI> Project folder which contain all the related sub projects including <API> and <Persistence>
            6. After the <dotnet restore> command successfully completed, we can add in the code for the database service in the <program.cs>
            7. Declare the database connection string in the <appsettings.Development.json> so that the database connection is globally accessible by the application, <API> and the <Persistence> project  
            8. Finally we need link up our database with the final step know as <Entity Framework Code Migration> => because we coded all our model(Domain => table), database(Persistence => SQLite database), service added(API) and connection string(weatherAPI) define done then finally we are here.
            9. <Entity Framework Code Migration> basically create the code that generate the schema base on the <Domain>, database <Persistence> and database connection string.
            10. How to generate the schema for our database?
            We need a tool from the donet entity framework which help to do that.
            11. type <dotnet tool list -g> in command prompt to see all available tools as show below:
            PS D:\App w .Net Core and React\WeatherAPI> dotnet tool list -g
            Package Id      Version      Commands
            -------------------------------------
            PS D:\App w .Net Core and React\WeatherAPI>

            The above result indicate that no dotnet tool was available. We actually need the <dotnet-ef> tool from the MS Dotnet to perform the database migration.
            12. Usually we can't install this <dotnet-ef> package from the Nu Get Gallery. (But some how when I type <dotnet-ef> into the Nu Get I found some result of <dotnet-ef>) The recommended method of installing this <dotnet-ef> is through command prompt terminal. We can type and search from internet => https://www.nuget.org/packages/dotnet-ef    We be direct to the <dotnet-ef> resource page. We just need to copy the command line from the <dotnet-ef> resource page and execute it in our project terminal. Make sure the Dotnet version is the same as our project runtime version. My current project runtime version is Dotnet v8.0. The command is
            <dotnet tool install --global dotnet-ef --version 8.0.1>
            PS D:\App w .Net Core and React\WeatherAPI> dotnet tool list -g
            Package Id      Version      Commands
            -------------------------------------
            PS D:\App w .Net Core and React\WeatherAPI> dotnet tool install --global dotnet-ef --version 8.0.1
            
            Install <donet-ef> completed
            PS D:\App w .Net Core and React\WeatherAPI> dotnet tool install --global dotnet-ef --version 8.0.1
            Skipping NuGet package signature verification.
            You can invoke the tool using the following command: dotnet-ef
            Tool 'dotnet-ef' (version '8.0.1') was successfully installed.
            PS D:\App w .Net Core and React\WeatherAPI> 

            Check for Dotnet Framework Tool again
            PS D:\App w .Net Core and React\WeatherAPI> dotnet tool list -g                  
            Package Id      Version      Commands 
            --------------------------------------
            dotnet-ef       8.0.1        dotnet-ef
            PS D:\App w .Net Core and React\WeatherAPI> 
            The above result shows the <dotnet-ef> been found
            <dotnet-ef       8.0.1        dotnet-ef>

            Please take note:
            The original <dotnet-ef> install command is <dotnet tool install --global dotnet-ef --version 8.0.1>

            If we just need to reinstall or update this <dotnet--ef> maybe from v8.0.1 to v8.0.2 in near future when available then just change the "install" to "update" as show below
            <dotnet tool update --global dotnet-ef --version 8.0.2>

            13. To check whether the Entity Framework <dotnet--ef> is working?
            Just type <dotnet ef> onto the project terminal. <PS D:\App w .Net Core and React\WeatherAPI> dotnet ef> we should be able to see the information an logo for this <dotnet-ef>
            The available command for this <dotne-ef> Entity Framework will be show

            PS D:\App w .Net Core and React\WeatherAPI> dotnet ef

                        _/\__       
                ---==/    \\      
            ___  ___   |.    \|\    
            | __|| __|  |  )   \\\   
            | _| | _|   \_/ |  //|\\ 
            |___||_|       /   \\\/\\

            Entity Framework Core .NET Command-line Tools 8.0.1

            Usage: dotnet ef [options] [command]

            Options:
            --version        Show version information
            -h|--help        Show help information
            -v|--verbose     Show verbose output.
            --no-color       Don't colorize output.
            --prefix-output  Prefix output with level.

            Commands:
            database    Commands to manage the database.
            dbcontext   Commands to manage DbContext types.
            migrations  Commands to manage migrations.

            Use "dotnet ef [command] --help" for more information about a command.
            PS D:\App w .Net Core and React\WeatherAPI>

            The most important command for this Entity Framework  are
            Commands:
            database    Commands to manage the database.
            dbcontext   Commands to manage DbContext types.
            migrations  Commands to manage migrations.

            Basically the <dotnet-ef> for Dotnet Database function.

            14. Database migration using Entity Framework <dotnet-ef>
            Type command <dotnet ef migrations> onto the project solution folder terminal. The <weatherAPI> folder is the solution project folder which contain all the other sub project folder.
            <D:\App w .Net Core and React\WeatherAPI> dotnet ef migrations add InitialCreate -s API -p Persistence>
            <add InitailCreate> => add is the command. "InitialCreate" is the variable name for the migration
            <-s API> => -s is the switch command, it means the startup project(We need to indicate where is the startup project for the project) follow by the project folder name of the startup project which is <API> for this <weatherAPI> project
            <-p Persistence> => -p is the switch command which require the location of the <DataContext> for this project which is the <Persistence> project folder. 

            15. executing the command from the project solution folder in the terminal
            <dotnet ef migrations add InitialCreate -s API -p Persistence> 
            < D:\App w .Net Core and React\WeatherAPI> dotnet ef migrations add InitailCreate -s API -p Persistence>
            
            PS D:\App w .Net Core and React\WeatherAPI> dotnet ef migrations add InitialCreate -s API -p Persistence
            Build started...
            Build succeeded.
            Your startup project 'API' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct, install the package, and try again.
            PS D:\App w .Net Core and React\WeatherAPI> 

            We encounter an error while executing the dotnet ef command. The error show that we need to install another require dotnet package <Microsoft.EntityFrameworkCore.Design> into the <API> project folder
            Let install this <Microsoft.EntityFrameworkCore.Design> from the Nu Get Gallery

        16. Executing the command <dotnet ef migrations add InitialCreate -s API -p Persistence> again after installed require package into API project folder
        PS D:\App w .Net Core and React\WeatherAPI> dotnet ef migrations add InitialCreate -s API -p Persistence
        Build started...
        Build succeeded.
        Done. To undo this action, use 'ef migrations remove'
        PS D:\App w .Net Core and React\WeatherAPI> 

        Database dataContext migration successfully completed

        17. What exactly did this <dotnet ef migrations add InitialCreate -s API -p Persistence> done?
        We notice there ia an additional folder call <Migrations> been created inside the <Persistence> project folder.
        This <Migrations> folder contain 3 files.
        a. There are two files are useful for Entity Framework to be able to roll back the migration to the previous one.
        they are <20240128193118_InitialCreate.Designer.cs> and <DataContextModelSnapshot.cs> 
        b. looing into the <20240128193118_InitialCreate.cs>, we see there is a <InitialCreate> class which derive from the <Migration>. Notice the <InitialCreate> class name is actually the variable name when we execute the migration command in the terminal. There are 2 method in this <InitialCreate> class, Up and Down methods.
        Looking in to the <20240128193118_InitialCreate.cs> goto <20240128193118_InitialCreate.cs>

        Method <Up> => Create the table base on the <Activity> Class Entity in the <Domain> project. The properties in the <Activity> Class will be generated as the column of the table. The table name will be "Activities"

        Method <Down> => Basically this <Down> method only function is to drop this <Activities> table

        !8 What next after the dotnet ef Migration have been compeleted?
        The next step is <Database Creation> to create the Database base on the dotnet ef Migration. The Migration process will create the <20240128193118_InitialCreate.cs> files in the <Migrations> folder. This file will generate the code for creating the tables for the databse base on the <Activity> class in the <Domain> project also know as the Entity or Model for this project

        19.How to Create the Database with the <dotnet ef>?
        looking into the <dotnet ef> tool again by typing <dotnet ef> command again into the command prompt at the solution folder
        We can see the info below for the Commands:

        database    Commands to manage the database.
        dbcontext   Commands to manage DbContext types.
        migrations  Commands to manage migrations.

        Notice beside "Migrations" we also have "database" command as well.

        20. Exploring <dotnet ef> database
        type command <dotnet ef database>
D:\App w .Net Core and React\WeatherAPI> dotnet ef database
Usage: dotnet ef database [options] [command]
Options:
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

    Commands:
    drop    Drops the database.
    update  Updates the database to a specified migration.

    Use "database [command] --help" for more information about a command.
    PS D:\App w .Net Core and React\WeatherAPI> 

    Notice we have two command as "drop" and "update" the database using the <dotnet ed> tool
    "update" command function is specify as "Updates the database to a specified migration" => this means this command will look into the latest Migration or all of the Migration (It is possible to have multiple Migration) then will create or update the Database to those current Migration been done.

    21. We will be doing this "Creat Databse" process in the terminal. Instead we be doing this by coding such that whenever we restart our application. it will effectively execute this <dotnet ef> command as well to create if the database doesn't exist or update the databse base on the current Migrations.

    22. Where should we place the <dotnet ef> code for the "Database creation"?
    The "Database Creation" code will be place inside the application Which will start first.
    The Program Class <Program.cs> will always be the first to be executed. Therefor the code for the "Database Creation" must be executed insde this <Program.cs> as well just before the <app.Run()> in the <Program.cs>
    The <Program.cs> is at the most top solution level with the main <weatjerAPI> project folder. The <waetherAPI> is the main solution project folder which contain all other sub project folder in this project.
    Looking into the <Program.cs> file for more details.

    24. Finally we have done all the necessary coding for this project with the final Coding done in the <Program.cs> for the "Database Creation or Updating"

    25. We will restart the application using the <ditnet watch> command. The starting for this particular project is actually the <API>. Therefore we need to navigate to this <API> folder in our Terminal.
    PS D:\App w .Net Core and React\WeatherAPI> cd API 
    PS D:\App w .Net Core and React\WeatherAPI\API> dotnet watch
    dotnet watch 🔥 Hot reload enabled. For a list of supported edits, see https://aka.ms/dotnet/hot-reload.
    💡 Press "Ctrl + R" to restart.
    dotnet watch 🔧 Building...
    Determining projects to restore...
    All projects are up-to-date for restore.
    Domain -> D:\App w .Net Core and React\WeatherAPI\Domain\bin\Debug\net8.0\Domain.dll
    Persistence -> D:\App w .Net Core and React\WeatherAPI\Persistence\bin\Debug\net8.0\Persistence.dll
    Application -> D:\App w .Net Core and React\WeatherAPI\Application\bin\Debug\net8.0\Application.dll
    API -> D:\App w .Net Core and React\WeatherAPI\API\bin\Debug\net8.0\API.dll
    dotnet watch 🚀 Started
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (77ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        PRAGMA journal_mode = 'wal';
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (34ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        CREATE TABLE "__EFMigrationsHistory" (
            "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
            "ProductVersion" TEXT NOT NULL
        );
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        SELECT COUNT(*) FROM "sqlite_master" WHERE "name" = '__EFMigrationsHistory' AND "type" = 'table';
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        SELECT "MigrationId", "ProductVersion"
        FROM "__EFMigrationsHistory"
        ORDER BY "MigrationId";
    info: Microsoft.EntityFrameworkCore.Migrations[20402]
        Applying migration '20240128193118_InitialCreate'.
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        CREATE TABLE "Activities" (
            "Id" TEXT NOT NULL CONSTRAINT "PK_Activities" PRIMARY KEY,
            "Title" TEXT NULL,
            "Date" TEXT NOT NULL,
            "Description" TEXT NULL,
            "Category" TEXT NULL,
            "City" TEXT NULL,
            "Venue" TEXT NULL
        );
    info: Microsoft.EntityFrameworkCore.Database.Command[20101]
        Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
        INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
        VALUES ('20240128193118_InitialCreate', '8.0.1');
    info: Microsoft.Hosting.Lifetime[14]
        Now listening on: http://localhost:5000
    info: Microsoft.Hosting.Lifetime[0]
        Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
        Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
        Content root path: D:\App w .Net Core and React\WeatherAPI\API
    dotnet watch ⌚ File changed: D:\App w .Net Core and React\WeatherAPI\Persistence\DataContext.cs.
    dotnet watch ⌚ No hot reload changes to apply.

    From the above terminal message as show below:
    CREATE TABLE "__EFMigrationsHistory" (
            "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
            "ProductVersion" TEXT NOT NULL
        );
    
    The Database Migration will create a table "__EFMigrationsHistory" => this is our database is going to use to track which migration have been applied to the current database. Basically this is a history table for Migration purpose, for track which Migration files been applied.

    This is the actual table creation table with the follow columns stated in the <20240128193118_InitialCreate.cs> file for this project in the </Persistence/Migrations> folder as follow:
    CREATE TABLE "Activities" (
            "Id" TEXT NOT NULL CONSTRAINT "PK_Activities" PRIMARY KEY,
            "Title" TEXT NULL,
            "Date" TEXT NOT NULL,
            "Description" TEXT NULL,
            "Category" TEXT NULL,
            "City" TEXT NULL,
            "Venue" TEXT NULL
        );

    This message means it will insert an entry into the Migration history table with the value of migration <VALUES ('20240128193118_InitialCreate', '8.0.1');> which seem to be the Migration file as we have known.
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
        VALUES ('20240128193118_InitialCreate', '8.0.1');

    The message finally show that the Application started as follow:
    Now listening on: http://localhost:5000
    info: Microsoft.Hosting.Lifetime[0]
        Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
        Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
        Content root path: D:\App w .Net Core and React\WeatherAPI\API

    We can finally test our SQLite database by search for <SQLite> from the F1 search bar. Then select the <Open Database> option
    The <SQlite: Open Database> will let us have a peek of the SQLite file system. (SQLite actually store the data in a file) 
    We have name our <SQLite> databse file as <weatherapi.db>
    We should be able to see this file name when we select the <SQlite: Open Database> option.
    For now seem nothig happen?
    But we can goto our project explorer, we should have a SQLite Explorer inside our Project.
    The folder name <SQLITE EXPLORER> and we can se and select the database file <weatherapi.db> file
    Click the <weatherapi.db> file, we can see there are 2 table been created
    1. Activities => the actual data the project be working on
    2. __EFMigrationsHistory => the history tracking table which will only be updated whenever a Database migration been executed. For this case whenever the application have been restart since the <database creation> process code is in the <Program.cs>

    26. As a start, we do not have any data in our database yet for now.
    We may want to populate some data for testing.
    We will create aome "seed" data and put it into the <Activities> table.
    This process is known as "Seeding data to the database".
    Procedure for "Seeding data base"
    1. Goto the <Persistence> project folder
    2. Create a new Class => <Seed.cs>
    3. Start creating some data in the <Seed.cs>

    */


    }
}