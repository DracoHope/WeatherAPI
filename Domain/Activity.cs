/*
    All the below import are not use anymore after Dotnet 6
    For Dotnet 7 and above, the <ImplicitUsings>enable</ImplicitUsings> setting in the <Domain.csproj> file will help us to import some of the commonly use library instead

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
*/

/*
    We now new to create the Entity Class object for this project.
    The Entity is also known as "Model" as well. They are very much similar refer to the same thing.
    We can store our <Entity> object into our database.
    All Entity Class will have their properties declare in its class

    The Domain folder will conrain the Class object for this project and all the properties for this entity we be dealing with for thi sproject

    This is a social media project, hence will create those properties accordingly in this Class Object

    The properties are also refer as <props>

    What do we need for a social media posting?

    We be using the Dotnet Entity Framework for our database
    We need to create, store and query our database
    1. We need to add in the Entity Framework Db Context
    2. The Class name of this <Entity> will be use as the database table name automatically. Hence a database table of name <Activity> will be created by the Entity Framework
    3. Each of teh properties will be the columns name or field in the database table
    4. We be using <An Object Relational Mapper> for the Entity Framework. This will provide an obstraction away from our database. Therefore we do not need to write "Queries" directly against teh Database itself. Basically it means we need not write "queries" anymore with the new Entity Framework, instead the <CRMs> or the <Object Relational Mapper> will handle the "Queries" at the background. But How?
    5. But How?
    We now no need to proficent in SQL Server, SQL lite or PostgreSQL now actually since no "Queries" is quite necessary for simple project with the Entuty Framework.
    Basically we do not need to write any "SQL Queries".
    Instead we only need to know some C# syntax for retriving our data from our database instead of the "SQL Queries"
    6. We be using the SQLLite database for this project, basically for testing puposes.
    We be using the <ORM> when we comes to publishing and using a production to switch to other SQL server easily with using of <ORM> with minimal code changes during the production.
    7. The SQLLite database is within the C# Dotnet Framework.
    So we do not need to install any software to support it. Basically just need to add SQLLite into our project.
    8. The SQLite will be added in in the <Persistence> Folder (Project) which will handling the Database. We need <Nu Get Galley> extension to help us. Installing SQLite using gthe <Nu Get Galley>
    8a. Press <F1> and get <Nu Get Galley>
    8b. Type "Microsoft.EntityFramework"
    8c. Search for <Microsoft.EntityFrameworkCore.Sqlite>, we need to install this Sqlite package for our <Persistence> project. Take note: We need <Microsoft.EntityFrameworkCore.Sqlite> not <Microsoft.EntityFrameworkCore.Sqlite.Core> which will give us many errors.
    8d. Please take note: We need to install the same <Runtime Donet Version> which we are running our project. We are running on Dotnet v8.0 then need to select Dotnet v8 for any packages to be install else it may run correctly or having errors.
    8e. 





*/
using System.ComponentModel;

namespace Domain
{
    public class Activity
    {
        /*
            All the declare properties need to be public for this project, so that we can access it globally

            All properties need to have getter and setter for this project. This will allow us to set and get the value from the variables.

            If at anytime we see any error about <string?> or <string nullable> error, we need to goto the <Domain.csproj> file ans set this <Nullable>disable</Nullable> from "enable" to "disable" 
        */

        /*
            Guid => Globally unique Identfier 

            We will later add  and create a Database for this project. Therefore the name of this variable is very important of type Guid
            We need to call it specifically <Id> since we want it to be "Primary Key" for our Database.
            By giving the variable name <Id> the Entity Framework will recognize this should be gtghe "Primary Ket" for the Database

            Looking into the <20240128193118_InitialCreate.cs> file in the <Persistence/Migrations> Folder after the dotnet ef Migration have been done successfully.
            Take note: The <Activity> class must declared variable name to be "Id" and of "Guid" datatype before the dotnet ef Migrations will assign it to be the Primary Key. If using any random variable name will cause many error while creating the table while assigning the Primary Key

            Another way of declaring a Property to be the Primary Key is to using the <[Key]> before the declaration of random variable name to be the Primary Key as shows in the example below:

            // Need may work as well, but have not try it yet
            [Key]
            public Guid MyPrimaryKey { get; set; }
        */
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }
        
    }
}