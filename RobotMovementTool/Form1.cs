using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using RobotOM;

namespace RobotMovementTool
{
    public partial class Form1 : Form
    {
        RobotApplication iapp;
        RobotStructure structure;
        RobotNodeServer nodes;
        RobotBarServer bars;
        RobotSelection selection;

        public Form1()
        {
            InitializeComponent();

            // Add version number to dialog title
            this.Text += " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // Set default selection in list box
            comboBoxCoords.SelectedIndex = 0;
        }

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            iapp = new RobotApplication();
            
            if (iapp.Project.IsActive == 0)
                return;

            Vctr movementVector = new Vctr();
            var str_coord = "";

            structure = iapp.Project.Structure;
            nodes = structure.Nodes;
            bars = structure.Bars;
            selection = structure.Selections.Get(IRobotObjectType.I_OT_NODE);

            if (radioRelativeNode.Checked)
            {
                try
                {
                    str_coord = ReturnCoordString(ValidateNodeID(coordInput.Text), comboBoxCoords.Text);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ErrorDialog("Invalid node number input", "ERROR: Node Not Found");
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

                if (comboBoxCoords.SelectedItem.ToString() == "XYZ")
                {
                    for (int j = 0; j < 3; j++)
                    {
                        movementVector.Move(j.ToString(), ValidateSingleCoord(str_coord, j), radioRelative.Checked);
                    }
                }
                else
                    movementVector.Move(comboBoxCoords.SelectedItem.ToString(), ValidateSingleCoord(str_coord), radioRelative.Checked);

                if (radioButtonMove.Checked)
                {
                    node.X = movementVector.X;
                    node.Y = movementVector.Y;
                    node.Z = movementVector.Z;
                }
                else
                    nodes.Create(nodes.FreeNumber, movementVector.X, movementVector.Y, movementVector.Z);
            }
        }

        private double ValidateSingleCoord(string str_coord, int idx = 0)
        {
            double output;
            Regex chr = new Regex(@"[a-zA-Z]");

            if (chr.Match(str_coord).Success)
                throw new ArgumentOutOfRangeException();

            str_coord = str_coord.Replace(" ", "");

            string[] str_firstpart = str_coord.Split(',');

            if (!Double.TryParse(str_firstpart[idx], out output))
                throw new ArgumentOutOfRangeException();

            return output;
        }

        private int ValidateNodeID(string input)
        {
            Regex chr = new Regex(@"[a-zA-Z]");

            if (chr.Match(input).Success)
                throw new ArgumentOutOfRangeException();

            string[] str_split = input.Split(',');
            return int.Parse(str_split[0]);
        }

        private string ReturnCoordString(int node_id, string axis = "XYZ")
        {
            IRobotNode curr_node;
            try
            {
                curr_node = (IRobotNode)nodes.Get(node_id);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw new ArgumentOutOfRangeException();
            }
            string output_str = "";

            if (axis == "X")
                output_str = curr_node.X.ToString();
            else if (axis == "Y")
                output_str = curr_node.Y.ToString();
            else if (axis == "Z")
                output_str = curr_node.Z.ToString();
            else
            {
                output_str += curr_node.X.ToString();
                output_str += "," + curr_node.Y.ToString();
                output_str += "," + curr_node.Z.ToString();
            }

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
            iapp = new RobotApplication();

            if (iapp.Project.IsActive == 0)
                return;

            structure = iapp.Project.Structure;
            nodes = structure.Nodes;
            bars = structure.Bars;
            selection = structure.Selections.Get(IRobotObjectType.I_OT_NODE);


            int nodeNumber;

            if (radioRelativeNode.Checked)
                nodeNumber = ValidateNodeID(coordInput.Text);
            else
                nodeNumber = selection.Get(1);

            IRobotNode node;


            try
            {
                node = (IRobotNode)nodes.Get(nodeNumber);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                ErrorDialog("Invalid node number input", "ERROR: Node Not Found");
                return;
            }

            string coordString = node.X.ToString() + ", " + node.Y.ToString() + ", " + node.Z.ToString();

            MessageBox.Show(coordString);
        }
    }

    // Helper class Vctr
    public class Vctr
    {
        public double X, Y, Z;

        public Vctr()
        {
            X = Y = Z = 0.0;
        }

        public Vctr(double X, double Y, double Z)
        {
            X = this.X; Y = this.Y; Z = this.Z;
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
