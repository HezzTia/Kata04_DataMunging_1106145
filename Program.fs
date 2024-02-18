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
            | numericPart : string -> float numericPart
        
        { dayNumber = dayNumber; maxTemp = maxTemp; minTemp = minTemp }
    with
    | :? System.FormatException ->
        failwith $"Error parsing line: {line}"

let calculateTempSpread (day: WeatherInfo) = day.maxTemp - day.minTemp


let processWeatherData (filePath: string) =
    let data: string list = File.ReadAllLines(filePath) |> Seq.skip count = 2 |> List.ofSeq
    let parsedData: WeatherInfo list =
        try
            List.map parseLine data
        with
        | ex: exn ->
            printfn "Exception ocurred while parsing data: %s" {ex.ToString()}
            []
    if parsedData <> [] then
        let minSpreadDay: WeatherInfo = parsedData |> List.minBy calculateTempSpread
            printfn "Day with smallest temperature spread: %d with %f degrees" minSpreadDay.dayNumber minSpreadDay.maxTemp
    else
        printfn "No valid data found in file."


let main () =
    processWeatherData filePath = @"C:\Users\Emily Cabrera\FirstIonideProject\weather.dat"

main()