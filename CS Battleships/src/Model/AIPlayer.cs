using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+, 
// using SwinGameSDK.SwinGame; // requires mcs version 4+, 

namespace Battleship
{
    /// <summary>
    /// The AIPlayer is a type of player. It can readonly deploy ships, it also has the
    /// functionality to generate coordinates and shoot at tiles.
    /// </summary>
    public abstract class AIPlayer : Player
    {

        /// <summary>
        /// Location can store the location of the last hit made by an
        /// AI Player. the use of which determiens the difficulty. 
        /// </summary>

        protected class Location
        {
            private int _Row;
            private int _Column;

            /// <summary>
            /// The row of the shot
            /// </summary>
            /// <value>The row of the shot</value>
            /// <returns>The row of the shot</returns>

            public int Row
            {
                get
                {
                    return _Row;
                }
                set
                {
                    _Row = value;
                }
            }

            /// <summary>
            /// The column of the shot
            /// </summary>
            /// <value>The column of the shot</value>
            /// <returns>The column of the shot</returns>

            public int Column
            {
                get
                {
                    return _Column;
                }
                set
                {
                    _Column = Row;
                }
            }

            /// <summary>
            /// Sets the last hit made to the last local variables
            /// </summary>
            /// <param name="row">the row of the location</param>
            /// <param name="column">the column of the location</param>
            /// 
            public Location(int row, int column)
            {
                _Column = column;
                _Row = row;
            }

            /// <summary>
            /// Check if two locations are equal
            /// </summary>
            /// <param name="location1">location 1</param>
            /// <param name="location2">locatoin 2</param>
            /// <returns>true if location 1 and 2 are equal</returns>
            public static bool operator ==(Location location1, Location location2)
            {
                return ((location1 != null) && (location2 != null) && (location1.Row == location2.Row)
                    && (location1.Column == location2.Column));
            }

            /// <summary>
            /// Check if two locations are not equal
            /// </summary>
            /// <param name="location1">location 1</param>
            /// <param name="location2">location 2</param>
            /// <returns>true if locations are not equal</returns>
            public static bool operator !=(Location location1, Location location2)
            {
                return ((location1 == null) || (location2 == null) || (location1.Row != location2.Row)
                    || (location1.Column != location2.Column));
            }
        }

        public AIPlayer(BattleShipsGame game) : base(game)
        {

        }

        /// <summary>
        /// Generate a valid row, column to shoot at 
        /// </summary>
        /// <param name="row">output the row for the next shot</param>
        /// <param name="column">output the column for the next shot</param>
        protected abstract void GenerateCoords(ref int row, ref int column);

        protected abstract void ProcessShot(int row, int col, AttackResult result);

        /// <summary>
        /// the AI takes its attacks until its go is over
        /// </summary>
        /// <returns>the result of the last attack</returns>
        public override AttackResult Attack()
        {
            AttackResult result;
            int row = 0;
            int column = 0;

            do // keep hitting until a miss
            {
                Delay();

                GenerateCoords(ref row, ref column); //generate coordinates for shot
                result = _game.Shoot(row, column); // take shot
                ProcessShot(row, column, result);
            } while (result.Value != ResultOfAttack.Miss && result.Value != ResultOfAttack.GameOver && (!SwinGame.WindowCloseRequested());

            return result;
        }

        private void Delay()
        {
            int i;
            for (i = 0; i < 150; i++)
            {
                //Don't delay if window is closed
                if (SwinGame.WindowCloseRequested()) return;
            }

            SwinGame.Delay(5);
            SwinGame.ProcessEvents();
            SwinGame.RefreshScreen();
        }
    }
}
