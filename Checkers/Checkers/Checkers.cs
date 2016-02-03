using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Checkers : Form
    {
        private const int BOARDSIZE = 8;
        private const int TILESIZE = 80;
        private int[,] BOARD_ARRAY = new int[BOARDSIZE, BOARDSIZE];

        private GamePiece CurrentPiece;
        private GamePiece ReplacedPiece;
        private TilePiece DestinationTile;

        public Checkers()
        {
            InitializeComponent();
            InitBoard();
        }

        private void InitBoard()
        {
            int iInd = 0;
            int iIndex = 64;
            int iWhite = 1;
            int iBlack = 1;
            for (int row = 0; row < BOARDSIZE; row++)
            {
                for (int col = 0; col < BOARDSIZE; col++)
                {
                    TilePiece gridCell = new TilePiece(TILESIZE);
                    gridCell.MouseDown += gameCell_MouseDown;
                    gridCell.BoardLocation = new BoardLocation { Row = row, Column = col };
                    gridCell.Location = new Point(12 + TILESIZE * col, 5 + TILESIZE * row);
                    //mBox.Text += (12 + TILESIZE * row).ToString() + ", " + (5 + TILESIZE * col) + Environment.NewLine;
                    gridCell.BackColor = (row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1) ? Color.White : Color.Black;
                    gridCell.DragDrop += gridCell_DragDrop;
                    gridCell.DragEnter += gridCell_DragEnter;
                    this.Controls.Add(gridCell);
                    this.Controls.SetChildIndex(gridCell, ++iInd);

                    if (row > 4 && gridCell.BackColor == Color.Black)
                    {
                        BOARD_ARRAY[row, col] = (int)CellState.State.BLACK_PIECE;
                        GamePiece piece = new GamePiece() { BoardLocation = new BoardLocation { Row = row, Column = col } };
                        piece.MouseDown += piece_MouseDown;
                        piece.MouseEnter += piece_MouseEnter;
                        piece.MouseLeave += piece_MouseLeave;
                        piece.DragEnter += piece_DragEnter;
                        piece.DragDrop += piece_DragDrop;
                        piece.PieceState = CellState.State.BLACK_PIECE;
                        piece.Name = "BLACKPIECE_" + iWhite++; 
                        piece.Image = new Bitmap(Image.FromFile(@"c:\users\jasonr\documents\visual studio 2013\Projects\Checkers\Checkers\Assets\pieceRed_border11.png"));
                        piece.Location = new Point((12 + TILESIZE * col) + 10, (5 + TILESIZE * row) + 10);

                        this.Controls.Add(piece);
                        this.Controls.SetChildIndex(piece, ++iIndex);
                        piece.BringToFront();
                    }
                    else if (row < 3 && gridCell.BackColor == Color.Black)
                    {
                        BOARD_ARRAY[row, col] = (int)CellState.State.WHITE_PIECE;
                        GamePiece piece = new GamePiece() { BoardLocation = new BoardLocation { Row = row, Column = col } };
                        piece.MouseDown += piece_MouseDown;
                        piece.MouseEnter += piece_MouseEnter;
                        piece.MouseLeave += piece_MouseLeave;
                        piece.DragEnter += piece_DragEnter;
                        piece.DragDrop += piece_DragDrop;
                        piece.PieceState = CellState.State.WHITE_PIECE;
                        piece.Name = "WHITEPIECE_" + iBlack++;
                        piece.Image = new Bitmap(Image.FromFile(@"c:\users\jasonr\documents\visual studio 2013\Projects\Checkers\Checkers\Assets\pieceWhite_border11.png"));
                        piece.Location = new Point((12 + TILESIZE * col) + 10, (5 + TILESIZE * row) + 10);

                        this.Controls.Add(piece);
                        this.Controls.SetChildIndex(piece, ++iIndex);
                        piece.BringToFront();
                    }
                    else
                    {
                        BOARD_ARRAY[row, col] = (int)CellState.State.EMPTY;
                    }
                }
            }



        }

        private bool IsLegalMove(GamePiece currentPiece, GamePiece replacedPiece, TilePiece tileToMove)
        {

            /*Black pieces can only move down unless promoted, and 
             * 
             * 
             *Pieces can only be on the black tiles
             *Can't Land on any piece... 
             * 
             */

            if (tileToMove.BackColor == Color.White)
                return false;

            if (ReplacedPiece != null && CurrentPiece != ReplacedPiece)
            {
                return false;
                //MessageBox.Show("Can't replace any piece, No legal move should allow this to happen...", "Is Legal Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bool IsCapture = IsCaptureMove(currentPiece, DestinationTile);
            if (DestinationTile != null && ReplacedPiece == null && CurrentPiece != null)
            {
                if(currentPiece.PieceState == CellState.State.WHITE_PIECE)
                {
                    if(currentPiece.IsPromoted == false)
                    {
                        if ((currentPiece.BoardLocation.Row + 1 == DestinationTile.BoardLocation.Row
                            && ((currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column + 1 
                                || currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column - 1) 
                                && DestinationTile.BoardLocation.Column >= 0
                                && DestinationTile.BoardLocation.Column < BOARDSIZE)) || IsCapture)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else if(currentPiece.PieceState == CellState.State.BLACK_PIECE)
                {
                    if (currentPiece.IsPromoted == false)
                    {
                        if (currentPiece.BoardLocation.Row - 1 == DestinationTile.BoardLocation.Row
                            && ((currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column + 1
                                || currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column - 1) 
                                && DestinationTile.BoardLocation.Column >= 0
                                && DestinationTile.BoardLocation.Column < BOARDSIZE) || IsCapture)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            //MessageBox.Show("Shouldn't Be here", "Is Legal Move", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return true;
        }

        private bool IsCaptureMove(GamePiece currentPiece, TilePiece destinationTile)
        {
            int row = currentPiece.BoardLocation.Row - destinationTile.BoardLocation.Row;
            int column = currentPiece.BoardLocation.Column - destinationTile.BoardLocation.Column;
            if (Math.Abs(row) > 2 || Math.Abs(column) > 2) 
                return false;


            bool IsCapture = false;
            BoardLocation capturedPiece = new BoardLocation();
            if(currentPiece.PieceState == CellState.State.WHITE_PIECE)
            {
                var land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column - 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    CellState.State cellState = (CellState.State)Enum.Parse(typeof(CellState.State), BOARD_ARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column - 1].ToString());
                    IsCapture = cellState == CellState.State.BLACK_PIECE;
                    capturedPiece = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column - 1 };
                }

                land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column + 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    CellState.State cellState = (CellState.State)Enum.Parse(typeof(CellState.State), BOARD_ARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column + 1].ToString());
                    IsCapture = cellState == CellState.State.BLACK_PIECE;
                    capturedPiece = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column + 1 };
                }
            }
            else if(currentPiece.PieceState == CellState.State.BLACK_PIECE)
            {
                var land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column - 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    CellState.State cellState = (CellState.State)Enum.Parse(typeof(CellState.State), BOARD_ARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column - 1].ToString());
                    IsCapture = cellState == CellState.State.WHITE_PIECE;
                    capturedPiece = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column - 1 };
                }

                land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column + 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    CellState.State cellState = (CellState.State)Enum.Parse(typeof(CellState.State), BOARD_ARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column + 1].ToString());
                    IsCapture = cellState == CellState.State.WHITE_PIECE;
                    capturedPiece = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column + 1 };
                }
            }

            //TODO:: Add Promoted Piece
            if(IsCapture)
            {
                foreach(Control c in this.Controls)
                {
                    if(c is GamePiece)
                    {
                        if(((GamePiece)c).BoardLocation.Row == capturedPiece.Row && ((GamePiece)c).BoardLocation.Column == capturedPiece.Column)
                        {
                            this.Controls.Remove(c);
                            BOARD_ARRAY[capturedPiece.Row, capturedPiece.Column] = (int)CellState.State.EMPTY;
                            break;
                        }
                    }
                }
            }

            return IsCapture;
        }

        private void AdjustBoard(BoardLocation location, CellState.State state)
        {
            BOARD_ARRAY[location.Row, location.Column] = (int)state;
        }

        private void PiecePromoted(GamePiece piece, int row, int col)
        {

        }

        void gridCell_DragEnter(object sender, DragEventArgs e)
        {
            DestinationTile = (TilePiece)sender;
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
        }

        void gridCell_DragDrop(object sender, DragEventArgs e)
        {

        }

        //FROM
        void piece_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentPiece = null;
            ReplacedPiece = null;
            DestinationTile = null;

            var img = ((PictureBox)sender).Image;
            if (img == null) return;
            CurrentPiece = (GamePiece)sender; 

            if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
            {
                if (IsLegalMove(CurrentPiece, ReplacedPiece, DestinationTile) == false)
                {
                    return;
                }

                //if (ReplacedPiece != null && CurrentPiece != ReplacedPiece)
                //{
                //    AdjustBoard(CurrentPiece.BoardLocation, CellState.State.EMPTY);
                //    this.Controls.Remove(ReplacedPiece);
                //    this.Controls.Remove(CurrentPiece);

                //    CurrentPiece.Location = ReplacedPiece.Location;
                //    CurrentPiece.BoardLocation = ReplacedPiece.BoardLocation;
                //    this.Controls.Add(CurrentPiece);
                //    this.Controls.SetChildIndex(CurrentPiece, 1000);
                //    this.CurrentPiece.BringToFront();
                //    AdjustBoard(CurrentPiece.BoardLocation, CurrentPiece.PieceState);
                //    return;
                    
                //}

                if (DestinationTile != null && ReplacedPiece == null && CurrentPiece != null)
                {
                    AdjustBoard(CurrentPiece.BoardLocation, CellState.State.EMPTY);
                    var movingPiece = ((GamePiece)sender);
                    this.Controls.Remove(movingPiece);
                    CurrentPiece.BoardLocation = new BoardLocation { Row = DestinationTile.BoardLocation.Row, Column = DestinationTile.BoardLocation.Column };
                    CurrentPiece.Location = new Point((12 + TILESIZE * DestinationTile.BoardLocation.Column) + 10, (5 + TILESIZE * DestinationTile.BoardLocation.Row) + 10);
                    this.Controls.Add(CurrentPiece);
                    this.Controls.SetChildIndex(CurrentPiece, 1000);
                    this.CurrentPiece.BringToFront();
                    AdjustBoard(CurrentPiece.BoardLocation, CurrentPiece.PieceState);
                    return;
                }

                MessageBox.Show("Shouldn't Be here", "Mouse Down", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TO
        void piece_DragDrop(object sender, DragEventArgs e)
        {
            //if (sender is GamePiece)
            //{
            //    ReplacedPiece = (GamePiece)sender;
            //    var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            //    ((GamePiece)sender).Image = bmp;
            //}
        }

        //FROM
        void piece_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
        }

        void piece_MouseLeave(object sender, EventArgs e)
        {
        }

        void piece_MouseEnter(object sender, EventArgs e)
        {
        }

        void gameCell_MouseDown(object sender, MouseEventArgs e)
        {
            var tile = (TilePiece)sender;
            this.mBox.Text = "[Row: " + tile.BoardLocation.Row + "] [Column: " + tile.BoardLocation.Column + "]";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.mBox.Clear();
            string str = string.Empty;
            for (int row = 0; row < BOARDSIZE; row++)
            {
                for (int col = 0; col < BOARDSIZE; col++)
                {
                    str += BOARD_ARRAY[row, col].ToString() + " "; 
                }
                this.mBox.Text += str + Environment.NewLine;
                str = string.Empty;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.mBox.Clear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if(c is PictureBox)
                {
                    this.Controls.Remove(c);
                }
            }
            InitBoard();
        }
    }
}
