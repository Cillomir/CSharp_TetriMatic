using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace J_Leckie_Lab02_TetriMatic
{
    // create delegates to recieve information from the other form
    public delegate void del_score(int x);
    public delegate void del_level(int x);
    public delegate void del_stats(int angle, int line, int square);

    public partial class ScoreWindow : Form
    {
        // variables assigned to the delegate to store the information
        public del_score _scoreUpdate = null;
        public del_level _levelUpdate = null;
        public del_stats _statsUpdate = null;

        public ScoreWindow()
        {
            InitializeComponent();
        }

        // intercept the dialog from being manually closed by the user
        private void ScoreWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // stop it from closing
                Hide(); // hide the dialog instead
            }
        }
    }
}
