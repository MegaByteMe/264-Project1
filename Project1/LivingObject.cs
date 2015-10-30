/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 2
 * Change: 6
 * Status: Operational - Light Bugs - Expanding Functionality
 * Notes:
 * Methods:
 * Dependencies:
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    abstract class LivingObject : AdvObject
    {
        // ==================== Variables Section ====================
        private bool alive = true, lootable = true, aggressive = false;
        private int health = 100, sightrange = 3, moverange = 1;
        private string age = null;
        private LivingObject aggresor = null;
        private Item lhand = null, rhand = null;

        // ==================== Fields Section =======================
        #region FIELDS

        public Item LHand
        {
            get { return lhand; }
            set { lhand = value; }
        }

        public Item RHand
        {
            get { return rhand; }
            set { rhand = value; }
        }

        public String Age
        {
            get { return age; }
            set { age = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public bool Lootable
        {
            get { return lootable; }
            set { lootable = value; }
        }

        public bool Aggressive
        {
            get { return aggressive; }
            set { aggressive = value; }
        }

        public LivingObject Aggressor
        {
            get { return aggresor; }
            set { aggresor = value; }
        }

        public int SightRange
        {
            get { return sightrange; }
            set { sightrange = value; }
        }

        public int MoveRange
        {
            get { return moverange; }
            set { moverange = value; }
        }
        #endregion

        // ==================== Methods Section ======================
        public void Look(Tile[,] world, int a, int b, int c, int d)
        {
            String peoples = "";

            Console.WriteLine("{0} looks around and sees: ", this.Name);
            Console.Write("Items: ");
            for (int j = b; j <= d; j++)
            {
                for (int i = a; i <= c; i++)
                { 
                    if(world[i, j].Holds.Count > 0) 
                    {
                        world[i, j].Holds.ForEach(PrintName);
                    }
                //    if (world[i, j].Occupied != null && i != this.LocX && j != this.LocY)
                    if (world[i, j].Occupied != null && world[i,j].Occupied.Name != this.Name)
                    {
                        peoples += world[i, j].Occupied.Name + " The " + world[i, j].Occupied.Title + ", ";
                    }
                }
            } // End for
                        
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(peoples);
            Console.ResetColor();
            Console.WriteLine("");
        }

        public void AdjBox(out int xstar, out int ystar, out int xend, out int yend, int MAXSX, int MAXSY, int range) //Ensure we dont out of scope
        {
            if (this.LocX + range > MAXSX)  // Xend
                xend = MAXSX - 1;
            else
                xend = this.LocX + range;

            if (this.LocX - range < 0)          // Xstart
                xstar = 0;
            else
                xstar = this.LocX - range;

            if (this.LocY + range > MAXSY)  //Yend
                yend = MAXSY - 1;
            else
                yend = this.LocY + range;

            if (this.LocY - range < 0)          //Ystart
                ystar = 0;
            else
                ystar = this.LocY - range;
        }

        public void MoveLO(Tile [,] world, int x, int y)
        {
            world[x, y].Occupied = this;
            world[LocX, LocY].Occupied = null;
            LocX = x;
            LocY = y;
            Console.WriteLine("{0} Moved into {1} ", this.Name, world[this.LocX, this.LocY].Name);
            Console.WriteLine(world[this.LocX, this.LocY].Desc);
        }
 
        private void PrintName(AdvObject a)         // Delegate style for look to see into the item list
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if( a.Visible == true)
                Console.Write(a.Name + ", ");
            Console.ResetColor();
        }

        public void DspHolding()
        {
            if (this.LHand != null && this.RHand != null)
                Console.WriteLine(this.Name + "'s left hand is holding " + this.LHand.Name + " and right hand is holding " + this.RHand.Name);
            else if (this.LHand != null && this.RHand == null)
                Console.WriteLine(this.Name + "'s left hand is holding " + this.LHand.Name + " and right hand is holding nothing.");
            else if (this.LHand == null && this.RHand != null)
                Console.WriteLine(this.Name + "'s left hand is holding nothing and right hand is holding " + this.RHand.Name);
            else
                Console.WriteLine(this.Name + "'s hand's are empty.");
        }

        public bool ChkTileMove(Tile T)             // Validate move observes map tile rules
        {
            if ((T.Name == "Entry" | T.Name == T.Name | T.Name == "Entry") && T.Passable == true && T != null
                    && T.PassReq == false && T.Occupied == null)
                return true;
            else if ((T.Name == "Entry" | T.Name == T.Name | T.Name == "Entry") && T.Passable == true && T != null
                    && T.PassReq == true && (this.LHand != null | this.RHand != null) && T.Occupied == null)
            {
                if (this.LHand != null)
                    if (this.LHand.Name.ToString() == T.PassPair.Name.ToString())
                        return true;
                    else
                        return false;
                else if (this.RHand != null)
                    if (this.RHand.Name.ToString() == T.PassPair.Name.ToString())
                        return true;
                    else
                        return false;
                else
                    return false;
            }
            else
                return false;
        }

    } // End Class
} // End Namespace
