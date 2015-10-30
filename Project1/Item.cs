/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 1
 * Change: 3
 * Status: Following Functionality
 * Notes:
 * Methods:
 * Dependencies:    AdvObject.cs
 *                  Item.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Item : AdvObject
    {
        // ==================== Variables Section ====================
        private bool baggable = true, broken = false, clue = false, keyitem = false, generator = false;
        private Item opens = null;
        private int dmgmul = 1, range = 1;

        // ==================== Fields Section =======================
        #region FIELDS
        public bool Baggable
        {
            get { return baggable; }
            set { baggable = value; }
        }

        public bool Broken
        {
            get { return broken; }
            set { broken = value; }
        }

        public bool Clue
        {
            get { return clue; }
            set { clue = value; }
        }

        public bool KeyItem
        {
            get { return keyitem; }
            set { keyitem = value; }
        }

        public bool Generator
        {
            get { return generator; }
            set { generator = value; }
        }

        public Item Opens
        {
            get { return opens; }
            set { opens = value; }
        }

        public int DmgMul
        {
            get { return dmgmul; }
            set { dmgmul = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        #endregion

        // ==================== Constructors ======================
        public Item( Item C )
        {
            this.Range = C.Range;
            this.DmgMul = C.DmgMul;
            this.Opens = C.Opens;
            this.Generator = C.Generator;
            this.Visible = C.Visible;
            this.KeyItem = C.KeyItem;
            this.Clue = C.Clue;
            this.Broken = C.Broken;
            this.Baggable = C.Baggable;

            this.Name = C.Name;
            this.Id = C.Id;
            this.Desc = C.Desc;
        }

        public Item(bool vis, bool key, bool clue, String name, String des, int lx, int ly, int dmgm, int rge)
        {
            Visible = vis;
            KeyItem = key;
            Clue = clue;
            Name = name;
            Desc = des;
            DmgMul = dmgm;
            Range = rge;
            LocX = lx;
            LocY = ly;
        }

        public Item(bool vis, bool key, bool clue, String name, String des)
        {
            Visible = vis;
            KeyItem = key;
            Clue = clue;
            Name = name;
            Desc = des;
        }

        public Item()
        {
        }

        // ==================== Methods Section ======================


    } // End Class
} // End Namespace
