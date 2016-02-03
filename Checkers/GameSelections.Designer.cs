namespace Checkers
{
    partial class GameSelections
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtWhitePlayerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBlackPlayerName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkWhite = new System.Windows.Forms.CheckBox();
            this.chkBlack = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "White Player Name";
            // 
            // txtWhitePlayerName
            // 
            this.txtWhitePlayerName.Location = new System.Drawing.Point(254, 68);
            this.txtWhitePlayerName.Name = "txtWhitePlayerName";
            this.txtWhitePlayerName.Size = new System.Drawing.Size(284, 26);
            this.txtWhitePlayerName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Black Player Name";
            // 
            // txtBlackPlayerName
            // 
            this.txtBlackPlayerName.Enabled = false;
            this.txtBlackPlayerName.Location = new System.Drawing.Point(254, 98);
            this.txtBlackPlayerName.Name = "txtBlackPlayerName";
            this.txtBlackPlayerName.Size = new System.Drawing.Size(284, 26);
            this.txtBlackPlayerName.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(254, 130);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(426, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkWhite
            // 
            this.chkWhite.AutoSize = true;
            this.chkWhite.Checked = true;
            this.chkWhite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWhite.Enabled = false;
            this.chkWhite.Location = new System.Drawing.Point(12, 70);
            this.chkWhite.Name = "chkWhite";
            this.chkWhite.Size = new System.Drawing.Size(87, 24);
            this.chkWhite.TabIndex = 7;
            this.chkWhite.Text = "Human";
            this.chkWhite.UseVisualStyleBackColor = true;
            this.chkWhite.CheckedChanged += new System.EventHandler(this.chkWhite_CheckedChanged);
            // 
            // chkBlack
            // 
            this.chkBlack.AutoSize = true;
            this.chkBlack.Location = new System.Drawing.Point(12, 100);
            this.chkBlack.Name = "chkBlack";
            this.chkBlack.Size = new System.Drawing.Size(87, 24);
            this.chkBlack.TabIndex = 8;
            this.chkBlack.Text = "Human";
            this.chkBlack.UseVisualStyleBackColor = true;
            this.chkBlack.CheckedChanged += new System.EventHandler(this.chkBlack_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Perpetua Titling MT", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(542, 47);
            this.label3.TabIndex = 9;
            this.label3.Text = "Please make your game selections";
            // 
            // GameSelections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 177);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkBlack);
            this.Controls.Add(this.chkWhite);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtBlackPlayerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWhitePlayerName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GameSelections";
            this.Text = "GameSelections";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWhitePlayerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBlackPlayerName;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkWhite;
        private System.Windows.Forms.CheckBox chkBlack;
        private System.Windows.Forms.Label label3;
    }
}