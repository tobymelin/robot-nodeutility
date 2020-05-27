using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RobotOM;

namespace RobotMovementTool
{
    public partial class MainWindow : Form
    {
        RobotApplication iapp;
        RobotStructure structure;
        RobotNodeServer nodes;
        RobotBarServer bars;
        RobotSelection selection;

        public MainWindow()
        {
            InitializeComponent();

            // Add version number to dialog title
            this.Text += " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

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
                    str_coord = ReturnCoordString(ValidateNodeID(coordInput.Text), comboBoxCoords.Text);
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
                str_coord = coordInput.Text.Replace(" ", "");

            for (int i = 1; i <= selection.Count; i++)
            {
                var node = (IRobotNode)nodes.Get(selection.Get(i));

                movementVector.X = node.X;
                movementVector.Y = node.Y;
                movementVector.Z = node.Z;

                try
                {
                    if (comboBoxCoords.SelectedItem.ToString() == "XYZ")
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            movementVector.Move(j.ToString(), ValidateSingleCoord(str_coord, j), radioRelative.Checked);
                        }
                    }
                    else
                        movementVector.Move(comboBoxCoords.SelectedItem.ToString(), ValidateSingleCoord(str_coord), radioRelative.Checked);
                }
                catch (ArgumentException)
                {
                    ErrorDialog("Invalid coordinates input in text box", "ERROR: Invalid Coordinates");
                    return;
                }

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
         * ValidateSingleCoord
         * - str_coord: a string of coordinates, can be a single value or comma-separated
         * - idx: integer specifying which part of a comma-separated list to return,
         *      defaults to 0
         *
         * Helper function to validate and return coordinate inputs
         */
        private double ValidateSingleCoord(string str_coord, int idx = 0)
        {
            double output;
            Regex coordRegEx = new Regex(@"[^0-9,.]");

            if (coordRegEx.Match(str_coord.Trim()).Success)
                throw new ArgumentException();

            str_coord = str_coord.Replace(" ", "");

            string[] str_coord_split = str_coord.Split(',');

            if (!Double.TryParse(str_coord_split[idx], out output))
                throw new ArgumentException();

            return output;
        }

        /*
         * ValidateNodeID
         * - input: string containing a node number
         *
         * Helper function used to parse node numbers.
         * Throws an ArgumentException if the string does
         * not contain a valid node number.
         */
        private int ValidateNodeID(string input)
        {
            Regex nodeRegEx = new Regex(@"[^0-9]");
            input = input.Trim();

            if (input.Length == 0 || nodeRegEx.Match(input).Success)
                throw new ArgumentException();

            return int.Parse(input);
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
                    nodeNumber = ValidateNodeID(coordInput.Text);
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
    }
}
