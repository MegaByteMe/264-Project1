/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 0
 * Change: 1
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
    class Monster : LivingObject
    {
        // ==================== Variables Section ====================
        private bool chained = true;

        // ==================== Fields Section =======================
        public bool Chained
        {
            get { return chained; }
            set { chained = value; }
        }

        // ==================== Constructors =========================
        public Monster( Monster C )
        {
            this.Chained = C.Chained;

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

        public Monster()
        {

        }

        // ==================== Methods Section ======================
    } // End Class
} // End Namespace
