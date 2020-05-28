/* Robot Node Utility
 * Copyright (C) 2020  Tobias Melin
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Windows.Forms;
using RobotOM;

namespace RobotNodeUtility
{
    public partial class MainWindow : Form
    {
        RobotApplication iapp;
        RobotStructure structure;
        RobotNodeServer nodes;
        RobotBarServer bars;
        RobotSelection selection;
        Helpers helpers = new Helpers();

        public MainWindow()
        {
            InitializeComponent();

            // Add version number to dialog title
            this.Text += " v" + Application.ProductVersion;

            // Set default selection in list box
            comboBoxCoords.SelectedIndex = 0;
        }

        // Helper function to determine if Robot has an active project open
        private bool IsRobotActive()
        {
            // Initialise connection to Robot if none exists
            if (!(iapp is RobotApplication))
                iapp = new RobotApplication();

            try
            {
                structure = iapp.Project.Structure;
                nodes = structure.Nodes;
                bars = structure.Bars;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                ErrorDialog("Robot does not appear to be running", "ERROR: Robot not running");
                return false;
            }

            if (iapp.Project.IsActive == 0)
            {
                ErrorDialog("No active Robot project was found", "ERROR: No active project");
                return false;
            }

            return true;
        }

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            if (!IsRobotActive())
                return;

            Vctr movementVector = new Vctr();
            string str_coord;
            double[] validatedCoordsList = { 0, 0, 0 };
            selection = structure.Selections.Get(IRobotObjectType.I_OT_NODE);

            if (selection.Count == 0)
            {
                ErrorDialog("No nodes have been selected", "ERROR: No selection");
                return;
            }

            // If Relative to Node No. has been selected, fetch coordinates
            // of the node referred to in the input field
            if (radioRelativeNode.Checked)
            {
                try
                {
                    str_coord = ReturnCoordString(helpers.ValidateNodeID(coordInput.Text), comboBoxCoords.Text);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ErrorDialog("Node does not exist", "ERROR: Node Not Found");
                    return;
                }
                catch (ArgumentException)
                {
                    ErrorDialog("Invalid node number input", "ERROR: Invalid Node Input");
                    return;
                }
            }
            else
            {
                str_coord = coordInput.Text.Replace(" ", "");

                // Validate coordinate inputs before any changes are made
                try
                {
                    if (comboBoxCoords.SelectedItem.ToString() == "XYZ")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            validatedCoordsList[i] = helpers.ValidateSingleCoord(str_coord, i);
                        }
                    }
                    else
                    {
                        if (str_coord.Contains(",")) {
                            ErrorDialog("Please enter a single coordinate into the input field", "ERROR: Invalid Coordinates");
                            return;
                        }
                        else
                            validatedCoordsList[0] = helpers.ValidateSingleCoord(str_coord);
                    }
                }
                catch (ArgumentException)
                {
                    ErrorDialog("Invalid coordinates input in text box. Please use a format of 'x,y,z'", "ERROR: Invalid Coordinates");
                    return;
                }
            }

            for (int i = 1; i <= selection.Count; i++)
            {
                var node = (IRobotNode)nodes.Get(selection.Get(i));

                movementVector.X = node.X;
                movementVector.Y = node.Y;
                movementVector.Z = node.Z;

                if (comboBoxCoords.SelectedItem.ToString() == "XYZ")
                {
                    for (int j = 0; j < 3; j++)
                    {
                        movementVector.Move(j, validatedCoordsList[j], radioRelative.Checked);
                    }
                }
                else
                    movementVector.Move(comboBoxCoords.SelectedItem.ToString(), validatedCoordsList[0], radioRelative.Checked);

                if (radioButtonMove.Checked)
                {
                    node.X = movementVector.X;
                    node.Y = movementVector.Y;
                    node.Z = movementVector.Z;
                }
                else
                    nodes.Create(nodes.FreeNumber, movementVector.X, movementVector.Y, movementVector.Z);
            }

            MessageBox.Show("Finished modifying " + selection.Count + " nodes.", "Action Completed");
        }

        /*
         * ReturnCoordString
         * - node_id: integer referring to a specific node
         * - axis: string specifying which coordinates to include (X, Y, Z or XYZ)
         *
         * Helper function used to return a string of the coordinates for a 
         * specific node. Throws an ArgumentOutOfRangeException if the node
         * does not exist in the Robot model.
         */
        private string ReturnCoordString(int node_id, string axis = "XYZ")
        {
            // Try to find node_id in the Robot project.
            // Throws an exception if the node does not exist.
            IRobotNode curr_node;
            string output_str;

            try
            {
                curr_node = (IRobotNode)nodes.Get(node_id);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (axis == "X")
                output_str = curr_node.X.ToString();
            else if (axis == "Y")
                output_str = curr_node.Y.ToString();
            else if (axis == "Z")
                output_str = curr_node.Z.ToString();
            else
                output_str = String.Format("{0},{1},{2}", curr_node.X.ToString(), curr_node.Y.ToString(), curr_node.Z.ToString());

            return output_str;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ErrorDialog(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonGetCoords_Click(object sender, EventArgs e)
        {
            if (!IsRobotActive())
                return;

            selection = structure.Selections.Get(IRobotObjectType.I_OT_NODE);
            int nodeNumber;
            string coordString;

            if (selection.Count == 0)
            {
                ErrorDialog("No nodes have been selected", "ERROR: No selection");
                return;
            }

            try
            {
                // If Relative to Node No. is selected, fetch coordinates for
                // the node number which has been input, otherwise return the
                // coords for the first node in the selection.
                if (radioRelativeNode.Checked)
                    nodeNumber = helpers.ValidateNodeID(coordInput.Text);
                else
                    nodeNumber = selection.Get(1);

                coordString = ReturnCoordString(nodeNumber);
            }
            catch (ArgumentOutOfRangeException)
            {
                ErrorDialog("Node does not exist", "ERROR: Node Not Found");
                return;
            }
            catch (ArgumentException)
            {
                ErrorDialog("Invalid node number input", "ERROR: Invalid Node Input");
                return;
            }

            MessageBox.Show(coordString);
        }
    }

    // Helper class Vctr, 
    public class Vctr
    {
        public double X, Y, Z;

        public Vctr()
        {
            X = Y = Z = 0.0;
        }

        public Vctr(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public void Move(string axis, double dist, bool relative = true)
        {
            if (relative)
            {
                if (axis == "X" || axis == "0")
                    X += dist;
                else if (axis == "Y" || axis == "1")
                    Y += dist;
                else if (axis == "Z" || axis == "2")
                    Z += dist;
            }
            else {
                if (axis == "X" || axis == "0")
                    X = dist;
                else if (axis == "Y" || axis == "1")
                    Y = dist;
                else if (axis == "Z" || axis == "2")
                    Z = dist;
            }
        }

        public void Move(int axis, double dist, bool relative = true)
        {
            Move(axis.ToString(), dist, relative);
        }

        public override String ToString()
        {
            return String.Format("{0},{1},{2}", X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            Vctr vector = obj as Vctr;
            return (vector != null)
            && Equals(vector.X, vector.Y, vector.Z);
        }

        /*
         * Implement Equals with an allowable tolerance of 
         * 0.001% to allow for slight inaccuracies due to
         * limitations with precision.
         */
        public bool Equals(double cX, double cY, double cZ)
        {
            double tolerance = .001 / 100;
            bool checkX, checkY, checkZ;

            checkX = Math.Abs(X - cX) <= tolerance;
            checkY = Math.Abs(Y - cY) <= tolerance;
            checkZ = Math.Abs(Z - cZ) <= tolerance;

            return checkX && checkY && checkZ;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(X, Y, Z).GetHashCode();
        }
    }
}
