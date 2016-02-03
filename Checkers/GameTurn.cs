using System;
using System.ComponentModel;

namespace Checkers
{
    public class GameTurn : INotifyPropertyChanged
    {
        private GameState _GameState = GameState.DEFAULTGAME;
        public GameState GameState
        {
            get { return this._GameState; }
            set
            {
                if(_GameState != value)
                {
                    this._GameState = value;
                    OnGameStateChanged(_GameState);

                }

            }
        }

        public BoardArray<int> BOARDARRAY
        {
            get { return _BOARD_ARRAY; }
            set
            {
                if(_BOARD_ARRAY != value)
                {
                    _BOARD_ARRAY = value;
                }
            }
        }

        private BoardArray<int> _BOARD_ARRAY;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseGameChanged(GameState gameState)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(gameState.ToString()));
            }
        }

        public EventHandler<GameStateChangedEventArgs> GameChanged;

        protected virtual void OnGameStateChanged(GameState state)
        {
            if(GameChanged != null)
            {
                GameChanged(this, new GameStateChangedEventArgs() { GameState = state });
            }
        }

        public EventHandler BoardArrayChangedEventHandler;

        protected virtual void RaiseBoardArrayChanged()
        {
            if (BoardArrayChangedEventHandler != null)
            {
                BoardArrayChangedEventHandler(this, EventArgs.Empty);
            }
        }

    }

    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState GameState { get; set; }
    }

    public class BoardArray<T>
    {
        protected T[,] arr;

        // constructor
        public BoardArray(int cols, int rows)
        {
            arr = new T[cols, rows];
        }

        // indexer
        public T this[int a, int b]
        {
            get
            {
                return arr[a, b];
            }
            set
            {
                if(arr[a, b].Equals(value) == false)
                {
                    arr[a, b] = value;
                    RaiseArrayChanged();
                }             
            }
        }

        public EventHandler ArrayChanged;

        protected virtual void RaiseArrayChanged()
        {
            if (ArrayChanged != null)
            {
                ArrayChanged(this, EventArgs.Empty);
            }
        }
    }

    public enum GameState
    {
        DEFAULTGAME = 0,
        BLACKTURN = 1, 
        WHITETURN = 2,
        BLACKWIN = 3,
        WHITEWIN = 4,
        INMOVE = 5
    }
}