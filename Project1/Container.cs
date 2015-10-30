/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 2
 * Change: 2
 * Status: Following Functionality
 * Notes:
 * Methods:
 * Dependencies:    AdvObject.cs
 *                  Container.cs
 *                  Item.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Container : AdvObject
    {
        // ==================== Variables Section ====================
        private int capacity = 5;
        private bool locked = false;
        private Item openwith = null;
        public List<Item> holds = new List<Item>();

        // ==================== Fields Section =======================
        #region FIELDS
        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        public Item Openwith
        {
            get { return openwith; }
            set { openwith = value; }
        }

        public List<Item> Holds
        {
            get { return holds; }
            set { holds = value; }
        }
        #endregion

        // ==================== Constructors ======================
        public Container( Container C )
        {
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

        public Container()
        {

        }

        // ==================== Methods Section ======================

    } // End Class
} // End Namespace
