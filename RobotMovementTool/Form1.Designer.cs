namespace RobotMovementTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonMove = new System.Windows.Forms.RadioButton();
            this.radioButtonCopy = new System.Windows.Forms.RadioButton();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.coordInput = new System.Windows.Forms.TextBox();
            this.comboBoxCoords = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioRelativeNode = new System.Windows.Forms.RadioButton();
            this.radioRelative = new System.Windows.Forms.RadioButton();
            this.radioAbsolute = new System.Windows.Forms.RadioButton();
            this.buttonGetCoords = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonMove
            // 
            this.radioButtonMove.AutoSize = true;
            this.radioButtonMove.Checked = true;
            this.radioButtonMove.Location = new System.Drawing.Point(16, 23);
            this.radioButtonMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonMove.Name = "radioButtonMove";
            this.radioButtonMove.Size = new System.Drawing.Size(63, 21);
            this.radioButtonMove.TabIndex = 0;
            this.radioButtonMove.TabStop = true;
            this.radioButtonMove.Text = "Move";
            this.radioButtonMove.UseVisualStyleBackColor = true;
            // 
            // radioButtonCopy
            // 
            this.radioButtonCopy.AutoSize = true;
            this.radioButtonCopy.Location = new System.Drawing.Point(16, 52);
            this.radioButtonCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonCopy.Name = "radioButtonCopy";
            this.radioButtonCopy.Size = new System.Drawing.Size(61, 21);
            this.radioButtonCopy.TabIndex = 1;
            this.radioButtonCopy.Text = "Copy";
            this.radioButtonCopy.UseVisualStyleBackColor = true;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(140, 76);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(100, 28);
            this.buttonExecute.TabIndex = 2;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(140, 112);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 28);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // coordInput
            // 
            this.coordInput.Location = new System.Drawing.Point(7, 22);
            this.coordInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.coordInput.Name = "coordInput";
            this.coordInput.Size = new System.Drawing.Size(149, 22);
            this.coordInput.TabIndex = 0;
            // 
            // comboBoxCoords
            // 
            this.comboBoxCoords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCoords.FormattingEnabled = true;
            this.comboBoxCoords.Items.AddRange(new object[] {
            "XYZ",
            "X",
            "Y",
            "Z"});
            this.comboBoxCoords.Location = new System.Drawing.Point(164, 20);
            this.comboBoxCoords.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxCoords.Name = "comboBoxCoords";
            this.comboBoxCoords.Size = new System.Drawing.Size(64, 24);
            this.comboBoxCoords.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonMove);
            this.groupBox1.Controls.Add(this.radioButtonCopy);
            this.groupBox1.Location = new System.Drawing.Point(16, 75);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(107, 87);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Mode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioRelativeNode);
            this.groupBox2.Controls.Add(this.radioRelative);
            this.groupBox2.Controls.Add(this.radioAbsolute);
            this.groupBox2.Location = new System.Drawing.Point(16, 183);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(235, 113);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Coordinate Options";
            // 
            // radioRelativeNode
            // 
            this.radioRelativeNode.AutoSize = true;
            this.radioRelativeNode.Location = new System.Drawing.Point(15, 81);
            this.radioRelativeNode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioRelativeNode.Name = "radioRelativeNode";
            this.radioRelativeNode.Size = new System.Drawing.Size(160, 21);
            this.radioRelativeNode.TabIndex = 7;
            this.radioRelativeNode.Text = "Relative to Node No.";
            this.radioRelativeNode.UseVisualStyleBackColor = true;
            // 
            // radioRelative
            // 
            this.radioRelative.AutoSize = true;
            this.radioRelative.Location = new System.Drawing.Point(15, 52);
            this.radioRelative.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioRelative.Name = "radioRelative";
            this.radioRelative.Size = new System.Drawing.Size(160, 21);
            this.radioRelative.TabIndex = 6;
            this.radioRelative.Text = "Relative Coordinates";
            this.radioRelative.UseVisualStyleBackColor = true;
            // 
            // radioAbsolute
            // 
            this.radioAbsolute.AutoSize = true;
            this.radioAbsolute.Checked = true;
            this.radioAbsolute.Location = new System.Drawing.Point(15, 23);
            this.radioAbsolute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioAbsolute.Name = "radioAbsolute";
            this.radioAbsolute.Size = new System.Drawing.Size(164, 21);
            this.radioAbsolute.TabIndex = 5;
            this.radioAbsolute.TabStop = true;
            this.radioAbsolute.Text = "Absolute Coordinates";
            this.radioAbsolute.UseVisualStyleBackColor = true;
            // 
            // buttonGetCoords
            // 
            this.buttonGetCoords.Location = new System.Drawing.Point(140, 148);
            this.buttonGetCoords.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonGetCoords.Name = "buttonGetCoords";
            this.buttonGetCoords.Size = new System.Drawing.Size(100, 28);
            this.buttonGetCoords.TabIndex = 9;
            this.buttonGetCoords.Text = "Get Coords.";
            this.buttonGetCoords.UseVisualStyleBackColor = true;
            this.buttonGetCoords.Click += new System.EventHandler(this.ButtonGetCoords_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.coordInput);
            this.groupBox3.Controls.Add(this.comboBoxCoords);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(237, 54);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Coordinate Input";
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonExecute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(269, 314);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonGetCoords);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonExecute);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Move/Copy";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonMove;
        private System.Windows.Forms.RadioButton radioButtonCopy;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox coordInput;
        private System.Windows.Forms.ComboBox comboBoxCoords;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioRelativeNode;
        private System.Windows.Forms.RadioButton radioRelative;
        private System.Windows.Forms.RadioButton radioAbsolute;
        private System.Windows.Forms.Button buttonGetCoords;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

