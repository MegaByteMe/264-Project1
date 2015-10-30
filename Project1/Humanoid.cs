/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 1
 * Change: 4
 * Status: Following Functionality
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
    class Humanoid : LivingObject
    {
        // ==================== Variables Section ====================
        private Container carrying = new Container();
        private bool drunk = false;

        // ==================== Fields Section =======================
        public Container Carrying
        {
            get { return carrying; }
            set { carrying = value; }
        }

        public bool Drunk
        {
            get { return drunk; }
            set { drunk = value; }
        }

        // ==================== Constructors ======================
        public Humanoid( Humanoid C )
        {
            this.Carrying = C.Carrying;
            this.LHand = C.LHand;
            this.RHand = C.RHand;

            // Inherited From LivingObject
            this.Age = C.Age;
            this.Alive = C.Alive;
            this.Health = C.Health;
            this.Lootable = C.Lootable;
            this.Aggressive = C.Aggressive;
            this.Aggressor = C.Aggressor;
            this.SightRange = C.SightRange;
            this.MoveRange = C.MoveRange;

            // Inherited From AdvObject
            this.Color = C.Color;
            this.Title = C.Title;
            this.Visible = C.Visible;
            this.LocX = C.LocX;
            this.LocY = C.LocY;
            this.Name = C.Name;
            this.Desc = C.Desc;
            this.Id = C.Id;
        }

        public Humanoid(String nam, String tit, String des, int Xloc, int Yloc )
        {
            Name = nam;
            Title = tit;
            Desc = des;
            LocX = Xloc;
            LocY = Yloc;
        }

        public Humanoid()
        {
        }

        // ==================== Methods Section ======================
        public void GetItem(Tile[,] world, int a, int b, int c, int d, String n)  // ISSUES
        {
            int g = 0, w = 0, x = 0, y = 0;

            for (int j = b; j <= d; j++)
            {
                for (int i = a; i <= c; i++)
                {
                    if (world[i, j].Holds != null)
                    {
                        w = 0;
                        foreach (Item it in world[i,j].Holds)
                        {
                            if (it.Name == n)
                            {
                                if(it.Baggable == true) {
                                    g = w;
                                    this.Carrying.Holds.Add(it);
                                    x = i;
                                    y = j;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(Name + " received " + it.Name);
                                    Console.ResetColor();
                                }
                            }
                            w++;
                        }
                    }
                }
            } // End for
            world[x, y].Holds.RemoveAt(g);
        }

        public void ShowBag()
        {
            Console.WriteLine( this.Name + "'s bag contains: ");

            if (this.Carrying.Holds == null)
                Console.WriteLine("Nothing, nada, zilch, zero, not a thing! Go find something already");
            else
                this.Carrying.Holds.ForEach(PrintBag);
            Console.WriteLine();
        }

        public void LHandToBag()
        {
            if (this.LHand != null)
            {
                this.Carrying.Holds.Add(this.LHand);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(this.Name + " placed " + this.LHand.Name + " in their bag.");
                this.LHand = null;
                Console.ResetColor();
            }
            else
                Console.WriteLine("You aren't holding anything in your left hand. Are you going to put nothing in your bag?");
        }

        public void RHandToBag()
        {
            if (this.RHand != null)
            {
                this.Carrying.Holds.Add(this.RHand);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(this.Name + " placed " + this.RHand.Name + " in their bag.");
                this.RHand = null;
                Console.ResetColor();
            }
            else
                Console.WriteLine("You aren't holding anything in your right hand. Are you going to put nothing in your bag?");
        }

        // ============== Private Methods Section =================
        private void PrintBag(Item a)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(a.Name);
            Console.ResetColor();
            Console.WriteLine(a.Desc);
            Console.WriteLine();
        }

    } // End Class
} // End Namespace