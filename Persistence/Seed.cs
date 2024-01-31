/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
using Domain;

namespace Persistence
{
    // Class name Seed
    public class Seed
    {
        /*
            Static => means static method => Static method is a method that we can use without creating a new instance of the Class. Basically this mean we can acces to this <SeedData> method without creating an object of this <Seed> class
            We just need apply the Class Name and apply the static method. For example: ClassName.SetaicMethod() => Seed.SeedData(whichDatabase) 

            Take note:
            This static method is define as a  async Task method. Which means whoever is calling this method must place a <await> keyword before call it.

            Notice this SeedData() method is an "async". This means whoever cllaing this method, need to place an "await" keyword infront of this method before it call properly execute.

            Whichever code calling any "async" method will receive a notification to as "delegate" on the function task have been completed. Then the code will move on from there.
            Basically for this case, once the database is updated, the database is going to respond and then move on from there.

            What is <async await method>?
            Reference: Async and Await in C#
            https://www.geeksforgeeks.org/async-and-await-in-c-sharp/
            An async keyword is a method that performs asynchronous tasks such as fetching data from a database, reading a file, etc, they can be marked as “async”. Whereas await keyword making  “await” to a statement means suspending the execution of the async method it is residing in until the asynchronous task completes. After suspension, the control goes back to the caller method. Once the task completes, the control comes back to the states where await is mentioned and executes the remaining statements in the enclosing method.

        */
        public static async Task SeedData(DataContext context)
        {
            /*
                To check whether is there any Activities in the database. 
                The <Activities> refer to the data records in the database table

                If there Activities exist then we do not want to seed more or creating more data record in the database table

                If there isn't any <Activities> in the database table, then will create a list of <Activities> to be populate into the database table.
            */
            if (context.Activities.Any()) return;
            
            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                new Activity
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new Activity
                {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new Activity
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new Activity
                {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Another pub",
                },
                new Activity
                {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Yet another pub",
                },
                new Activity
                {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Just another pub",
                },
                new Activity
                {
                    Title = "Future Activity 6",
                    Date = DateTime.UtcNow.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new Activity
                {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new Activity
                {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema",
                }
            };

            /*
                To add range of Activites (The list of activities) into the memory (Have not store into the database yet at this time)
            */
            await context.Activities.AddRangeAsync(activities);
            /*
                SaveChangesAsync() will actually save the list of activities into the database from the memory
            */
            await context.SaveChangesAsync();

            /*
                Where and how to execute this eeding database process?
                Recall the the <Program.cs> is the starting point of the application, therefore will execute this process in the <Program.cs> after the database and tables have been created which mean we have <Migrated the Database>
            */
        }
    }
}