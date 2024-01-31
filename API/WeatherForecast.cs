namespace API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /*
        This Summary <string?> with a "?" means this is optional

        From Dotnet v6 onward, the MS introduce <Nullable> property flag.
        Refer to API.csproj file which indicate <<Nullable>enable</Nullable>> this means the string is always require unless we explicitly say its optional.
        Since we want the string to be option <string?>, then we will disable this <Nullable> flag => <<Nullable>disable</Nullable>>
        then we can remove the 
        "?" sign
    */
    // Original code
    // public string? Summary { get; set; }
    public string Summary { get; set; }
}
