using System;
using System.Windows.Forms;
using System.Drawing;

namespace Checkers
{
    public class GamePiece : PictureBox
    {
        public GridEntry PieceState { get; set; }
        public BoardLocation BoardLocation { get; set; }
        public string PieceName { get; set; }
        public bool IsPromoted { get; set; }
        private const int PieceSize = 40;
        private bool bFocus = false;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PictureBox box = this;
            if (bFocus)
            {
                Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                box.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Red);
                Graphics g = e.Graphics;
                g.DrawRectangle(p, rectBorder);
            }
            else
            {
                box.BorderStyle = BorderStyle.None;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            bFocus = true;
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            bFocus = false;
            Refresh();
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