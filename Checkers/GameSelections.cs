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
    public partial class GameSelections : Form
    {
        public GameOptions options = new GameOptions();
        public GameSelections()
        {
            InitializeComponent();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            options.WhitePlayer = new Players { GameSide = PlayerColor.WHITE, IsHuman = chkWhite.Checked, PlayerName = txtWhitePlayerName.Text };

            string blackName = string.IsNullOrWhiteSpace(txtBlackPlayerName.Text) ? "Computer Player" : txtBlackPlayerName.Text;

            options.BlackPlayer = new Players { GameSide = PlayerColor.BLACK, IsHuman = chkBlack.Checked, PlayerName = blackName };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chkWhite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBlack_CheckedChanged(object sender, EventArgs e)
        {
            txtBlackPlayerName.Enabled = chkBlack.Checked;
        }

        
    }
}
