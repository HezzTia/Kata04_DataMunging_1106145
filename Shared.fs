module Shared

// Data type for weather day information
type DayData = {
    DayNumber: int;
    MaxTemp: float;
    MinTemp: float;
}

open System.IO

type Parser<'T> = {
    ParseLine: string -> Option<'T>
}

// Function to read data from a file using a provided parser
let readData<'T> (filePath: string) (parser: Parser<'T>) =
    File.ReadAllLines(filePath)
    |> Seq.skip 1
    |> List.ofSeq
    |> List.choose parser.ParseLine
