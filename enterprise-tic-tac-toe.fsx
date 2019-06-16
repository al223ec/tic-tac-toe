// event-driven use-cases
// Initialize a game
// Player X moves
// Player O moves

module TicTacToeDomain =

    type HorizPosition = Left | HCenter | Right
    type VertPosition = Top | VCenter | Bottom
    type CellPosition = HorizPosition * VertPosition

    type CellState =
        | X
        | O
        | Empty

    type Cell = {
        pos : CellPosition
        state  : CellState
    }

    type PlayerXPos = PlayerXPos of CellPosition
    type PlayerOPos = PlayerOPos of CellPosition

    // the private Game state, only a placeholder
    type GameState = exn

    // the use-cases
    type InitGame = unit -> GameState
    type InitialGameState = GameState
    type PlayerXMoves = GameState * PlayerXPos -> GameState
    type PlayerOMoves = GameState * PlayerOPos -> GameState

    // helper funciton
    type GetCells = GameState -> Cell list

    // type UserAction =
    //     | PlayerXMoves
    //     | PlayerOMoves

    // type Move = UserAction * GameState -> GameState