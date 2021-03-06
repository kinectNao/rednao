﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Kinect
{
    public class ArmAngles
    {
  
        public float shoulderPitch_Right { get; set; }
        public float shoulderRoll_Right { get; set; }
        public float elbowRoll_Right { get; set; }
        public float elbowYaw_Right { get; set; }

        public float shoulderPitch_Left { get; set; }
        public float shoulderRoll_Left { get; set; }
        public float elbowRoll_Left { get; set; }
        public float elbowYaw_Left { get; set; }


        public ArmAngles(float shoulderPitch_Right, float shoulderRoll_Right, float elbowRoll_Right, float elbowYaw_Right, float shoulderPitch_Left, float shoulderRoll_Left,
            float elbowRoll_Left, float elbowYaw_Left)
        {
            this.shoulderPitch_Right = shoulderPitch_Right;
            this.shoulderRoll_Right = shoulderRoll_Right;
            this.elbowRoll_Right = elbowRoll_Right;
            this.elbowYaw_Right = elbowYaw_Right;

            this.shoulderPitch_Left = shoulderPitch_Left;
            this.shoulderRoll_Left = shoulderRoll_Left;
            this.elbowRoll_Left = elbowRoll_Left;
            this.elbowYaw_Left = elbowYaw_Left;

        }



    }
}
