using System.Windows.Forms;

namespace Checkers
{
    public class TilePiece : PictureBox
    {
        public BoardLocation BoardLocation { get; set; }

        public TilePiece(int tileSize)
        {
            this.Width = tileSize;
            this.Height = tileSize;
            this.AllowDrop = true;
        }

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
    }
}