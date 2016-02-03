using System.ComponentModel;

namespace Checkers
{
    public class Players : INotifyPropertyChanged
    {
        public PlayerColor GameSide { get; set; }
        public string PlayerName
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    RaisePropertyEvent(_Name);
                }
            }
        }
        private string _Name = string.Empty;


        public bool IsHuman { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyEvent(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum PlayerColor
    {
        UNASSIGNED = 0,
        BLACK = 1,
        WHITE = 2
    }
}