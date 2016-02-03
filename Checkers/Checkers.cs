using Checkers.AI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Checkers : Form
    {
        private const int BOARDSIZE = 8;
        private const int TILESIZE = 60;
        private const int TOPMARGIN = 35;
        private const int LEFTMARGIN = 35;

        private GamePiece CurrentPiece;
        private GamePiece ReplacedPiece;
        private TilePiece DestinationTile;
        private bool bGameStarted = false;
        private GameTurn _GameTurn = new GameTurn();
        private Players _WhitePlayer = new Players();
        private Players _BlackPlayer = new Players();

        public Checkers()
        {
            InitializeComponent();
            _GameTurn.BOARDARRAY = new int[BOARDSIZE, BOARDSIZE];

            InitBoard();
            _WhitePlayer.PropertyChanged += WhitePlayer_PropertyChanged;
            _BlackPlayer.PropertyChanged += BlackPlayer_PropertyChanged;
            _GameTurn.GameChanged += ChangeState;
        }

        //Methods
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
                    gridCell.Location = new Point(LEFTMARGIN + TILESIZE * col, TOPMARGIN + TILESIZE * row);
                    //mBox.Text += (TOPMARGIN + TILESIZE * row).ToString() + ", " + (5 + TILESIZE * col) + Environment.NewLine;
                    gridCell.BackColor = (row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1) ? Color.White : Color.Black;
                    gridCell.DragDrop += gridCell_DragDrop;
                    gridCell.DragEnter += gridCell_DragEnter;
                    this.Controls.Add(gridCell);
                    this.Controls.SetChildIndex(gridCell, ++iInd);

                    if (row > 4 && gridCell.BackColor == Color.Black)
                    {
                        _GameTurn.BOARDARRAY[row, col] = (int)GridEntry.BLACKPAWN;
                        GamePiece piece = new GamePiece() { BoardLocation = new BoardLocation { Row = row, Column = col } };
                        piece.MouseDown += piece_MouseDown;
                        piece.DragEnter += piece_DragEnter;
                        piece.DragDrop += piece_DragDrop;
                        piece.PieceState = GridEntry.BLACKPAWN;
                        piece.Name = "BLACKPIECE_" + iWhite++;
                        piece.Image = GetImage("Checkers.Assets.BlackPiece.png");
                        piece.Location = new Point((LEFTMARGIN + TILESIZE * col) + 10, (TOPMARGIN + TILESIZE * row) + 10);

                        this.Controls.Add(piece);
                        this.Controls.SetChildIndex(piece, ++iIndex);
                        piece.BringToFront();
                    }
                    else if (row < 3 && gridCell.BackColor == Color.Black)
                    {
                        _GameTurn.BOARDARRAY[row, col] = (int)GridEntry.WHITEPAWN;
                        GamePiece piece = new GamePiece() { BoardLocation = new BoardLocation { Row = row, Column = col } };
                        piece.MouseDown += piece_MouseDown;
                        piece.DragEnter += piece_DragEnter;
                        piece.DragDrop += piece_DragDrop;
                        piece.PieceState = GridEntry.WHITEPAWN;
                        piece.Name = "WHITEPIECE_" + iBlack++;

                        piece.Image = GetImage("Checkers.Assets.WhitePiece.png");
                        piece.Location = new Point((LEFTMARGIN + TILESIZE * col) + 10, (TOPMARGIN + TILESIZE * row) + 10);

                        this.Controls.Add(piece);
                        this.Controls.SetChildIndex(piece, ++iIndex);
                        piece.BringToFront();
                    }
                    else
                    {
                        if (gridCell.BackColor == Color.Black)
                        {
                            _GameTurn.BOARDARRAY[row, col] = (int)GridEntry.EMPTY;
                        }
                        else
                        {
                            _GameTurn.BOARDARRAY[row, col] = (int)GridEntry.NULL;
                        }
                    }
                }
            }
        }

        private Image GetImage(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                return Image.FromStream(stream);
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
            bool IsLegal = false;
            if (tileToMove == null) return IsLegal;

            if (tileToMove.BackColor == Color.White)
                return false;

            if (ReplacedPiece != null && CurrentPiece != ReplacedPiece)
            {
                return false;
            }

            bool IsCapture = IsCaptureMove(currentPiece, DestinationTile);
            if (DestinationTile != null && ReplacedPiece == null && CurrentPiece != null)
            {
                if (currentPiece.PieceState == GridEntry.WHITEPAWN)
                {
                    if ((currentPiece.BoardLocation.Row + 1 == DestinationTile.BoardLocation.Row
                        && ((currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column + 1
                            || currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column - 1)
                            && DestinationTile.BoardLocation.Column >= 0
                            && DestinationTile.BoardLocation.Column < BOARDSIZE)) || IsCapture)
                    {
                        IsLegal = true;
                    }
                }
                else if (currentPiece.PieceState == GridEntry.BLACKPAWN)
                {
                    if (currentPiece.BoardLocation.Row - 1 == DestinationTile.BoardLocation.Row
                        && ((currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column + 1
                            || currentPiece.BoardLocation.Column == DestinationTile.BoardLocation.Column - 1)
                            && DestinationTile.BoardLocation.Column >= 0
                            && DestinationTile.BoardLocation.Column < BOARDSIZE) || IsCapture)
                    {
                        IsLegal = true;
                    }
                }
                else if (currentPiece.IsPromoted)
                {
                    if ((Math.Abs(currentPiece.BoardLocation.Row - DestinationTile.BoardLocation.Row) == 1
                        && Math.Abs(currentPiece.BoardLocation.Column - DestinationTile.BoardLocation.Column) == 1) || IsCapture)
                    {
                        IsLegal = true;
                    }
                }
            }

            return IsLegal;
        }

        private bool IsCaptureMove(GamePiece currentPiece, TilePiece destinationTile)
        {
            int row = currentPiece.BoardLocation.Row - destinationTile.BoardLocation.Row;
            int column = currentPiece.BoardLocation.Column - destinationTile.BoardLocation.Column;
            if (Math.Abs(row) > 2 || Math.Abs(column) > 2)
                return false;

            bool IsCapture = false;
            BoardLocation captureLoc = new BoardLocation();
            if (currentPiece.PieceState == GridEntry.WHITEPAWN)
            {
                var land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column - 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column - 1].ToString());
                    IsCapture = cellState == GridEntry.BLACKPAWN || cellState == GridEntry.BLACKKING;
                    captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column - 1 };
                }

                land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column + 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column + 1].ToString());
                    IsCapture = cellState == GridEntry.BLACKPAWN || cellState == GridEntry.BLACKKING;
                    captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column + 1 };
                }
            }
            else if (currentPiece.PieceState == GridEntry.BLACKPAWN)
            {
                var land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column - 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column - 1].ToString());
                    IsCapture = cellState == GridEntry.WHITEPAWN || cellState == GridEntry.WHITEKING;
                    captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column - 1 };
                }

                land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column + 2 };
                if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                {
                    GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column + 1].ToString());
                    IsCapture = cellState == GridEntry.WHITEPAWN || cellState == GridEntry.WHITEKING;
                    captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column + 1 };
                }
            }
            else if (currentPiece.IsPromoted)
            {
                if (currentPiece.PieceState == GridEntry.WHITEKING)
                {
                    var land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column - 2 };

                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column - 1].ToString());
                        IsCapture = cellState == GridEntry.BLACKPAWN || cellState == GridEntry.BLACKKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column - 1 };
                    }

                    land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column + 2 };
                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column + 1].ToString());
                        IsCapture = cellState == GridEntry.BLACKPAWN || cellState == GridEntry.BLACKKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column + 1 };
                    }

                    land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column + 2 };
                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column + 1].ToString());
                        IsCapture = cellState == GridEntry.BLACKPAWN || cellState == GridEntry.BLACKKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column + 1 };
                    }

                    land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column - 2 };
                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column - 1].ToString());
                        IsCapture = cellState == GridEntry.BLACKPAWN || cellState == GridEntry.BLACKKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column - 1 };
                    }
                }
                else if (currentPiece.PieceState == GridEntry.BLACKKING)
                {
                    var land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column - 2 };

                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column - 1].ToString());
                        IsCapture = cellState == GridEntry.WHITEPAWN || cellState == GridEntry.WHITEKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column - 1 };
                    }

                    land = new BoardLocation { Row = currentPiece.BoardLocation.Row - 2, Column = currentPiece.BoardLocation.Column + 2 };
                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row - 1, currentPiece.BoardLocation.Column + 1].ToString());
                        IsCapture = cellState == GridEntry.WHITEPAWN || cellState == GridEntry.WHITEKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row - 1, Column = currentPiece.BoardLocation.Column + 1 };
                    }

                    land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column + 2 };
                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column + 1].ToString());
                        IsCapture = cellState == GridEntry.WHITEPAWN || cellState == GridEntry.WHITEKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column + 1 };
                    }

                    land = new BoardLocation { Row = currentPiece.BoardLocation.Row + 2, Column = currentPiece.BoardLocation.Column - 2 };
                    if (destinationTile.BoardLocation.Row == land.Row && destinationTile.BoardLocation.Column == land.Column)
                    {
                        GridEntry cellState = (GridEntry)Enum.Parse(typeof(GridEntry), _GameTurn.BOARDARRAY[currentPiece.BoardLocation.Row + 1, currentPiece.BoardLocation.Column - 1].ToString());
                        IsCapture = cellState == GridEntry.WHITEPAWN || cellState == GridEntry.WHITEKING;
                        captureLoc = new BoardLocation() { Row = currentPiece.BoardLocation.Row + 1, Column = currentPiece.BoardLocation.Column - 1 };
                    }
                }
            }

            if (IsCapture)
            {
                this.Controls.Remove(FindPiece(captureLoc) as Control);
                _GameTurn.BOARDARRAY[captureLoc.Row, captureLoc.Column] = (int)GridEntry.EMPTY;
            }

            return IsCapture;
        }

        private GamePiece FindPiece(BoardLocation loc)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GamePiece)
                {
                    if (((GamePiece)c).BoardLocation.Row == loc.Row && ((GamePiece)c).BoardLocation.Column == loc.Column)
                    {
                        return (GamePiece)c;
                    }
                }
            }

            return null;
        }

        private void AdjustBoard(BoardLocation location, GridEntry state)
        {
            _GameTurn.BOARDARRAY[location.Row, location.Column] = (int)state;
        }

        private void TurnChange()
        {
            if (_GameTurn.GameState == GameState.BLACKTURN)
            {
                _GameTurn.GameState = GameState.WHITETURN;
            }
            else if(_GameTurn.GameState == GameState.WHITETURN)
            {
                _GameTurn.GameState = GameState.BLACKTURN;
            }
            if (_GameTurn.GameState == GameState.BLACKTURN && _BlackPlayer.IsHuman == false
                || _GameTurn.GameState == GameState.WHITETURN && _WhitePlayer.IsHuman == false)
            {
                TakeComputerTurn();
            }
        }

        private void TakeComputerTurn()
        {
            ShowBoard(_GameTurn.BOARDARRAY, "Before Computer Turn");
            GridEntry[] boardState = TransformBoardToAIGridArray(_GameTurn.BOARDARRAY);
            CheckGame boardForComputerMove = new CheckGame(boardState);
            Board blackMove = boardForComputerMove.ComputerMakeMove(7);
            var transformedBoard = TransformGridToBoard(blackMove.BoardArray);
            ShowBoard(transformedBoard, "Computer Transformed board");
            var changes = FindComputerChanges(_GameTurn.BOARDARRAY, transformedBoard).ToList();

            if(changes.Count < 2 || changes.Count > 3)
            {
                MessageBox.Show("The AI returned " + changes.Count + " changes", "AI Move Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EvaluteComputerChanges(changes);
            ShowBoard(_GameTurn.BOARDARRAY, "After computer moves");
        }

        private void EvaluteComputerChanges(List<BoardChange> Changes)
        {
            if(Changes.Count == 2)//Must be a move because there are only two changes, From and To Destination
            {
                BoardChange to = Changes.First(f => f.StartState == GridEntry.EMPTY && f.NewState != GridEntry.EMPTY);
                BoardChange from = Changes.First(f => f.StartState != GridEntry.EMPTY && f.NewState == GridEntry.EMPTY);
                GamePiece mover = FindPiece(from.ChangeLocation);
                MovePiece(mover, to.ChangeLocation, _GameTurn.GameState);
            }

            if(Changes.Count == 3)//Must be a capture because there are three changes, From dest., To dest., and Capture Location
            {
                BoardChange to = Changes.First(f => f.StartState == GridEntry.EMPTY && f.NewState != GridEntry.EMPTY);
                BoardChange from = Changes.First(f => f.StartState == to.NewState && f.NewState == GridEntry.EMPTY);
                BoardChange captured = Changes.First(f => f.StartState != to.NewState && f.NewState == GridEntry.EMPTY);
                RemoveCapturedPiece(captured.ChangeLocation);
                MovePiece(FindPiece(from.ChangeLocation), to.ChangeLocation, _GameTurn.GameState);
            }
        }

        private IEnumerable<BoardChange> FindComputerChanges(int[,] currentBoard, int[,]computerMoves)
        {
            for(int r =0; r < BOARDSIZE; r++)
            {
                for (int c = 0; c < BOARDSIZE; c++)
                {
                    if(currentBoard[r, c] != computerMoves[r, c])
                    {
                        //AdjustBoard(new BoardLocation() { Row = r, Column = c }, (GridEntry)Enum.Parse(typeof(GridEntry), computerMoves[r, c].ToString()));

                        yield return new BoardChange() { 
                            ChangeLocation = new BoardLocation { Row = r, Column = c }, 
                            StartState = (GridEntry)Enum.Parse(typeof(GridEntry), 
                            currentBoard[r, c].ToString()), NewState = (GridEntry)Enum.Parse(typeof(GridEntry), 
                            computerMoves[r, c].ToString()) };
                    }
                }
            }

        }

        private GridEntry[] TransformBoardToAIGridArray(int[,] boardArray)
        {
            GridEntry[] transformed = new GridEntry[BOARDSIZE * BOARDSIZE];

            for(int i = 0; i < BOARDSIZE; i++)
            {
                transformed[(BOARDSIZE * i)] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 0].ToString());
                transformed[(BOARDSIZE * i) + 1] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 1].ToString());
                transformed[(BOARDSIZE * i) + 2] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 2].ToString());
                transformed[(BOARDSIZE * i) + 3] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 3].ToString());
                transformed[(BOARDSIZE * i) + 4] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 4].ToString());
                transformed[(BOARDSIZE * i) + 5] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 5].ToString());
                transformed[(BOARDSIZE * i) + 6] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 6].ToString());
                transformed[(BOARDSIZE * i) + 7] = (GridEntry)Enum.Parse(typeof(GridEntry), boardArray[i, 7].ToString());
            }

            return transformed;
        }

        private int[,] TransformGridToBoard(GridEntry[] grid)
        {
            int[,] bArray = new int[BOARDSIZE, BOARDSIZE];
            for(int i = 0; i < grid.Length; i++)
            {
                int iRow = i/8;
                int iColumn = i % 8;
                bArray[iRow, iColumn] = (int)grid[i];
            }

            return bArray;
        }

        private int[] whiteStart = { 1, 3, 5, 7, 8, 10, 12, 14, 17, 19, 21, 23 };
        private int[] blackStart = { 40, 42, 44, 46, 49, 51, 53, 55, 56, 58, 60, 62 };
        private int[] EmptyStart = { 24, 26, 28, 30, 33, 35, 37, 39, };

        private GridEntry[] SetUpInitBoard()
        {
            var emptyBoard = Enumerable.Repeat(GridEntry.NULL, 64).ToArray();
            for (int i = 0; i < 64; i++)
            {
                if (whiteStart.Contains(i))
                {
                    emptyBoard[i] = GridEntry.BLACKPAWN;
                }

                if (blackStart.Contains(i))
                {
                    emptyBoard[i] = GridEntry.WHITEPAWN;
                }

                if (EmptyStart.Contains(i))
                {
                    emptyBoard[i] = GridEntry.EMPTY;
                }
            }

            return emptyBoard;
        }

        private void MovePiece(GamePiece movingPiece, BoardLocation EndLoc, GameState gameState)
        {

            _GameTurn.GameState = GameState.INMOVE;
            AdjustBoard(movingPiece.BoardLocation, GridEntry.EMPTY);
            Controls.Remove(movingPiece as Control);
            movingPiece.BoardLocation = EndLoc;

            if (movingPiece.IsPromoted == false && (EndLoc.Row == 0 || EndLoc.Row == 7))
            {
                if (movingPiece.PieceState == GridEntry.WHITEPAWN && EndLoc.Row == 7)
                {
                    KingPiece(movingPiece, GetImage("Checkers.Assets.WhiteKing.png"), GridEntry.WHITEKING);
                }

                if (movingPiece.PieceState == GridEntry.BLACKPAWN && EndLoc.Row == 0)
                {
                    KingPiece(movingPiece, GetImage("Checkers.Assets.BlackKing.png"), GridEntry.BLACKKING);
                }
            }
            movingPiece.Location = new Point((LEFTMARGIN + TILESIZE * EndLoc.Column) + 10, (TOPMARGIN + TILESIZE * EndLoc.Row) + 10);
            this.Controls.Add(movingPiece);
            this.Controls.SetChildIndex(movingPiece, 1000);
            movingPiece.BringToFront();
            _GameTurn.GameState = gameState;
            AdjustBoard(movingPiece.BoardLocation, movingPiece.PieceState);
            TurnChange();
        }

        private void KingPiece(GamePiece movingPiece, Image KingImage, GridEntry PromotionState)
        {
            Controls.Remove(movingPiece as Control);
            movingPiece.Image = KingImage;
            movingPiece.PieceState = PromotionState;
            movingPiece.IsPromoted = true;
            Controls.Add(movingPiece);
            Controls.SetChildIndex(movingPiece, 1001);
            movingPiece.BringToFront();
            AdjustBoard(movingPiece.BoardLocation, movingPiece.PieceState);
        }

        private void RemoveCapturedPiece(BoardLocation captureLocation)
        {
            this.Controls.Remove(FindPiece(captureLocation) as Control);
            AdjustBoard(captureLocation, GridEntry.EMPTY);
        }

        private void ShowBoard(int[,] board, string sTitle)
        {
            this.mBox.AppendText(Environment.NewLine);
            this.mBox.AppendText(sTitle + Environment.NewLine);
            string str = string.Empty;
            for (int row = 0; row < BOARDSIZE; row++)
            {
                for (int col = 0; col < BOARDSIZE; col++)
                {
                    str += (board[row, col] == -1 ? " " : board[row, col].ToString()) + " ";
                }
                this.mBox.AppendText(str + Environment.NewLine);
                str = string.Empty;
            }
        }

        //Events
        private void gridCell_DragEnter(object sender, DragEventArgs e)
        {
            DestinationTile = (TilePiece)sender;
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
        }

        private void gridCell_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void piece_MouseDown(object sender, MouseEventArgs e)
        {
            //FROM
            if (!bGameStarted)
                return;

            CurrentPiece = null;
            ReplacedPiece = null;
            DestinationTile = null;

            var img = ((PictureBox)sender).Image;
            if (img == null) return;
            CurrentPiece = (GamePiece)sender;

            if ((CurrentPiece.PieceState == GridEntry.BLACKPAWN && _GameTurn.GameState == GameState.BLACKTURN
                || CurrentPiece.PieceState == GridEntry.WHITEPAWN && _GameTurn.GameState == GameState.WHITETURN
                || CurrentPiece.PieceState == GridEntry.BLACKKING && _GameTurn.GameState == GameState.BLACKTURN
                || CurrentPiece.PieceState == GridEntry.WHITEKING && _GameTurn.GameState == GameState.WHITETURN) == false)
                return;

            if (DoDragDrop(img, DragDropEffects.Move) == DragDropEffects.Move)
            {
                var currentMover = _GameTurn.GameState;
                _GameTurn.GameState = GameState.INMOVE;
                if (IsLegalMove(CurrentPiece, ReplacedPiece, DestinationTile) == false)
                {
                    _GameTurn.GameState = currentMover;
                    return;
                }

                if (DestinationTile != null && ReplacedPiece == null && CurrentPiece != null)
                {
                    MovePiece(CurrentPiece, DestinationTile.BoardLocation, currentMover);
                    return;
                }

                MessageBox.Show("Shouldn't Be here", "Mouse Down", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void piece_DragDrop(object sender, DragEventArgs e)
        {
            //TO
            //if (sender is GamePiece)
            //{
            //    ReplacedPiece = (GamePiece)sender;
            //    var bmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            //    ((GamePiece)sender).Image = bmp;
            //}
        }

        private void piece_DragEnter(object sender, DragEventArgs e)
        {
            //FROM
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Move;
        }

        private void gameCell_MouseDown(object sender, MouseEventArgs e)
        {
            var tile = (TilePiece)sender;
            this.mBox.AppendText("[Row: " + tile.BoardLocation.Row + "] [Column: " + tile.BoardLocation.Column + "]");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowBoard(_GameTurn.BOARDARRAY, "Add hoc Show");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.mBox.Clear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox)
                {
                    this.Controls.Remove(c);
                }
            }
            InitBoard();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            using (GameSelections select = new GameSelections())
            {
                result = select.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    var options = select.options;
                    _WhitePlayer.GameSide = options.WhitePlayer.GameSide;
                    _WhitePlayer.IsHuman = options.WhitePlayer.IsHuman;
                    _WhitePlayer.PlayerName = options.WhitePlayer.PlayerName;
                    _BlackPlayer.GameSide = options.BlackPlayer.GameSide;
                    _BlackPlayer.IsHuman = options.BlackPlayer.IsHuman;
                    _BlackPlayer.PlayerName = options.BlackPlayer.PlayerName;

                    this._GameTurn.GameState = GameState.BLACKTURN;
                    TakeComputerTurn();
                }
            }

            if (result == DialogResult.OK)
                bGameStarted = true;
        }

        private void BlackPlayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            txtBlackName.Text = e.PropertyName;
        }

        private void WhitePlayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            txtWhiteName.Text = e.PropertyName;
        }

        private void ChangeState(object sender, GameStateChangedEventArgs e)
        {
            this.Refresh();
        }

        private void lblWhite_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Label box = (Label)sender;
            if (this._GameTurn.GameState == GameState.WHITETURN)
            {
                Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                box.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Green);
                Graphics g = e.Graphics;
                g.DrawRectangle(p, rectBorder);
            }
            else
            {
                box.BorderStyle = BorderStyle.None;
            }
        }

        private void lblBlack_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Label box = (Label)sender;
            if (this._GameTurn.GameState == GameState.BLACKTURN)
            {
                Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                box.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Green);
                Graphics g = e.Graphics;
                g.DrawRectangle(p, rectBorder);
            }
            else
            {
                box.BorderStyle = BorderStyle.None;
            }
        }

        private void BoardArrayChanged(object sender, EventArgs e)
        {
            if (_GameTurn.GameState == GameState.DEFAULTGAME || _GameTurn.GameState == GameState.INMOVE)
                return;

            TurnChange();

            if (_GameTurn.GameState == GameState.BLACKTURN && _BlackPlayer.IsHuman == false
                || _GameTurn.GameState == GameState.WHITETURN && _WhitePlayer.IsHuman == false)
            {
                TakeComputerTurn();
            }
        }
    }

    public class BoardChange
    {
        public BoardLocation ChangeLocation { get; set; }
        public GridEntry StartState { get; set; }
        public GridEntry NewState { get; set; }
    }
}