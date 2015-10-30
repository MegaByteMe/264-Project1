/* Name: MR-GroupQ
 * Course: ECE264
 * Date: 27-Feb-2015
 * Assignment: Project 1 Adventure Game
 * Revision: 4
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
using System.IO;
using System.Xml;                   // Needed for XML use
using System.Xml.Linq;              // Needed for XML XElement calls

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==================== Variables Section ====================
            String[] sep = new String[] { "\r\n" };
            String done = null, temp = null;
            int store = 0;
            int XPSTART = 3, YPSTART = 6, t1x = 12, t1y = 60;

            // ==================== Objects ====================
            Validator Val = new Validator();
            Engine game = new Engine(XPSTART, YPSTART);
            XDocument UI = XDocument.Load("uitext.xml");       // Open Map Attributes To Assist In Tile Creation

            String[] commands = UI.Element("interface").Element("help").Element("comlist").FirstNode.ToString().ToUpper().Split(sep,StringSplitOptions.RemoveEmptyEntries);
            String[] arg = UI.Element("interface").Element("help").Element("moveargs").FirstNode.ToString().ToUpper().Split(sep, StringSplitOptions.RemoveEmptyEntries);

            game.Init();

            Console.Write( UI.Element("interface").Element("messages").Element("pname").FirstNode.ToString() );
            game.FindHero().Name = Console.ReadLine();
            Console.WriteLine("Welcome {0}! Let your adventure begin...", game.FindHero().Name );

            #region Run Loop
            while (done != "QUIT")
            {
                done = Val.Check(": ", "Please enter a valid command.", commands);

                switch (done)
                {
                    case "MOVE":
                    case "M":
                        temp = Val.Check("Direction?: ", "Please enter a valid direction!", arg);
                        game.MoveP(temp);
                        game.DrawZoom();
                        Console.WriteLine("Location: " + game.PLocX + ", " + game.PLocY);
                        break;
                    case "MAP":
                        game.DrawMap(0,0,0,0, "");
                        break;
                    case "ZOOM":
                        game.DrawZoom();
                        break;
                    case "LOOK":
                    case "L":
                        game.Look(game.FindHero());
                        break;
                    case "HELP":
                    case "H":
                        foreach (String ee in commands)
                        {
                            if (ee.Length > 2)
                            {
                                Console.Write((String)UI.Element("interface").Element("commands").Element(ee).Element("name").FirstNode.ToString() + " - ");
                                Console.WriteLine((String)UI.Element("interface").Element("commands").Element(ee).Element("description").FirstNode.ToString());
                            }
                        }
                        break;
                    case "INSPECT":
                    case "I":
                //        Console.Write("Which item?: ");
                //        temp = Console.ReadLine();
                //        game.InsItem(temp);
                        break;
                    case "TAKE":
                    case "T":
                        game.Look(game.FindHero());
                        Console.Write("Which item?: ");
                        temp = Console.ReadLine();
                        store = game.FindItem(temp);
                        if (store < 0)
                            Console.WriteLine("I cant find " + temp + ". Maybe it's misspelled.");
                        else
                            game.GetItem(temp);
                        break;
                    case "BAG":
                        game.FindHero().ShowBag();
                        break;
                    case "TELEPORT":
                        t1x = Val.Check("x coord: ", "WTF?", 64);
                        t1y = Val.Check("y coord: ", "WTF?", 64);
                        game.MoveTele(t1x, t1y);
                        break;
                    case "LHOLD":
                        game.DspBag(game.FindHero());
                        Console.Write("Which item?: ");
                        temp = Console.ReadLine();
                        game.PutPLhand(temp);
                        break;
                    case "RHOLD":
                        game.DspBag(game.FindHero());
                        Console.Write("Which item?: ");
                        temp = Console.ReadLine();
                        game.PutPRhand(temp);
                        break;
                    case "HOLDING":
                        game.FindHero().DspHolding();
                        break;
                    case "LBAG":
                        game.FindHero().LHandToBag();
                        break;
                    case "RBAG":
                        game.FindHero().RHandToBag();
                        break;
                    case "LEGEND":

                        break;
                }
            } // End While
            #endregion

        } // End Main

    } // End Class
} // End Namespace
