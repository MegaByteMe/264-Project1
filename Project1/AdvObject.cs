/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 2
 * Change: 0
 * Status: Operational Final
 * Notes:
 * Methods:
 * Dependencies: None
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    abstract class AdvObject
    {
        // ==================== Variables Section ====================
        private String name = null, desc = null, id = null;
        private int locx = 0, locy = 0;
        private bool visible = true;
        private String title = null;
        private String color = null;

        // ==================== Fields Section =======================
        public String Color
        {
            get { return color; }
            set { color = value; }
        }

        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public int LocX
        {
            get { return locx; }
            set { locx = value; }
        }

        public int LocY
        {
            get { return locy; }
            set { locy = value; }
        }

        // ==================== Methods Section ======================

    } // End Class
} // End Namespace
