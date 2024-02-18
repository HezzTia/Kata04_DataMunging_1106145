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

let parseLine (line: string) =
    let parts = Regex.Split(line, @"\s+")
                 |> Array.filter (fun s -> not (isNullOrWhiteSpace s))
                 |> Array.toList
    if parts.Length < 14 then failwith message = $"Invalid data format: {line}"
    try
        let dayNumber = int parts.[0]
        let maxTemp = float parts.[1]

        let minTempwithAsterisk: string = parts.[2]
        let minTemp: float = 
            match Regex.Match(input = minTempwithAsterisk, pattern = @"(\d+\.\d*)\*?").Groups.[1].Value with
            | "" -> failwith message = $"Error parsing MinT field: {line}"