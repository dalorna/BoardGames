namespace Checkers
{
    partial class Checkers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnShow = new System.Windows.Forms.Button();
            this.mBox = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblWhite = new System.Windows.Forms.Label();
            this.lblBlack = new System.Windows.Forms.Label();
            this.txtWhiteName = new System.Windows.Forms.Label();
            this.txtBlackName = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(659, 523);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 0;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // mBox
            // 
            this.mBox.Location = new System.Drawing.Point(559, 12);
            this.mBox.Multiline = true;
            this.mBox.Name = "mBox";
            this.mBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mBox.Size = new System.Drawing.Size(377, 510);
            this.mBox.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(859, 523);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(759, 523);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(860, 551);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblWhite
            // 
            this.lblWhite.AutoSize = true;
            this.lblWhite.Location = new System.Drawing.Point(2, 8);
            this.lblWhite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWhite.Name = "lblWhite";
            this.lblWhite.Size = new System.Drawing.Size(67, 13);
            this.lblWhite.TabIndex = 5;
            this.lblWhite.Text = "White Player";
            this.lblWhite.Paint += new System.Windows.Forms.PaintEventHandler(this.lblWhite_Paint);
            // 
            // lblBlack
            // 
            this.lblBlack.AutoSize = true;
            this.lblBlack.Location = new System.Drawing.Point(2, 537);
            this.lblBlack.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBlack.Name = "lblBlack";
            this.lblBlack.Size = new System.Drawing.Size(66, 13);
            this.lblBlack.TabIndex = 6;
            this.lblBlack.Text = "Black Player";
            this.lblBlack.Paint += new System.Windows.Forms.PaintEventHandler(this.lblBlack_Paint);
            // 
            // txtWhiteName
            // 
            this.txtWhiteName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtWhiteName.Location = new System.Drawing.Point(71, 8);
            this.txtWhiteName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtWhiteName.Name = "txtWhiteName";
            this.txtWhiteName.Size = new System.Drawing.Size(206, 15);
            this.txtWhiteName.TabIndex = 7;
            // 
            // txtBlackName
            // 
            this.txtBlackName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtBlackName.Location = new System.Drawing.Point(71, 537);
            this.txtBlackName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtBlackName.Name = "txtBlackName";
            this.txtBlackName.Size = new System.Drawing.Size(206, 15);
            this.txtBlackName.TabIndex = 8;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(559, 523);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Checkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 596);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtBlackName);
            this.Controls.Add(this.txtWhiteName);
            this.Controls.Add(this.lblBlack);
            this.Controls.Add(this.lblWhite);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.mBox);
            this.Controls.Add(this.btnShow);
            this.Name = "Checkers";
            this.Text = "Checkers AI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TextBox mBox;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblWhite;
        private System.Windows.Forms.Label lblBlack;
        private System.Windows.Forms.Label txtWhiteName;
        private System.Windows.Forms.Label txtBlackName;
        private System.Windows.Forms.Button btnStart;
    }
}

