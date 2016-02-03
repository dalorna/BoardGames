using System.Windows.Forms;
using System.Drawing;
using System;

namespace Checkers
{
    public class TilePiece : PictureBox
    {
        public BoardLocation BoardLocation { get; set; }
        private bool bFocus = false;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PictureBox box = (PictureBox)this;
            if (bFocus)
            {
                Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                box.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Blue);
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
}