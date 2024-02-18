module FootballData

open System.IO

// Data type for football team information
type TeamInfo = {
    Team: string;
    GoalsFor: int;
    GoalsAgainst: int;
}

// Helper fuunction to parse a line and extract relevant information

let parseline (line: string) = //string -> option<TeamInfo>
    let parts : string array = line.Split(separator = [| ' '; '\t' |], options = System.StringSplitOptions.RemoveEmptyEntries)

    // Ignore lines that don't contain team data
    if parts.Length < 8 then None
    else
        let team = parts.[1]
        let goalsFor = int parts.[6]
        let goalsAgainst = int parts.[8]
        Some { Team = team; GoalsFor = goalsFor; GoalsAgainst = goalsAgainst }  

// Function to calculate the goal difference

let calculateGoalDifference (team: )