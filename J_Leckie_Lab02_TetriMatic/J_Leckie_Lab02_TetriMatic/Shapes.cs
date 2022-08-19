using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;

namespace J_Leckie_Lab02_TetriMatic
{
    #region Shape
    class Shape
    {
        // a public enumeration of shape types Square, Line, Angle
        public enum Type
        {
            Square,
            Line,
            Angle // image is El
            // three additional pieces (T, Z, and J) not needed
        }

        // an automatic property of the enumeration Shape type, hidden setter
        private Type _type;
        public Type type { get => _type; set => _type = value; }

        // a public automatic property of type bool whether the Shape is falling
        public bool falling { get; set; }

        // a public automatic property of type Point for the Shape core location, hidden setter
        private Point _pos;
        public Point pos { get => _pos; set => _pos = value; }

        // a member that is a List of Blocks comprising the shape, using the initializer
        public List<Block> blocks;

        // two predicates returning true if the Shape is falling or not falling 
        public static Predicate<Shape> Falling = (Shape check) => check.falling;
        public static Predicate<Shape> NotFalling = (Shape check) => !check.falling;

        // no static properties, so no static constructor required

        // instance constructor accepting a Point for the Shape base location
        public Shape(Point location) 
        {
            // falling flag to true, random color, assign member location
            falling = true;
            Color color = RandColor.GetKnownColor();
            pos = location;

            // randomly assign enumeration type
            type = (Type)Block.rnd.Next(3);
            
            // initialize list of blocks
            blocks = new List<Block>();
            // add respective blocks based on type, passing "this" to each one
            blocks.Add(new Block(this, 0, 0, color)); // included in all shapes
            blocks.Add(new Block(this, 1, 0, color)); // included in all shapes
            switch (type)
            {
                case Type.Square:
                    blocks.Add(new Block(this, 0, 1, color));
                    blocks.Add(new Block(this, 1, 1, color));
                    break;
                case Type.Line:
                    blocks.Add(new Block(this, 2, 0, color));
                    blocks.Add(new Block(this, 3, 0, color));
                    break;
                case Type.Angle:
                    blocks.Add(new Block(this, 2, 0, color));
                    blocks.Add(new Block(this, 3, 0, color));
                    blocks.Add(new Block(this, 3, -1, color));
                    break;
                default:
                    break;
            }
        } // end of Shape()

        // override Equals using an extension method to determine any overlap in blocks between two shapes
        public override bool Equals(object obj)
        {
            if (!(obj is Shape other)) return false; // validate & unbox Shape
            foreach (Block check in other.blocks) // check every block in the argument Shape
                if (blocks.Contains(check)) return true; // against the list of blocks in "this" Shape
            return false;
        } // end of Equals()

        // override GetHashCode, make it nice and unique
        public override int GetHashCode() => (pos.X * 100 + pos.Y) * (int)this.type;
        
        // create a method to show the Shapes blocks
        public void ShowShape()
        {
            foreach (Block block in blocks) 
                block.ShowBlock();
        } // end of ShowShape()

        // Create a method, accepting a LinkedList of Shapes representing the current floor, returning nothing
        public void Drop(LinkedList<Shape> floor)
        {
            // if not falling, return
            if (!this.falling) return;

            // if blocks have hit the floor, set falling to false and add to the end of linked list
            if (!(this.blocks.TrueForAll(Block.CanFall)))
            {
                this.falling = false;
                floor.AddLast(this);
                return;
            }

            // test if the shape can be safely dropped by assigning a reversable Point
            Point oldPos = pos;
            this.pos = new Point(this.pos.X, this.pos.Y + 1);
            // verify the new Point is a valid position
            if (TouchingFloor(floor))
            {
                // otherwise revert it back, set falling as false, and add it to the floor
                pos = oldPos;
                falling = false;
                floor.AddLast(this);
            }
        } // end of Drop()

        // create a method, accepting a LinkedList for the floor, to check if shifting left is possible
        public void ShiftLeft(LinkedList<Shape> floor)
        {
            Point oldPos = pos;
            this.pos = new Point(this.pos.X - 1, this.pos.Y);
            if (TouchingFloor(floor)) pos = oldPos;
            else if (this.blocks.All(x => x.CanShiftLeft(x))) return;
            else pos = oldPos;
        } // end of ShiftLeft()

        // create a method, accepting a LinkedList for the floor, to check if shifting right is possible
        public void ShiftRight(LinkedList<Shape> floor)
        {
            Point oldPos = pos;
            this.pos = new Point(this.pos.X + 1, this.pos.Y);
            if (TouchingFloor(floor)) pos = oldPos;
            else if (this.blocks.All(x => x.CanShiftRight(x))) return;
            else pos = oldPos;
        } // end of ShiftRight()

        // check if any blocks in the shape are touching any floor blocks
        private bool TouchingFloor(LinkedList<Shape> floor) 
        {
            bool valid = false;
            foreach (Shape inFloor in floor)
            {
                foreach (Block item in inFloor.blocks)
                    if (blocks.Contains(item)) valid = true;
            }
            return valid;
        } // end of TouchingFloor()
    }
    #endregion

    #region Block
    class Block
    {
        // Include a static CDrawer object initialized to null with an accesible manual property
        private static CDrawer Canvas = null;
        public static CDrawer canvas
        {
            // getter returns the current CDrawer member
            get { return Canvas; }
            // setter with close an existing CDrawer before assigning the new value
            set 
            {
                if (Canvas != null) Canvas.Close();
                Canvas = value;
            }
        }

        // include a static automatic Random property with a public getter and hidden setter
        public static Random rnd { get; }

        // reference of type Shape, initialized to null, representing the Blocks parent
        public Shape parentShape = null;
        public Shape shape
        {
            get { return parentShape; }
            set { parentShape = value; }
        }

        // two int fields for the block offset (no property)
        public int Xoffset;
        public int Yoffset;

        // two manual int properties for parents X/Y coordinates
        public int X 
        {
            get { return shape.pos.X + Xoffset; } 
        }
        public int Y 
        {
            get { return shape.pos.Y + Yoffset; }
        }

        // a blocks type color
        private Color color;

        // add an appropriate static constructor (static CDrawer, static Random)
        static Block()
        {
            Canvas = null;
            rnd = new Random();
        }

        // add an instance constructor accepting the parent Shape, X & Y offsets, and a color
        public Block(Shape shape, int offsetX, int offsetY, Color color)
        {
            parentShape = shape;
            Xoffset = offsetX;
            Yoffset = offsetY;
            this.color = color;
        }

        // add a public method (no args, no return) to add a block (size 1) at its appropriately offset position
        public void ShowBlock()
        {
            canvas.AddCenteredRectangle(X, Y, 1, 1, color, 1, color == Color.White ? Color.Black : Color.White);
        }

        // override Equals so blocks are equal if they have identical offset positions
        public override bool Equals(object obj)
        {
            if (!(obj is Block other)) return false;
            return X == other.X && Y == other.Y;
        }

        // override GetHashCode since Equals was overridden
        public override int GetHashCode() => (100 * X) * Y;

        // add a public method (no args, no return) to rotate the block around the parent Shape
        public void Rotate()
        {
            int tempX = Xoffset;
            Xoffset = Yoffset;
            Yoffset = -tempX;
        }

        // add predicates so block won't go out of bounds left, or out of bounds right
        public Predicate<Block> CanShiftLeft = (Block check) => check.X + 1 > 0;
        public Predicate<Block> CanShiftRight = (Block check) => check.X < Canvas.ScaledWidth;

        // add a predicate if block is not at the bottom of the CDrawer
        public static Predicate<Block> CanFall = (Block check) => check.Y + 1 < Canvas.ScaledHeight;
    }
    #endregion
}