/***************************************************
* Lab 02: TetriMatic
* 
* This lab demonstrates a basic replication of the
*   classic "Tetris" game. It comprises of two classes,
*   Shape and Block, which relate to each other is a 
*   single class file. The Shape will have one of 
*   three types and will build a list of blocks to
*   display that shape. The gameplay occurs in a GDI
*   Drawer canvas, with events for arrow keys and
*   mouseclicks.
* The score, level, and a preview of the next shape
*   are held in a modeless dialog. Information is
*   passed to the dialog through delegates with methods
*   to modify the dialog in the original calling form.
*   Preview images were drawn via paint and held in
*   a resources file in the solution. The score increases
*   for each piece that reaches the floor, and when
*   a row is completed (with a higher multiplier for
*   completing multiple rows at once).
* The lab utilizes dictionaries, lists, and queues. 
*   Several extension methods are used to find information
*   from these collections. The list of blocks within each
*   shape object is modified via a temporary list to
*   make changes when a row is completed. Statistics
*   for how many blocks of each type have been played
*   are held in a dictionary and shown in a listbox in
*   the modeless score dialog.
* Some additional enhancements, shown below, would have
*   allowed the program to replicate "Tetris" even closer
*   but were not able to be implemented in the time
*   allotted.
* 
* Author: Joel Leckie
* CMPE 2300 – OE01: Spring 2022
**************************************************/

// fun to add:
//  additional tetriminos (T, Z, and J)
//  game restart (game over screen)
//  row elimination animation
//  score multiplier bonuses per level
//  game speed multiplier per level

using GDIDrawer;
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
    public partial class Form1 : Form
    {
        // Gameplay members
        System.Diagnostics.Stopwatch sWatch = new System.Diagnostics.Stopwatch();
        Timer gameTimer = new Timer();
        Queue<Shape> allShapes;
        Shape currentShape = null;
        LinkedList<Shape> floor = new LinkedList<Shape>();
        bool keyUp = false;
        bool keyDown = false;
        bool keyLeft = false;
        bool keyRight = false;
        bool clickL = false;
        bool clickR = false;

        // Enhancement members
        int level = 0;
        int score = 0;
        ScoreWindow dlg = null;
        Dictionary<Shape.Type, int> piecesPlayed;

        public Form1()
        {
            InitializeComponent();
            // set and enable timer and stopwatch in the form constructor
            gameTimer.Interval = 50;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            sWatch.Start();
        }

        // Button events to move pieces (key handler)
        private void Canvas_MouseRightClick(Point pos, CDrawer dr) => clickR = true;

        private void Canvas_MouseLeftClick(Point pos, CDrawer dr) => clickL = true;

        private void Canvas_KeyboardEvent(bool bIsDown, Keys keyCode, CDrawer dr)
        {
            keyUp = (keyCode == Keys.Up && bIsDown) ? true : false;
            keyDown = (keyCode == Keys.Down && bIsDown) ? true : false;
            keyLeft = (keyCode == Keys.Left && bIsDown) ? true : false;
            keyRight = (keyCode == Keys.Right && bIsDown) ? true : false;
        } // end of event handlers

        private void btn_GameStart_Click(object sender, EventArgs e)
        {
            int scale = (int)setScale.Value; //initial scale at 50, x10 (max 500 pixels wide)
            // Create CDrawer with the calculated width and twice the height, setting the scale
            Block.canvas = new CDrawer(scale * 10, scale * 20, true) { Scale = scale };
            Block.canvas.Position = new Point(500, 0);
            // Event handlers now that a canvas has been generated
            Block.canvas.KeyboardEvent += Canvas_KeyboardEvent;
            Block.canvas.MouseLeftClick += Canvas_MouseLeftClick;
            Block.canvas.MouseRightClick += Canvas_MouseRightClick;

            // initialize the dictionary for piece statistics
            piecesPlayed = new Dictionary<Shape.Type, int>();
            piecesPlayed[Shape.Type.Angle] = 0;
            piecesPlayed[Shape.Type.Line] = 0;
            piecesPlayed[Shape.Type.Square] = 0;

            // check if a score window exists
            if (dlg == null)
            {
                // create a new score window
                dlg = new ScoreWindow();
                dlg._scoreUpdate = new del_score(UpdateScore);
                dlg._levelUpdate = new del_level(UpdateLevel);
            }

            // handles all Queue creation, population, floor clearing, initial piece preview
            NewLevel();
            // disable the start button so only one game can be played at a time
            btn_GameStart.Enabled = false;
            
            // show the score dialog and reset the score & level
            dlg.Show();
            dlg._scoreUpdate(0);
            dlg._levelUpdate(0);

           
        } // end of btn_GameStart_Click()

         // Primary game operation
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (Block.canvas == null) return; // no game running, nothing to see
            if (currentShape == null) return; // something went wrong, back away
            
            if ((keyDown) || (sWatch.ElapsedMilliseconds > 1000)) // drop piece or add to floor
            {
                // update to the next piece if needed
                if (!currentShape.falling)
                {
                    // go to next level if out of pieces
                    if (allShapes.Count == 0) NewLevel();
                    else
                    {
                        currentShape = allShapes.Dequeue();
                        // increase the piece statistics
                        piecesPlayed[currentShape.type] += 1;
                        // increase the score
                    }
                    UpdateScore(10);
                }
                // check, and drop, the shape if it can
                currentShape.Drop(floor);
                //if (Shape.NotFalling(currentShape)) currentShape = null; // breaks getting the next piece, had to remove
                
                // stop, reset, and start the stopwatch all in one
                sWatch.Restart();
            }

            // shift the Shape to the left, if possible
            if ((keyLeft) || (clickL))
            {
                currentShape.ShiftLeft(floor);
                clickL = false;
            }

            // shift the Shape to the right, if possible
            if ((keyRight) || (clickR))
            {
                currentShape.ShiftRight(floor);
                clickR = false;
            }

            // first enhancement, rotate the Shape with the up key
            if (keyUp)
            {
                foreach (Block block in currentShape.blocks) block.Rotate();
                while (currentShape.blocks.Any(x => x.X < 0)) 
                    currentShape.ShiftRight(floor);
                while (currentShape.blocks.Any(x => x.X >= Block.canvas.ScaledWidth))
                    currentShape.ShiftLeft(floor);
            }

            // render all the floor shapes and the current shape
            Block.canvas.Clear();
            foreach (Shape shape in floor) shape.ShowShape();
            if (currentShape != null) currentShape.ShowShape();

            // preview the next piece
            NextPiece();

            // checks if any rows are full and can be eliminated
            CheckFloor();
        } // end of GameTimer_Tick()

        private void NewLevel()
        {
            dlg._levelUpdate(1); // proceed to next level
            allShapes = new Queue<Shape>(20); // create a 20 item Queue
            for (int i = 0; i < 20; i++)
            {
                // populate queue with 20 new Shapes (top-left block centered at top)
                allShapes.Enqueue(new Shape(new Point(Block.canvas.ScaledWidth / 2, 0)));
            }
            
            // clear the current floor
            floor.Clear();
            // move the first piece to the game board
            currentShape = allShapes.Dequeue();
            piecesPlayed[currentShape.type] += 1;
            // preview the next piece
            NextPiece();
        } // end of NewLevel()

        private void NextPiece()
        {
            // adjust the preview box to display the next shape in the queue
            if (allShapes.Count > 0) switch (allShapes.Peek().type)
                {
                    case Shape.Type.Square:
                        dlg.pb_Preview.Image = Properties.Resources.Square;
                        break;
                    case Shape.Type.Line:
                        dlg.pb_Preview.Image = Properties.Resources.Line;
                        break;
                    case Shape.Type.Angle:
                        dlg.pb_Preview.Image = Properties.Resources.El;
                        break;
                }
            
            // update the preview box to show as empty if on the last piece of the level
            else dlg.pb_Preview.Image = null;
            dlg.pb_Preview.Update();
        } // end of NextPiece()

        private void UpdateScore(int x)
        {
            // update the score and display it in the dialog
            if (x == 0) score = 0;
            else score += x;
            dlg.lbl_Score.Text = $"Score: " + score;
            dlg.lb_Pieces.Items.Clear();
            dlg.lb_Pieces.Items.Add($"Angle Pieces: {piecesPlayed[Shape.Type.Angle].ToString()}");
            dlg.lb_Pieces.Items.Add($"Line Pieces:  {piecesPlayed[Shape.Type.Line].ToString()}");
            dlg.lb_Pieces.Items.Add($"Square Pieces: {piecesPlayed[Shape.Type.Square].ToString()}");
        } // end of UpdateScore()

        private void UpdateLevel(int x)
        {
            // update the level and display it in the dialog
            if (x == 0) level = 1;
            else level += x;
            dlg.lbl_Level.Text = $"Level: " + level;
        } // end of UpdateLevel()

        private void CheckFloor()
        {
            // build a dictionary of rows to calculate how many blocks each one contains
            Dictionary<int, int> lineCount = new Dictionary<int, int>();
            for (int i = 0; i < Block.canvas.ScaledHeight; i++)
                lineCount.Add(i, 0);

            // check if there are any complete lines on the floor
            foreach (Shape shape in floor)
            {
                foreach (Block block in shape.blocks)
                {
                    // count the blocks in each row
                    lineCount[block.Y]++;
                }
            }

            // count the lines removed as a score multiplier
            int linesRemoved = 0;

            // iterate through each row
            foreach (int row in lineCount.Keys)
            {
                // check if the row is full (can hold 10 blocks wide)
                if (lineCount[row] >= 10)
                {
                    linesRemoved++; // add to score multiplier
                    foreach (Shape shape in floor)
                    {
                        // create a temporary list that can be modified safely
                        List<Block> tempList = new List<Block>(shape.blocks);
                        shape.blocks.ForEach(x =>
                        {
                            // remove all blocks from shapes where those blocks are in a full row
                            if (x.Y == row) tempList.Remove(x);
                        });

                        // save the modifications to the original list
                        shape.blocks = tempList;
                        shape.blocks.ForEach(x =>
                        {
                            // drop down any blocks from rows above what was removed
                            if (x.Y < row) tempList[shape.blocks.IndexOf(x)].Yoffset++;
                        });

                        // save the modifications to the original list again
                        shape.blocks = tempList;
                    }
                }
            }

            // add to the score for each row that was eliminated, giving bonus points for multiple rows at a time
            if (linesRemoved > 0) UpdateScore(50 * linesRemoved * linesRemoved);
        } // end of CheckFloor()
    }
}
