module FootballData

open System.IO

// Data type for football team information
type TeamInfo = {
    Team: string;
    GoalsFor: int;
    GoalsAgainst: int;
}

// Helper function to parse a line and extract relevant information
let parseLine (line: string) =
    let parts = line.Split([|' '; '\t'|], System.StringSplitOptions.RemoveEmptyEntries)


    // Ignore lines that don't contain team data
    if parts.Length < 8 then None
    else
        let team = parts.[1]
        let goalsFor = int parts.[6]
        let goalsAgainst = int parts.[8]
        Some { Team = team; GoalsFor = goalsFor; GoalsAgainst = goalsAgainst }


// Function to calculate the goal difference
let calculateGoalDifference (team: TeamInfo) = abs (team.GoalsFor - team.GoalsAgainst)

// Main function to find the team with the smallest goal difference
let findTeamWithSmallestDifference (filePath: string) =
    let data = File.ReadAllLines(filePath) |> Seq.skip 1 |> List.ofSeq
    let teamData =
        data
        |> List.choose parseLine
        |> List.sortBy calculateGoalDifference
    match teamData with
    | [] -> None
    | smallestDiffTeam :: _ ->
        Some smallestDiffTeam.Team
// Usage
let main () =
    match findTeamWithSmallestDifference @"C:\Users\Emily Cabrera\FirstIonideProject\football.dat" with
    | Some team ->
        printfn "Team with the smallest goal difference: %s" team
    | None ->
        printfn "No valid team data found in the file."


main()


