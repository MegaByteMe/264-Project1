/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 3
 * Change: 6
 * Status: Operational - Light Bugs - Expanding Functionality
 * Notes:            
 *          Look into launching thread processes to allow random npc movement and one thread for random npc chatter.
 *              Porbably need mutex's for world map.
 *          Work on battle mechanics.
 *          
 * Methods:         
 *          // ==================== Constructors ======================
            public Engine(int playxstart, int playystart)
 * 
 *          // =============== Public Methods Section =================
 *          public void Init()
 *          public void MoveP(String direction)
 *          public void MoveTele(int x, int y)          !!!! Testing only !!!! This is not a release feature
 *          public void DrawMap(int xstar, int ystar, int xend, int yend, String space)
 *          public void DrawZoom()
 *          public void ChkBox(out int xstar, out int ystar, out int xend, out int yend, int range)
 *          public void Look(LivingObject me)
 *          public void DspHBag()
 *          public void FindItem(String s)
 *          public void InsItem(String s)
 *          public void GenItem(bool vis, bool key, bool clue, String name, String desc, int lx, int ly, int dmgm, int range)
 *          public Humanoid FindHero()
 *          public String GetnpcNat(int x, int y)
 *                  
 *          // ============== Private Methods Section =================
 *          private Tile NewTile(XDocument X, String type, int x, int y)
 *          private Humanoid NewTownee(int x, int y)
 *          private String Randy(String top, String type, String which)
 *          private bool ChkPMove(int x, int y)
 *          private void GenPLayer()
 *          private void PrintBag(Item a)
 *          private void SynPLoc(int x, int y)
 *          private void Colorize(int x, int y)
 *          private Monster NewMon(int x, int y)
 *          
 * Dependencies:
 *              AdvObject.cs
 *              LivingObject.cs
 *              Container.cs
 *              Tile.cs
 *              Item.cs
 *              Monster.cs
 *              Humanoid.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;                   // Needed for XML
using System.Xml.Linq;              // Needed for XDocument

namespace Project1
{
    class Engine
    {
        // ==================== Variables Section ====================
        private int MAXSX = 0, MAXSY = 0;      // Define size of map file
        private int XPSTART = 0, YPSTART = 0;
        private int plocx, plocy;                      // Player location
        public Tile[,] world;
        private Item temp;
        private Random rand = new Random();

        // ==================== Fields Section =======================
        public int PLocX
        {
            get { return plocx; }
            set { plocx = value; }
        }

        public int PLocY
        {
            get { return plocy; }
            set { plocy = value; }
        }

        // ==================== Constructors ======================
        public Engine(int playxstart, int playystart)
        {
            XPSTART = playxstart;
            YPSTART = playystart;
        }

        // =============== Public Methods Section =================
        public void Init()                      // Setup the world environment, player, npcs, and monsters
        {
            int i = 0, j = 0;
            String titype = null;

            #region Open Support Files
            // Open Map File Begin Instantiating World
            var reader = new StreamReader(File.OpenRead("map.txt"));
            
            // Open Map Attributes To Assist In Tile Creation
            XDocument mapAT = XDocument.Load("mapattribute.xml");

            // Open storyline file
            XDocument story = XDocument.Load("storytext.xml");
            #endregion

            int.TryParse(mapAT.Element("maptranslator").Element("maxsize").Element("x").FirstNode.ToString(), out MAXSX);
            int.TryParse(mapAT.Element("maptranslator").Element("maxsize").Element("y").FirstNode.ToString(), out MAXSY);

            world = new Tile[MAXSX, MAXSY];

            #region Generate World
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                foreach (String ele in values)
                {
                    switch (ele) 
                    {
                        case "B":           // Generate Building Tile
                            titype = "Building";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "BT":           // Generate Building Tile and townee
                            titype = "Building";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewTownee(i, j);
                            break;
                        case "D":           // Generate Desert Tile
                            titype = "Desert";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "DM":           // Generate Desert Tile
                            titype = "Desert";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            break;
                        case "F":           // Generate Forest Tile
                            titype = "Forest";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "FM":           // Generate Forest Tile
                            titype = "Forest";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            break;
                        case "C":           // Generate Cave Tile
                            titype = "Cave";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            world[i, j].PassReq = true;
                            world[i, j].PassPair = new Item(true, true, true, "Torch", "This item may be useful in an area where darkness makes it impassable.");
                            break;
                        case "CM":           // Generate Cave Tile
                            titype = "Cave";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            world[i, j].PassReq = true;
                            world[i, j].PassPair = new Item(true, true, true, "Torch", "This item may be useful in an area where darkness makes it impassable.");
                            break;
                        case "H":           // Generate Castle Tile
                            titype = "Castle";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "HM":           // Generate Castle Tile
                            titype = "Castle";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            break;
                        case "S":           // Generate Marsh Tile
                            titype = "Swamp";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "SM":           // Generate Marsh Tile
                            titype = "Swamp";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            break;
                        case "Y":           // Generate Courtyard Tile
                            titype = "Courtyard";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "YT":           // Generate Courtyard Tile
                            titype = "Courtyard";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewTownee(i, j);
                            break;
                        case "P":           // Generate Plains Tile
                            titype = "Plains";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "PM":           // Generate Plains Tile
                            titype = "Plains";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            break;
                        case "O":           // Generate Ocean Tile
                            titype = "Ocean";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Passable = false;
                            break;
                        case "G":           // Generate Graveyard Tile
                            titype = "Graveyard";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "GM":           // Generate Graveyard Tile
                            titype = "Graveyard";
                            world[i, j] = NewTile(mapAT, titype, i, j);
                            world[i, j].Occupied = NewMon(i, j);
                            break;
                        case "E":           // Generate Entryway Tile
                            titype = "Entry";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "W":           // Generate Wall Tile
                            titype = "Wall";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            world[i,j].Passable = false;
                            break;
                        case "M":           // Generate Mountain Tile
                            titype = "Mountain";
                            world[i,j] = NewTile(mapAT, titype, i, j);
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Error Parsing Map File!");
                            break;
                    }
                    if (i < MAXSX - 1)
                        i++;
                    else { 
                        i = 0;
                        j++;
                    } // End Else
                } // End Foreach
            } // End While
            #endregion

            GenPLayer();
            GenItem(true, false, false, "Hammer", "It's Hammer Time!", PLocX + 1, PLocY, 4, 1);
            GenItem(true, true, true, "Torch", "This item may be useful in an area where darkness makes it impassable.", PLocX, PLocY + 1, 2, 2);

            String opener = story.Element("story").Element("intronote").FirstNode.ToString();
            Console.WriteLine(story.Element("story").Element("intro").FirstNode.ToString() + "\n" + opener + "\n"
                                                + story.Element("story").Element("intro2").FirstNode.ToString());

            FindHero().Carrying.Holds.Add(new Item(true, false, true, "Dusty Note", opener, PLocX, PLocY, 1, 1));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(FindHero().Name + " received Dusty Note");
            Console.ResetColor();

            }

        public void Leg()
        {

        }
        
        public void MoveP(String direction)
        {
            switch (direction)
            {
                case "NORTH":
                case "N":               //move north
                    if (ChkMScope(PLocX, PLocY - 1) && FindHero().ChkTileMove(world[PLocX, PLocY - 1]))
                    {
                        world[PLocX, PLocY].Occupied.MoveLO(world, PLocX, PLocY - 1);
                        SynPLoc(PLocX, PLocY - 1);
                    }
                    else if (world[PLocX, PLocY - 1].Occupied != null)
                    {
                        Console.WriteLine("I can't walk through {0}. I should walk around.", NPCNaT(PLocX, PLocY - 1));
                    }
                    else
                    {
                        if (world[PLocX, PLocY - 1].PassReq == true)
                            Console.WriteLine("I can't pass walk through {0} Perhaps there is an item that will assist me here", world[PLocX, PLocY - 1].Name);
                        else
                            Console.WriteLine("I can't walk through {0}. Perhaps if I find an entrance.", world[PLocX, PLocY - 1].Name);
                    }
                    break;
                case "SOUTH":
                case "S":               //move south
                    if (ChkMScope(PLocX, PLocY + 1) && FindHero().ChkTileMove(world[PLocX, PLocY + 1]))
                    {
                        world[PLocX, PLocY].Occupied.MoveLO(world, PLocX, PLocY + 1);
                        SynPLoc(PLocX, PLocY + 1);
                    }
                    else if (world[PLocX, PLocY + 1].Occupied != null)
                    {
                        Console.WriteLine("I can't walk through {0}. I should walk around.", NPCNaT(PLocX, PLocY + 1));
                    }
                    else
                    {
                        if (world[PLocX, PLocY - 1].PassReq == true)
                            Console.WriteLine("I can't pass walk through {0} Perhaps there is an item that will assist me here", world[PLocX, PLocY + 1].Name);
                        else
                            Console.WriteLine("I can't walk through {0}. Perhaps if I find an entrance.", world[PLocX, PLocY + 1].Name);
                    }
                    break;
                case "EAST":
                case "E":               //move east
                    if (ChkMScope(PLocX + 1, PLocY) && FindHero().ChkTileMove(world[PLocX + 1, PLocY]))
                    {
                        world[PLocX, PLocY].Occupied.MoveLO(world, PLocX + 1, PLocY);
                        SynPLoc(PLocX + 1, PLocY);
                    }
                    else if (world[PLocX + 1, PLocY].Occupied != null)
                    {
                        Console.WriteLine("I can't walk through {0}. I should walk around.", NPCNaT(PLocX + 1, PLocY));
                    }
                    else
                    {
                        if (world[PLocX, PLocY - 1].PassReq == true)
                            Console.WriteLine("I can't pass walk through {0} Perhaps there is an item that will assist me here", world[PLocX + 1, PLocY].Name);
                        else
                            Console.WriteLine("I can't walk through {0}. Perhaps if I find an entrance.", world[PLocX + 1, PLocY].Name);
                    }
                    break;
                case "WEST":
                case "W":               //move west
                    if (ChkMScope(PLocX - 1, PLocY - 1) && FindHero().ChkTileMove(world[PLocX - 1, PLocY]))
                    {
                        world[PLocX, PLocY].Occupied.MoveLO(world, PLocX - 1, PLocY);
                        SynPLoc(PLocX - 1, PLocY);
                    }
                    else if (world[PLocX - 1, PLocY].Occupied != null)
                    {
                        Console.WriteLine("I can't walk through {0}. I should walk around.", NPCNaT(PLocX - 1, PLocY));
                    }
                    else
                    {
                        if (world[PLocX, PLocY - 1].PassReq == true)
                            Console.WriteLine("I can't pass walk through {0} Perhaps there is an item that will assist me here", world[PLocX - 1, PLocY].Name);
                        else
                            Console.WriteLine("I can't walk through {0}. Perhaps if I find an entrance.", world[PLocX - 1, PLocY].Name);
                    }
                    break;
                default:
                    Console.WriteLine("This is embarassing. Move Error");
                    break;
            }
        }

        public void MoveTele(int x, int y)      // This is a testing method or possibly a "bonus" of a key item but not release feature
        {
            FindHero().MoveLO(world, x, y);
            SynPLoc(x,y);
        }

        public void DrawMap(int xstar, int ystar, int xend, int yend, String space)
        {
            if (xstar == 0 && ystar == 0 && xend == 0 && yend == 0)
            {
                xend = MAXSX - 1;
                yend = MAXSY - 1;
            }

            for (int j = ystar; j <= yend; j++)
            {
                Console.WriteLine();
                for (int i = xstar; i <= xend; i++)
                {
                    if (i == PLocX && j == PLocY && space == " ")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(space + "P" + space);
                    }
                    else if (i == PLocX && j == PLocY && space == "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(space + "P" + space);
                    }
                    else
                    {
                        if (world[i, j].Occupied != null && world[i,j].Occupied.Alive == true)
                        {
                            Console.ResetColor();
                            Console.Write(space + world[i, j].Occupied.Id + space);
                        }
                        else
                        {
                            Colorize(i, j);
                            Console.Write(space + world[i, j].Id + space);
                        }
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DrawZoom()                  // Uses DrawMap
        {
            int a = 0, b = 0, c = 0, d = 0;

            ChkBox(out a, out b, out c, out d, FindHero().SightRange);
            DrawMap(a,b,c,d, " ");
        }

        public void ChkBox(out int xstar, out int ystar, out int xend, out int yend, int range)     //Creates bounding box
        {
            if (PLocX + range > MAXSX)  // Xend
                xend = MAXSX - 1;
            else
                xend = PLocX + range;

            if (PLocX - range < 0)          // Xstart
                xstar = 0;
            else
                xstar = PLocX - range;

            if (PLocY + range > MAXSY)  //Yend
                yend = MAXSY - 1;
            else
                yend = PLocY + range;

            if (PLocY - range < 0)          //Ystart
                ystar = 0;
            else
                ystar = PLocY - range;
        }

        public void Look(LivingObject me)       // Wrapper for object look function
        {
            int a = 0, b = 0, c = 0, d = 0;
            me.AdjBox(out a, out b, out c, out d, MAXSX, MAXSY, me.SightRange);
            me.Look(world, a, b, c, d);
        }

        public void DspBag(Humanoid me)         // Wrapper for Humanoid ShowBag() - More for NPC's
        {
            me.ShowBag();
        }

        public int FindItem(String s)           // Return location of matching item
        {
            int a = 0, b = 0, c = 0, d = 0, w = 0;
            ChkBox(out a, out b, out c, out d, FindHero().SightRange);

            for (int j = b; j <= d; j++)
            {
                for (int i = a; i <= c; i++)
                {
                    if (world[i, j].Holds != null)
                    {
                        foreach (Item it in world[i,j].Holds)
                        {
                            if (it.Name == s)
                            {
                                return w;
                            }
                            w++;
                        }   
                    }
                }
            } // End for
            return -1;
        }

        public void InsItem(String s)           // Inspect an item - This is broken!
        {
            int a = 0, b = 0, c = 0, d = 0;
            ChkBox(out a, out b, out c, out d, FindHero().SightRange);

        }

        public void GenItem(bool vis, bool key, bool clue, String name, String desc, int lx, int ly, int dmgm, int range)
        {
            world[lx, ly].Holds.Add(new Item( vis, key, clue, name, desc, lx, ly, dmgm, range));
        }

        public Humanoid FindHero()              // Returns the hero to quickly access heros methods and data
        {
            return (Humanoid)world[PLocX, PLocY].Occupied;
        }

        public String NPCNaT(int x, int y)      // Returns the string value of an NPC's NaT - Name and Title
        {
            return world[x, y].Occupied.Name + " The " + world[x, y].Occupied.Title;
        }

        public void DspHolding(LivingObject me)     // Display what passed character is holding this is more important to NPC's
        {
            me.DspHolding();
        }

        public void PutPLhand(String s)         // Takes an item from players bag and places it in their hand
        {
            int i = 0, g = 0;

            if (FindHero().LHand == null && FindHero().Carrying.Holds.Count > 0 )
            {
                    foreach (Item it in FindHero().Carrying.Holds)
                    {
                        if (it.Name == s)
                        {
                            FindHero().LHand = it;
                            g = i;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(FindHero().Name + " is holding " + s + " in left hand.");
                            Console.ResetColor();
                        }
                        i++;
                    }
                    FindHero().Carrying.Holds.RemoveAt(g);
            }
            else if( FindHero().LHand == null && FindHero().Carrying.Holds.Count == 0 )
                Console.WriteLine("The bag is empty! You want to hold nothing?!");
            else
                Console.WriteLine(FindHero().Name + " cannot find " + s + ". Either it doesn't exist or it was misspelled.");
        }

        public void PutPRhand(String s)         // Takes an item from players bag and places it in their hand
        {
            int i = 0, g = 0;

            if (FindHero().RHand == null && FindHero().Carrying.Holds.Count > 0 )
            {
                    foreach (Item it in FindHero().Carrying.Holds)
                    {
                        if (it.Name == s)
                        {
                            FindHero().RHand = it;
                            g = i;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(FindHero().Name + " is holding " + s + " in right hand.");
                            Console.ResetColor();
                        }
                        i++;
                    }
                    FindHero().Carrying.Holds.RemoveAt(g);
            }
            else if( FindHero().RHand == null && FindHero().Carrying.Holds.Count == 0 )
                Console.WriteLine("The bag is empty! You want to hold nothing?!");
            else
                Console.WriteLine(FindHero().Name + " cannot find " + s + ". Either it doesn't exist or it was misspelled.");
        }

        public void GetItem(String s)           // Wrapper for GetItem in humanoid
        {
            int a = 0, b = 0, c = 0, d = 0;
            FindHero().AdjBox(out a, out b, out c, out d, MAXSX, MAXSY, FindHero().SightRange);
            FindHero().GetItem(world, a, b, c, d, s);
        }

        public bool ChkMScope(int x, int y)     // Make sure movement does not out of scope world
        {
            if ((x > 0) && (y > 0) && (x < MAXSX - 1) && (y < MAXSY - 1))
                return true;
            else
                return false;
        }

        // ============== Private Methods Section =================
        private Tile NewTile(XDocument X, String type, int x, int y)    // Generate a new tile for map init
        {
            Tile T = new Tile();
            T.Name = (String)X.Element("maptranslator").Element(type).Element("Name");
            T.Desc = (String)X.Element("maptranslator").Element(type).Element("Description");
            T.Id = (String)X.Element("maptranslator").Element(type).Element("ID");
            T.Color = (String)X.Element("maptranslator").Element(type).Element("Color");
            T.LocX = x;
            T.LocY = y;
            return T;
        }

        private Humanoid NewTownee(int x, int y)                        // Generate a new NPC - human type
        {
            Humanoid H = new Humanoid();
            H.Name = Randy("townees", "names", "name");
            H.Title = Randy("townees", "titles", "human");
            H.LocX = x;
            H.LocY = y;
            H.Id = "T";

            if (H.Title == "Drunk")
                H.Drunk = true;

            return H;
        }

        private String Randy(String top, String type, String which)     // Generate random info for NPC's
        {
            XDocument NPC = XDocument.Load("npcinits.xml");
            String[] sep = new String[] { "\r\n" };

            if (which.ToUpper() == "NAME")
            {
                String temp = NPC.Element(top).Element(type).Element("firstname").FirstNode.ToString();
                String[] rar = temp.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                temp = NPC.Element(top).Element(type).Element("lastname").FirstNode.ToString();
                String[] raw = temp.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                temp = rar[rand.Next(0, rar.Count())] + " " + raw[rand.Next(0, raw.Count())];

                return temp;
            }
            else
            {   
                String temp = NPC.Element(top).Element(type).Element(which).FirstNode.ToString();
                String[] rar = temp.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                temp = rar[rand.Next(1, rar.Count())];

                return temp;
            }
        }

        private void GenPLayer()                // Generate the player on the map
        {
            world[XPSTART, YPSTART].Occupied = new Humanoid("Adventurer", "Hero", "Player", XPSTART, YPSTART);
            PLocX = XPSTART;
            PLocY = YPSTART;
        }

        private void SynPLoc(int x, int y)      // Synchronize player location
        {
            PLocX = world[x, y].Occupied.LocX;
            PLocY = world[x, y].Occupied.LocY;
        }

        private void Colorize(int x, int y)     // Parse tile color field to colorize map
        {
            switch (world[x, y].Color)
            {
                case "Yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "DarkGray":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "DarkYellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "DarkMagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "White":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "DarkBlue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "Brown":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "DarkRed":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }
                
        }

        private Monster NewMon(int x, int y)    // Generate new monster
        {
            Monster M = new Monster();
            M.Name = Randy("townees", "names", "named");
            M.Title = Randy("townees", "titles", "mon");
            M.LocX = x;
            M.LocY = y;
            M.Chained = true;
            M.Aggressive = true;
            M.Id = "M";

            return M;
        }

        private void FindBag(Item a)            // Needs work
        {
            Console.Write(a.Name + a.Desc + ", ");
        }

    } // End Class
} // End Namespace
