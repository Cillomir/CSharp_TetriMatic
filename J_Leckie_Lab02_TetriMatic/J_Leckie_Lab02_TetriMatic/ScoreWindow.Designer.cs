
namespace J_Leckie_Lab02_TetriMatic
{
    partial class ScoreWindow
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
            this.lbl_Preview = new System.Windows.Forms.Label();
            this.pb_Preview = new System.Windows.Forms.PictureBox();
            this.lbl_Score = new System.Windows.Forms.Label();
            this.lbl_Level = new System.Windows.Forms.Label();
            this.lb_Pieces = new System.Windows.Forms.ListBox();
            this.lbl_Statistics = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Preview
            // 
            this.lbl_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Preview.Location = new System.Drawing.Point(138, 8);
            this.lbl_Preview.Name = "lbl_Preview";
            this.lbl_Preview.Size = new System.Drawing.Size(72, 13);
            this.lbl_Preview.TabIndex = 2;
            this.lbl_Preview.Text = "Next Piece:";
            // 
            // pb_Preview
            // 
            this.pb_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pb_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_Preview.Location = new System.Drawing.Point(171, 29);
            this.pb_Preview.Name = "pb_Preview";
            this.pb_Preview.Size = new System.Drawing.Size(50, 50);
            this.pb_Preview.TabIndex = 4;
            this.pb_Preview.TabStop = false;
            // 
            // lbl_Score
            // 
            this.lbl_Score.Location = new System.Drawing.Point(12, 33);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(120, 24);
            this.lbl_Score.TabIndex = 1;
            this.lbl_Score.Text = "Score: 0";
            this.lbl_Score.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Level
            // 
            this.lbl_Level.Location = new System.Drawing.Point(12, 9);
            this.lbl_Level.Name = "lbl_Level";
            this.lbl_Level.Size = new System.Drawing.Size(120, 24);
            this.lbl_Level.TabIndex = 0;
            this.lbl_Level.Text = "Level: 0";
            this.lbl_Level.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Pieces
            // 
            this.lb_Pieces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_Pieces.FormattingEnabled = true;
            this.lb_Pieces.Location = new System.Drawing.Point(12, 106);
            this.lb_Pieces.Name = "lb_Pieces";
            this.lb_Pieces.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lb_Pieces.Size = new System.Drawing.Size(120, 69);
            this.lb_Pieces.TabIndex = 4;
            this.lb_Pieces.TabStop = false;
            // 
            // lbl_Statistics
            // 
            this.lbl_Statistics.AutoSize = true;
            this.lbl_Statistics.Location = new System.Drawing.Point(12, 68);
            this.lbl_Statistics.Name = "lbl_Statistics";
            this.lbl_Statistics.Size = new System.Drawing.Size(81, 13);
            this.lbl_Statistics.TabIndex = 3;
            this.lbl_Statistics.Text = "Shapes Played:";
            // 
            // ScoreWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 187);
            this.Controls.Add(this.lbl_Statistics);
            this.Controls.Add(this.lb_Pieces);
            this.Controls.Add(this.lbl_Level);
            this.Controls.Add(this.lbl_Score);
            this.Controls.Add(this.lbl_Preview);
            this.Controls.Add(this.pb_Preview);
            this.Name = "ScoreWindow";
            this.Text = "ScoreWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScoreWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Preview;
        public System.Windows.Forms.Label lbl_Score;
        public System.Windows.Forms.Label lbl_Level;
        private System.Windows.Forms.Label lbl_Statistics;
        public System.Windows.Forms.ListBox lb_Pieces;
        public System.Windows.Forms.PictureBox pb_Preview;
    }
}