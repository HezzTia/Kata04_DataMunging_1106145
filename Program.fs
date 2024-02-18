module WeatherData

open System.IO

type DayData = {
    dayNumber : int;
    maxTemp : float;
    minTemp : float;
}

open System.Text.RegularExpressions

type WeatherInfo = { dayNumber : int; maxTemp : float; minTemp : float }

let isNullOrWhiteSpace (s: string) = //string -> bool
    System.String.IsNullOrEmpty(s) || s.Trim().Length = 0

let parseline (line : string) = 
    let parts : string list = Regex.Split(line, @"\s+")  //string array
                 |> Array.filter (fun s -> not (isNullOrWhiteSpace s))
                 |> Array.toList