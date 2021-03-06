﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Factory_Inventory
{
    partial class editQuality
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addQualityButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteUserCheckboxCK = new System.Windows.Forms.CheckBox();
            this.editedQualityTextboxTB = new System.Windows.Forms.TextBox();
            this.newQualityTextboxTB = new System.Windows.Forms.TextBox();
            this.newAccessLevelLabel = new System.Windows.Forms.Label();
            this.newConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.userLabel = new System.Windows.Forms.Label();
            this.addHSNNoTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.editHSNNoTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.hsComboBox3 = new HatchStyleComboBox.HSComboBox();
            this.hsComboBox2 = new HatchStyleComboBox.HSComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.editQBTCB = new System.Windows.Forms.ComboBox();
            this.addQBTCB = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(230, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(570, 700);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 615);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Quality before Twist";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Quality before Twist";
            // 
            // addQualityButton
            // 
            this.addQualityButton.Location = new System.Drawing.Point(11, 663);
            this.addQualityButton.Name = "addQualityButton";
            this.addQualityButton.Size = new System.Drawing.Size(75, 28);
            this.addQualityButton.TabIndex = 21;
            this.addQualityButton.Text = "Add";
            this.addQualityButton.UseVisualStyleBackColor = true;
            this.addQualityButton.Click += new System.EventHandler(this.addQualityButton_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Quality";
            // 
            // deleteUserCheckboxCK
            // 
            this.deleteUserCheckboxCK.AutoSize = true;
            this.deleteUserCheckboxCK.Enabled = false;
            this.deleteUserCheckboxCK.Location = new System.Drawing.Point(47, 275);
            this.deleteUserCheckboxCK.Name = "deleteUserCheckboxCK";
            this.deleteUserCheckboxCK.Size = new System.Drawing.Size(119, 21);
            this.deleteUserCheckboxCK.TabIndex = 11;
            this.deleteUserCheckboxCK.Text = "Delete Quality";
            this.deleteUserCheckboxCK.UseVisualStyleBackColor = true;
            // 
            // editedQualityTextboxTB
            // 
            this.editedQualityTextboxTB.Location = new System.Drawing.Point(15, 47);
            this.editedQualityTextboxTB.Name = "editedQualityTextboxTB";
            this.editedQualityTextboxTB.ReadOnly = true;
            this.editedQualityTextboxTB.Size = new System.Drawing.Size(144, 22);
            this.editedQualityTextboxTB.TabIndex = 1;
            // 
            // newQualityTextboxTB
            // 
            this.newQualityTextboxTB.Location = new System.Drawing.Point(13, 429);
            this.newQualityTextboxTB.Name = "newQualityTextboxTB";
            this.newQualityTextboxTB.Size = new System.Drawing.Size(149, 22);
            this.newQualityTextboxTB.TabIndex = 13;
            // 
            // newAccessLevelLabel
            // 
            this.newAccessLevelLabel.AutoSize = true;
            this.newAccessLevelLabel.Location = new System.Drawing.Point(12, 275);
            this.newAccessLevelLabel.Name = "newAccessLevelLabel";
            this.newAccessLevelLabel.Size = new System.Drawing.Size(29, 17);
            this.newAccessLevelLabel.TabIndex = 0;
            this.newAccessLevelLabel.Text = "OR";
            // 
            // newConfirmPasswordLabel
            // 
            this.newConfirmPasswordLabel.AutoSize = true;
            this.newConfirmPasswordLabel.Location = new System.Drawing.Point(12, 409);
            this.newConfirmPasswordLabel.Name = "newConfirmPasswordLabel";
            this.newConfirmPasswordLabel.Size = new System.Drawing.Size(52, 17);
            this.newConfirmPasswordLabel.TabIndex = 0;
            this.newConfirmPasswordLabel.Text = "Quality";
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.Location = new System.Drawing.Point(12, 28);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Size = new System.Drawing.Size(52, 17);
            this.newPasswordLabel.TabIndex = 0;
            this.newPasswordLabel.Text = "Quality";
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(15, 302);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 31);
            this.confirmButton.TabIndex = 9;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click_1);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.Location = new System.Drawing.Point(10, 5);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(111, 25);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = "Edit Quality";
            // 
            // addHSNNoTB
            // 
            this.addHSNNoTB.Location = new System.Drawing.Point(14, 474);
            this.addHSNNoTB.Name = "addHSNNoTB";
            this.addHSNNoTB.Size = new System.Drawing.Size(147, 22);
            this.addHSNNoTB.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 454);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "HSN Number";
            // 
            // editHSNNoTB
            // 
            this.editHSNNoTB.Location = new System.Drawing.Point(15, 92);
            this.editHSNNoTB.Name = "editHSNNoTB";
            this.editHSNNoTB.Size = new System.Drawing.Size(147, 22);
            this.editHSNNoTB.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "HSN Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Print Pattern";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 499);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Print Pattern";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 165);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 59);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // hsComboBox3
            // 
            this.hsComboBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.hsComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hsComboBox3.FormattingEnabled = true;
            this.hsComboBox3.Items.AddRange(new object[] {
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond"});
            this.hsComboBox3.Location = new System.Drawing.Point(15, 135);
            this.hsComboBox3.Name = "hsComboBox3";
            this.hsComboBox3.Size = new System.Drawing.Size(147, 23);
            this.hsComboBox3.TabIndex = 5;
            this.hsComboBox3.SelectedIndexChanged += new System.EventHandler(this.hsComboBox3_SelectedIndexChanged);
            // 
            // hsComboBox2
            // 
            this.hsComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.hsComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hsComboBox2.FormattingEnabled = true;
            this.hsComboBox2.Items.AddRange(new object[] {
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond",
            "Horizontal",
            "Min",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "LargeGrid",
            "Max",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical",
            "SmallConfetti",
            "LargeConfetti",
            "ZigZag",
            "Wave",
            "DiagonalBrick",
            "HorizontalBrick",
            "Weave",
            "Plaid",
            "Divot",
            "DottedGrid",
            "DottedDiamond",
            "Shingle",
            "Trellis",
            "Sphere",
            "SmallGrid",
            "SmallCheckerBoard",
            "LargeCheckerBoard",
            "OutlinedDiamond",
            "SolidDiamond"});
            this.hsComboBox2.Location = new System.Drawing.Point(12, 519);
            this.hsComboBox2.Name = "hsComboBox2";
            this.hsComboBox2.Size = new System.Drawing.Size(149, 23);
            this.hsComboBox2.TabIndex = 17;
            this.hsComboBox2.SelectedIndexChanged += new System.EventHandler(this.hsComboBox2_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(11, 553);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(209, 59);
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // editQBTCB
            // 
            this.editQBTCB.Enabled = false;
            this.editQBTCB.FormattingEnabled = true;
            this.editQBTCB.Location = new System.Drawing.Point(13, 248);
            this.editQBTCB.Name = "editQBTCB";
            this.editQBTCB.Size = new System.Drawing.Size(147, 24);
            this.editQBTCB.TabIndex = 29;
            // 
            // addQBTCB
            // 
            this.addQBTCB.FormattingEnabled = true;
            this.addQBTCB.Location = new System.Drawing.Point(11, 635);
            this.addQBTCB.Name = "addQBTCB";
            this.addQBTCB.Size = new System.Drawing.Size(147, 24);
            this.addQBTCB.TabIndex = 30;
            // 
            // editQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addQBTCB);
            this.Controls.Add(this.editQBTCB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hsComboBox3);
            this.Controls.Add(this.hsComboBox2);
            this.Controls.Add(this.editHSNNoTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addHSNNoTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addQualityButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteUserCheckboxCK);
            this.Controls.Add(this.editedQualityTextboxTB);
            this.Controls.Add(this.newQualityTextboxTB);
            this.Controls.Add(this.newAccessLevelLabel);
            this.Controls.Add(this.newConfirmPasswordLabel);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.dataGridView1);
            this.Name = "editQuality";
            this.Size = new System.Drawing.Size(800, 700);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.editQuality_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addQualityButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox deleteUserCheckboxCK;
        private System.Windows.Forms.TextBox editedQualityTextboxTB;
        private System.Windows.Forms.TextBox newQualityTextboxTB;
        private System.Windows.Forms.Label newAccessLevelLabel;
        private System.Windows.Forms.Label newConfirmPasswordLabel;
        private System.Windows.Forms.Label newPasswordLabel;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TextBox addHSNNoTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox editHSNNoTB;
        private System.Windows.Forms.Label label5;
        private HatchStyleComboBox.HSComboBox hsComboBox2;
        private HatchStyleComboBox.HSComboBox hsComboBox3;
        private Label label3;
        private Label label6;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private ComboBox editQBTCB;
        private ComboBox addQBTCB;
    }
}
