namespace RogueLike
{
    public class Position
    {
        /// <summary>
        /// Gets and sets a row value.
        /// </summary>
        public byte Row { get; private set; }

        /// <summary>
        /// Gets and sets a column value.
        /// </summary>
        public byte Column { get; private set; }
    
        /// <summary>
        /// Gets and sets a value indicating if the piece is playable.
        /// </summary>
        public bool IsPlayable { get; set; }

        /// <summary>
        /// Gets and sets a value indicating if the position is occupied.
        /// </summary>
        public bool Occupied { get; private set; }
        
        /// <summary>
        /// Constructor used when using the Class Position with the Class
        /// Board
        /// </summary>
        /// <param name="row">Row value  of the board </param>
        /// <param name="column">Column value of the board</param>
        /// <param name="isPlayable">Specifies whether the position is playable. 
        /// </param>
        public Position(byte row, byte column, bool isPlayable)
        {
            Row = row;
            Column = column;
            IsPlayable = isPlayable;
        }        
        /// <summary>
        /// Constructor used when using the Class Position with the Class
        /// Player.
        /// </summary>
        /// <param name="row">Row value  of the board.</param>
        /// <param name="column">Column value of the board.</param>
        public Position(byte row, byte column)
        {
            Row = row;
            Column = column;
        }
    }
}
