module FootballData

open System.IO

// Data type for football team information
type TeamInfo = {
    Team: string;
    GoalsFor: int;
    GoalsAgainst: int;
}