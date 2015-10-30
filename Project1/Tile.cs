/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 2
 * Change: 1
 * Status: Following Functionality
 * Notes:
 * Methods:
 * Dependencies:    AdvObject.cs
 *                  Container.cs
 *                  LivingObject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Tile : Container
    {
        // ==================== Variables Section ====================
        private bool passable = true, passreq = false;
        private LivingObject occupied;
        private Item passpair = null;

        // ==================== Fields Section =======================
        public bool Passable
        {
            get { return passable; }
            set { passable = value; }
        }

        public Item PassPair
        {
            get { return passpair; }
            set { passpair = value; }
        }

        public bool PassReq
        {
            get { return passreq; }
            set { passreq = value; }
        }

        public LivingObject Occupied
        {
            get { return occupied; }
            set { occupied = value; }
        }
        
        // ==================== Constructors ======================
        public Tile( Tile C )
        {
            this.Passable = C.Passable;
            this.Visible = C.Visible;
            this.Occupied = C.Occupied;

            // Inherited From Container
            this.Holds = C.Holds;
            this.Openwith = C.Openwith;
            this.Locked = C.Locked;
            this.Capacity = C.Capacity;

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

        public Tile()
        {

        }
        
        // ==================== Methods Section ======================

    } // End Class
} // End Namespace
