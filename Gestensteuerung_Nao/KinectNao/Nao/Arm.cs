using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public abstract class Arm
    {
        protected static float fractionMaxSpeed = 0.2f;

        

        public static float inRadian(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }

        //Control LArm with all Joints
        public abstract void controlArm(MotionProxy mp, float LSP, float LSR, float LER, float LEY, float LWY);


        //Kinect Winkel > 90° --> invertieren
        public float invertGreaterThan90(float angle)
        {
            if (angle < inRadian(90)) return angle = inRadian(90) - angle;
            else if (angle > inRadian(90)) return angle = (-1) * (angle - inRadian(90));
            return 0; //RSP = 90°
        }

        //Kinect Winkel < 90° --> invertieren
        public float invertLowerThan90(float angle)
        {
            if (angle < inRadian(90)) return angle = (inRadian(90) - angle) *(-1);
            else if (angle > inRadian(90)) return angle = (angle - inRadian(90));
            return 0; //RSP = 90°
        }

       
       

     




      


    }
}
