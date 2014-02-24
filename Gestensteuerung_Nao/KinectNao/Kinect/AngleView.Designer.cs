namespace KinectNao.Kinect
{
    using System;
    using System.Windows.Forms;

    partial class AngleView : ISkeletonAngles
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
            handler.removeMeFromAngleSubscriber(this);
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
            this.shoulderPitchLabel = new System.Windows.Forms.Label();
            this.shoulderPitchValue = new System.Windows.Forms.Label();
            this.shoulderRollLabel = new System.Windows.Forms.Label();
            this.shoulderRollValue = new System.Windows.Forms.Label();
            this.ellbowRollLabel = new System.Windows.Forms.Label();
            this.ellbowRollValue = new System.Windows.Forms.Label();
            this.ellbowYawValue = new System.Windows.Forms.Label();
            this.ellbowYawLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // shoulderPitchLabel
            // 
            this.shoulderPitchLabel.AutoSize = true;
            this.shoulderPitchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoulderPitchLabel.Location = new System.Drawing.Point(12, 32);
            this.shoulderPitchLabel.Name = "shoulderPitchLabel";
            this.shoulderPitchLabel.Size = new System.Drawing.Size(225, 37);
            this.shoulderPitchLabel.TabIndex = 0;
            this.shoulderPitchLabel.Text = "Shoulder Pitch";
            // 
            // shoulderPitchValue
            // 
            this.shoulderPitchValue.AutoSize = true;
            this.shoulderPitchValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoulderPitchValue.Location = new System.Drawing.Point(264, 17);
            this.shoulderPitchValue.Name = "shoulderPitchValue";
            this.shoulderPitchValue.Size = new System.Drawing.Size(187, 55);
            this.shoulderPitchValue.TabIndex = 1;
            this.shoulderPitchValue.Text = "VALUE";
            // 
            // shoulderRollLabel
            // 
            this.shoulderRollLabel.AutoSize = true;
            this.shoulderRollLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoulderRollLabel.Location = new System.Drawing.Point(12, 117);
            this.shoulderRollLabel.Name = "shoulderRollLabel";
            this.shoulderRollLabel.Size = new System.Drawing.Size(208, 37);
            this.shoulderRollLabel.TabIndex = 2;
            this.shoulderRollLabel.Text = "Shoulder Roll";
            // 
            // shoulderRollValue
            // 
            this.shoulderRollValue.AutoSize = true;
            this.shoulderRollValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoulderRollValue.Location = new System.Drawing.Point(264, 102);
            this.shoulderRollValue.Name = "shoulderRollValue";
            this.shoulderRollValue.Size = new System.Drawing.Size(187, 55);
            this.shoulderRollValue.TabIndex = 3;
            this.shoulderRollValue.Text = "VALUE";
            // 
            // ellbowRollLabel
            // 
            this.ellbowRollLabel.AutoSize = true;
            this.ellbowRollLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellbowRollLabel.Location = new System.Drawing.Point(12, 227);
            this.ellbowRollLabel.Name = "ellbowRollLabel";
            this.ellbowRollLabel.Size = new System.Drawing.Size(174, 37);
            this.ellbowRollLabel.TabIndex = 4;
            this.ellbowRollLabel.Text = "Ellbow Roll";
            // 
            // ellbowRollValue
            // 
            this.ellbowRollValue.AutoSize = true;
            this.ellbowRollValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellbowRollValue.Location = new System.Drawing.Point(264, 209);
            this.ellbowRollValue.Name = "ellbowRollValue";
            this.ellbowRollValue.Size = new System.Drawing.Size(187, 55);
            this.ellbowRollValue.TabIndex = 5;
            this.ellbowRollValue.Text = "VALUE";
            // 
            // ellbowYawValue
            // 
            this.ellbowYawValue.AutoSize = true;
            this.ellbowYawValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellbowYawValue.Location = new System.Drawing.Point(264, 291);
            this.ellbowYawValue.Name = "ellbowYawValue";
            this.ellbowYawValue.Size = new System.Drawing.Size(187, 55);
            this.ellbowYawValue.TabIndex = 6;
            this.ellbowYawValue.Text = "VALUE";
            // 
            // ellbowYawLabel
            // 
            this.ellbowYawLabel.AutoSize = true;
            this.ellbowYawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellbowYawLabel.Location = new System.Drawing.Point(12, 309);
            this.ellbowYawLabel.Name = "ellbowYawLabel";
            this.ellbowYawLabel.Size = new System.Drawing.Size(183, 37);
            this.ellbowYawLabel.TabIndex = 7;
            this.ellbowYawLabel.Text = "Ellbow Yaw";
            // 
            // AngleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 380);
            this.Controls.Add(this.ellbowYawLabel);
            this.Controls.Add(this.ellbowYawValue);
            this.Controls.Add(this.ellbowRollValue);
            this.Controls.Add(this.ellbowRollLabel);
            this.Controls.Add(this.shoulderRollValue);
            this.Controls.Add(this.shoulderRollLabel);
            this.Controls.Add(this.shoulderPitchValue);
            this.Controls.Add(this.shoulderPitchLabel);
            this.Name = "AngleView";
            this.Text = "AngleView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label shoulderPitchLabel;
        private System.Windows.Forms.Label shoulderPitchValue;
        private System.Windows.Forms.Label shoulderRollLabel;
        private System.Windows.Forms.Label shoulderRollValue;
        private System.Windows.Forms.Label ellbowRollLabel;
        private System.Windows.Forms.Label ellbowRollValue;
        private System.Windows.Forms.Label ellbowYawValue;
        private System.Windows.Forms.Label ellbowYawLabel;


        private SkeletonAngleHandler handler;

        public AngleView(SkeletonAngleHandler _handler)
        {
            handler = _handler;
            _handler.addMeToAngleSubscriber(this);
            InitializeComponent();
        }



        public void updateAngles(float r_shoulderPitch, float r_shoulderRoll, float r_ellbowRoll, float r_ellbowYaw, float l_shoulderPitch, float l_shoulderRoll, float l_ellbowRoll, float l_ellbowYaw)
        {

            //Only for RArm
            this.BeginInvoke((MethodInvoker)delegate
            {

                r_shoulderPitch = 180 / 3.1415f * r_shoulderPitch;
                r_shoulderRoll = 180 / 3.1415f * r_shoulderRoll;
                r_ellbowRoll = 180 / 3.1415f * r_ellbowRoll;
                r_ellbowYaw = 180 / 3.1415f * r_ellbowYaw;

                shoulderPitchValue.Text = Convert.ToString(r_shoulderPitch);
                shoulderRollValue.Text = Convert.ToString(r_shoulderRoll);
                ellbowRollValue.Text = Convert.ToString(r_ellbowRoll);
                ellbowYawValue.Text = Convert.ToString(r_ellbowYaw);


            });
        }
    }


}