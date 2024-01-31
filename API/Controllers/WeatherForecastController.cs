using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/*  
    This <[ApiController]> attribute are used to serve HTTP API response

    Each controller has a <Route>. For this case the <Route> is "[controller]"
    [Route("[controller]")].
    This will enable this API app/project knows where to redirect any HTTP request been received.
    The access address for the <[Route("[controller]")]> is => localhost:5000/weatherforecast where port 5000 is define in the <launchSettings.json> file that can change to any unuse app port number. <weatherforecast> is from the Class name <public class WeatherForecastController> omitting the "Controller"

    This API controller will contain "Routes" and "End Point"
*/

/*
    Query of Database after Seeding database have been done
    1. The API controller is use to make query against the Database
    2. Every controller should be derive from ControllerBase
    3. The root route is </controller> ControllerBase Class
       public class WeatherForecastController : ControllerBase
    4. Create a new C# Class for the Database Query Purpose in the <Controller> folder
       We will name it as <BaseApiController.cs>
    5. Create another C# class controller file name as <ActivitiesController.cs>
    

*/
[ApiController]
/* 
The Endpoint for the HTTP request is "//api/activities" therefore need to add ""api/" before the [controller] => the controller referring to <ActivitiesController>
All Endpoint id been define in the <[ActivitiesController)]>
*/
// Default controller route
// [Route("[controller]")]
[Route("api/[controller]")] // Represent the designated Endpoint => api/activities
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    // Constructor
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /* 
        This is the End point of this API project to retrieve the data

        The End Point will define the HTTP request either Get, Post, Put, Update or Delete request
        For this case it will response to the Http Get request 

        Testing URL
        http://localhost:5000/Activities
        List all records in SQLite Database
        http://localhost:5000/Activities/103AC1A9-E0B1-4DF0-9C94-8358600DF0DD
        Just get the record for id = 103AC1A9-E0B1-4DF0-9C94-8358600DF0DD
        Using Swagger
        http://localhost:5000/swagger
        or
        http://localhost:5000/swagger/index.html
        or Default Get Weather Database
        http://localhost:5000/api/WeatherForecast

    */
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        /* Result will return from the WeatherForecast Class <WeatherForecast.cs> 
        
        The return result for this End Point will be something like:

        [
            {
                "date": "2024-01-29",
                "temperatureC": 42,
                "temperatureF": 107,
                "summary": "Chilly"
            },
            {
                "date": "2024-01-30",
                "temperatureC": 53,
                "temperatureF": 127,
                "summary": "Freezing"
            },
            {
                "date": "2024-01-31",
                "temperatureC": -9,
                "temperatureF": 16,
                "summary": "Warm"
            },
            {
                "date": "2024-02-01",
                "temperatureC": -19,
                "temperatureF": -2,
                "summary": "Hot"
            },
            {
                "date": "2024-02-02",
                "temperatureC": 6,
                "temperatureF": 42,
                "summary": "Sweltering"
            }
        ]



        */
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
