module WeatherData

open System.IO

// Data types
type DayData = {
    dayNumber : int;
    maxTemp : float;
    minTemp : float;
}

// Helper functions
open System.Text.RegularExpressions


type WeatherInfo = { dayNumber: int; maxTemp: float; minTemp: float }


let isNullOrWhiteSpace (s: string) =
    System.String.IsNullOrEmpty(s) || s.Trim().Length = 0


let parseLine (line: string) =
    let parts = Regex.Split(line, @"\s+")
                 |> Array.filter (fun s -> not (isNullOrWhiteSpace s))
                 |> Array.toList
   
    if parts.Length < 14 then failwith $"Invalid data format: {line}"
   
    try
        let dayNumber = int parts.[0]
        let maxTemp = float parts.[1]
       
        // Handle the MinT field with an asterisk
        let minTempWithAsterisk = parts.[2]
        let minTemp =
            match Regex.Match(minTempWithAsterisk, @"(\d+\.?\d*)\*?").Groups.[1].Value with
            | "" -> failwith $"Error parsing MinT field: {line}"
            | numericPart -> float numericPart


        { dayNumber = dayNumber; maxTemp = maxTemp; minTemp = minTemp }
    with
    | :? System.FormatException ->
        failwith $"Error parsing line: {line}"

let calculateTempSpread (day: WeatherInfo) = day.maxTemp - day.minTemp

// Main function
let processWeatherData (filePath: string) =
    let data = File.ReadAllLines(filePath) |> Seq.skip 2 |> List.ofSeq
    let parsedData =
        try
            List.map parseLine data
        with
        | ex ->
            printfn "Exception occurred while parsing data: %s" (ex.ToString())
            []
    if parsedData <> [] then
        let minSpreadDay = parsedData |> List.minBy calculateTempSpread
        printfn "Day with smallest temperature spread: %d with %f degrees" minSpreadDay.dayNumber minSpreadDay.minTemp
    else
        printfn "No valid data found in the file."

// Usage
let main () =
    processWeatherData @"C:\Users\Emily Cabrera\FirstIonideProject\weather.dat"


main()
