namespace KinectNaoHandler
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
            this.shoulderPitchLabel.Location = new System.Drawing.Point(13, 13);
            this.shoulderPitchLabel.Name = "shoulderPitchLabel";
            this.shoulderPitchLabel.Size = new System.Drawing.Size(76, 13);
            this.shoulderPitchLabel.TabIndex = 0;
            this.shoulderPitchLabel.Text = "Shoulder Pitch";
            // 
            // shoulderPitchValue
            // 
            this.shoulderPitchValue.AutoSize = true;
            this.shoulderPitchValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoulderPitchValue.Location = new System.Drawing.Point(95, 13);
            this.shoulderPitchValue.Name = "shoulderPitchValue";
            this.shoulderPitchValue.Size = new System.Drawing.Size(58, 17);
            this.shoulderPitchValue.TabIndex = 1;
            this.shoulderPitchValue.Text = "VALUE";
            // 
            // shoulderRollLabel
            // 
            this.shoulderRollLabel.AutoSize = true;
            this.shoulderRollLabel.Location = new System.Drawing.Point(12, 45);
            this.shoulderRollLabel.Name = "shoulderRollLabel";
            this.shoulderRollLabel.Size = new System.Drawing.Size(70, 13);
            this.shoulderRollLabel.TabIndex = 2;
            this.shoulderRollLabel.Text = "Shoulder Roll";
            // 
            // shoulderRollValue
            // 
            this.shoulderRollValue.AutoSize = true;
            this.shoulderRollValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoulderRollValue.Location = new System.Drawing.Point(95, 45);
            this.shoulderRollValue.Name = "shoulderRollValue";
            this.shoulderRollValue.Size = new System.Drawing.Size(58, 17);
            this.shoulderRollValue.TabIndex = 3;
            this.shoulderRollValue.Text = "VALUE";
            // 
            // ellbowRollLabel
            // 
            this.ellbowRollLabel.AutoSize = true;
            this.ellbowRollLabel.Location = new System.Drawing.Point(13, 107);
            this.ellbowRollLabel.Name = "ellbowRollLabel";
            this.ellbowRollLabel.Size = new System.Drawing.Size(59, 13);
            this.ellbowRollLabel.TabIndex = 4;
            this.ellbowRollLabel.Text = "Ellbow Roll";
            // 
            // ellbowRollValue
            // 
            this.ellbowRollValue.AutoSize = true;
            this.ellbowRollValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellbowRollValue.Location = new System.Drawing.Point(95, 107);
            this.ellbowRollValue.Name = "ellbowRollValue";
            this.ellbowRollValue.Size = new System.Drawing.Size(58, 17);
            this.ellbowRollValue.TabIndex = 5;
            this.ellbowRollValue.Text = "VALUE";
            // 
            // ellbowYawValue
            // 
            this.ellbowYawValue.AutoSize = true;
            this.ellbowYawValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ellbowYawValue.Location = new System.Drawing.Point(95, 137);
            this.ellbowYawValue.Name = "ellbowYawValue";
            this.ellbowYawValue.Size = new System.Drawing.Size(58, 17);
            this.ellbowYawValue.TabIndex = 6;
            this.ellbowYawValue.Text = "VALUE";
            // 
            // ellbowYawLabel
            // 
            this.ellbowYawLabel.AutoSize = true;
            this.ellbowYawLabel.Location = new System.Drawing.Point(12, 137);
            this.ellbowYawLabel.Name = "ellbowYawLabel";
            this.ellbowYawLabel.Size = new System.Drawing.Size(62, 13);
            this.ellbowYawLabel.TabIndex = 7;
            this.ellbowYawLabel.Text = "Ellbow Yaw";
            // 
            // AngleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 178);
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

        public void updateAngles(float shoulderPitch, float shoulderRoll, float ellbowRoll, float ellbowYaw)
        {

            this.BeginInvoke((MethodInvoker)delegate {

                shoulderPitchValue.Text = Convert.ToString(shoulderPitch);
                shoulderRollValue.Text = Convert.ToString(shoulderRoll);
                ellbowRollValue.Text = Convert.ToString(ellbowRoll);
                ellbowYawValue.Text = Convert.ToString(ellbowYaw);
            
            
            });
            
        }
    }

   
}