using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public class Angles
    {
        public List<float> currentAngles {get; set;}

        public Angles(MotionProxy mp, String[] joints)
        {
           currentAngles = mp.getAngles(joints, true);   
        }

        /*
         * Verify that angles are in range & difference not too small
         * */
        public float[] filter(float[] newAngles)
        {
            
            return null;
        }

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
            if (angle < inRadian(90)) return angle = (inRadian(90) - angle) * (-1);
            else if (angle > inRadian(90)) return angle = (angle - inRadian(90));
            return 0; //RSP = 90°
        }

        public static float inRadian(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }

    }
}
