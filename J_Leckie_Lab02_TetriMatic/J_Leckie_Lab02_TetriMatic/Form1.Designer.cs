
namespace J_Leckie_Lab02_TetriMatic
{
    partial class Form1
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
            this.setScale = new System.Windows.Forms.NumericUpDown();
            this.btn_GameStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.setScale)).BeginInit();
            this.SuspendLayout();
            // 
            // setScale
            // 
            this.setScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setScale.Location = new System.Drawing.Point(12, 12);
            this.setScale.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.setScale.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.setScale.Name = "setScale";
            this.setScale.Size = new System.Drawing.Size(120, 20);
            this.setScale.TabIndex = 0;
            this.setScale.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // btn_GameStart
            // 
            this.btn_GameStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GameStart.Location = new System.Drawing.Point(157, 12);
            this.btn_GameStart.Name = "btn_GameStart";
            this.btn_GameStart.Size = new System.Drawing.Size(75, 23);
            this.btn_GameStart.TabIndex = 1;
            this.btn_GameStart.Text = "Start Game";
            this.btn_GameStart.UseVisualStyleBackColor = true;
            this.btn_GameStart.Click += new System.EventHandler(this.btn_GameStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 48);
            this.Controls.Add(this.btn_GameStart);
            this.Controls.Add(this.setScale);
            this.Name = "Form1";
            this.Text = "Tetrimatic - Joel Leckie";
            ((System.ComponentModel.ISupportInitialize)(this.setScale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown setScale;
        private System.Windows.Forms.Button btn_GameStart;
    }
}

