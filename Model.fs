// https://www.youtube.com/watch?v=hsTmLhnzRhE
type Details =
    { Name : string
      Description : string }

type Item =
    { Details : Details }

type Exit =
    | Passable of Details * destination : Room
    | Locked of Details * Key : Item * next : Exit
    | NoExit of Details option

and Exits =
    { North : Exit
      South : Exit
      East : Exit
      West : Exit }

and Room =
    { Details : Details
      Items : Item list
      Exits : Exits }

let firstRoom =
    { Details =
          { Name = "First Room"
            Description = "You're standing in a room" }
      Items = []
      Exits =
          { North = NoExit(None)
            South = NoExit(None)
            East = NoExit(None)
            West = NoExit(None) } }
