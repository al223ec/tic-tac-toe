// event-driven use-cases
// Initialize a game
// Player X moves
// Player O moves

module TicTacToeDomain =

    type HorizPosition = Left | HCenter | Right
    type VertPosition = Top | VCenter | Bottom
    type CellPosition = HorizPosition * VertPosition

    type Player = PlayerO | PlayerX

    type CellState =
        | Played of Player
        | Empty

    type Cell = {
        pos : CellPosition
        state  : CellState
    }

    type PlayerXPos = PlayerXPos of CellPosition
    type PlayerOPos = PlayerOPos of CellPosition

    type ValidMovesForPlayerX  = PlayerXPos list
    type ValidMovesForPlayerO  = PlayerOPos list

    // the private Game state, only a placeholder
    type GameState = exn

    // the move result
    type MoveResult =
        | PlayerXToMove of GameState * ValidMovesForPlayerX
        | PlayerOToMove of GameState * ValidMovesForPlayerO
        | GameWon of GameState * Player
        | GameTied of GameState

    // the use-cases
    type NewGame =
        GameState * MoveResult
    type PlayerXMoves =
        GameState * PlayerXPos -> // input
            GameState * MoveResult // output
    type PlayerOMoves =
        GameState * PlayerOPos ->
            GameState * MoveResult

    // helper funciton
    type GetCells = GameState -> Cell list

    // type UserAction =
    //     | PlayerXMoves
    //     | PlayerOMoves
    // type Move = UserAction * GameState -> GameState