/* Name: MR
 * Course: ECE264
 * Date: 12-Feb-2015
 * Revision: 1
 * Change: 3D
 * Notes: Code Diverge! -> ComplexNUM.TryParse() removed - Unnecessary for current implementation
 * Methods: public String Check(String Input_Message, String Error_Message, String[] Required_Value)
 *          public int Check(String Input_Message, String Error_Message, Int Required_Value)
 *          public int Check(String Input_Message, String Error_Message, String Required_Value)
 *          public bool Check(String Input_Message, String Error_Message)
 *          public double Check(String Input_Message, String Error_Message)
 * Dependencies:    None
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project1 {
    class Validator {
        // ==================== Variables Section ====================
        private String eee;
        private int go = 0;
        private double gone = 0;

        // ==================== Methods Section ====================
        // Compares an array of supplied strings to user input, if user input matches one item in the list, return the match.
        public String Check(String innMsg, String errMsg, String[] valCon) {

            Console.Write("{0}", innMsg);
            eee = Console.ReadLine().ToUpper();

            foreach (String ele in valCon)
                if (ele == eee)
                    return eee;

            Console.WriteLine("{0}", errMsg);
            return Check(innMsg, errMsg, valCon);
        } // End Method

        //--------|| Passing int valCon value of 0 just ensures input is a number
        //--------|| Passing int valCon value of int ensures input in int and less than valCon but greater then 0
        public int Check(String innMsg, String errMsg, int valCon) {

            Console.Write("{0}", innMsg);
            eee = Console.ReadLine();

            if (int.TryParse(eee, out go)) {
                if ((go <= valCon) && (go > 0))
                    return go;
                else if ((valCon == 0) && (go > 0))
                    return go;
            }

            Console.WriteLine("{0}", errMsg);
            return Check(innMsg, errMsg, valCon);
        } // End Method

        //--------|| Prints index in output for dealing with arrays
        //--------|| Passing int valCon value of 0 just ensures input is a number
        //--------|| Passing int valCon value of int ensures input in int and less than valCon but greater then 0
        public int Check(String innMsg, String errMsg, int valCon, int inde)
        {

            Console.Write("{0}" + "{1}: ", innMsg, inde);
            eee = Console.ReadLine();

            if (int.TryParse(eee, out go))
            {
                if ((go <= valCon) && (go > 0))
                    return go;
                else if ((valCon == 0) && (go > 0))
                    return go;
            }

            Console.WriteLine("{0}", errMsg);
            return Check(innMsg, errMsg, valCon, inde);
        } // End Method

        // Compares user input to retTrue, if a match is found return true.
        //                     "  retFalse, if a match is found return false.
        public bool Check(String innMsg, String errMsg, String[] retTrue, String[] retFalse) {

            Console.Write("{0}", innMsg);
            eee = Console.ReadLine().ToUpper();

            foreach( String ele in retTrue ) {
                if (ele == eee)
                    return true;
            };

            foreach (String ele in retFalse) {
                if (ele == eee)
                    return false;
            };

            Console.WriteLine("{0}", errMsg);
            return Check(innMsg, errMsg, retTrue, retFalse);
        } // End Method

        public int Check(String innMsg, String errMsg, String valCon) {

            Console.Write("{0}" + "{1}" + ":", innMsg, valCon);
            eee = Console.ReadLine();

            if (int.TryParse(eee, out go))
                return go;

            Console.WriteLine("{0}", errMsg);
            return Check(innMsg, errMsg, valCon);
        } // End Method

        public double Check(String innMsg, String errMsg) {
            Console.Write("{0}", innMsg);
            eee = Console.ReadLine();

            if (double.TryParse(eee, out gone))
                return gone;

            Console.WriteLine("{0}", errMsg);
            return Check(innMsg, errMsg);
        } // End Method		
    } // End Class
} // End Namespace
