using System;
using System.Text.RegularExpressions;

namespace RobotMovementTool
{
    public class Helpers
    {
        /*
         * ValidateNodeID
         * - input: string containing a node number
         *
         * Helper function used to parse node numbers.
         * Throws an ArgumentException if the string does
         * not contain a valid node number.
         */
        public int ValidateNodeID(string input)
        {
            Regex nodeRegEx = new Regex(@"[^\-0-9]");
            input = input.Trim();

            if (input.Length == 0 || nodeRegEx.Match(input).Success)
                throw new ArgumentException();

            return int.Parse(input);
        }
        
        /*
         * ValidateSingleCoord
         * - str_coord: a string of coordinates, can be a single value or comma-separated
         * - idx: integer specifying which part of a comma-separated list to return,
         *      defaults to 0
         *
         * Helper function to validate and return coordinate inputs
         */
        public double ValidateSingleCoord(string str_coord, int idx = 0)
        {
            double output;
            Regex coordRegEx = new Regex(@"[^\-0-9.]");

            string[] str_coord_split = str_coord.Split(',');

            try
            {
                str_coord_split[idx] = str_coord_split[idx].Trim();
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new ArgumentException();
            }

            if (coordRegEx.Match(str_coord_split[idx]).Success)
                throw new ArgumentException();

            if (!Double.TryParse(str_coord_split[idx], out output))
                throw new ArgumentException();

            return output;
        }
    }
}
