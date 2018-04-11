using System;
namespace Battleship
{
    // <summary>
    // The ISeaGrid defines the read only interface of a Grid. This
    // allows each player to see and attack their opponents grid.
    // </summary>
    public interface ISeaGrid
    {
        // <summary>
        // Indicates that the grid has changed.
        // </summary>
        event EventHandler Changed;

        // <summary>
        // Provides access to the given row/column
        // </summary>
        // <param name="row">the row to access</param>
        // <value>what the player can see at that location</value>
        // <returns>What the player can see at that location</returns>
        TileView Item(int row, int column);

        // <summary>
        // Mark the indicated tile as shot
        // </summary>
        // <param name="row">The row of the tile</param>
        // <param name="col">The column of the tile</param>
        // <returns>The result of the attack</returns>
        AttackResult HitTile(int row, int col);
    }
}