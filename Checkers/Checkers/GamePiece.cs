using System.Windows.Forms;

namespace Checkers
{
    public class GamePiece : PictureBox
    {
        public CellState.State PieceState { get; set; }
        public BoardLocation BoardLocation { get; set; }
        public string PieceName { get; set; }
        public bool IsPromoted { get; set; }
        private const int PieceSize = 60;

        public override bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                base.AllowDrop = value;
            }
        }

        public GamePiece()
        {
            this.Height = PieceSize;
            this.Width = PieceSize;
            this.AllowDrop = true;
        }
    }

    public class BoardLocation
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public override string ToString()
        {
            return "[Row: " + Row + "] [Column: " + Column + "]";
        }
    }
}