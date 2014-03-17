using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public class Angles
    {
        public List<float> currAngles {get; set;}

        public Angles(MotionProxy mp, String[] joints)
        {
           currAngles = mp.getAngles(joints, true);   
        }

        public Angles()
        {
            // TODO: Complete member initialization
        }

        /*
         * set to currAngle if difference to newAngle is too small
         * */
        public float[] checkDifference(float[] newAngles)
        {
            int diffAngle;
            float[] filteredAngles = new float[5];

            for (int i = 0; i < newAngles.Length; i++)
            {
                float difference = findDifference(newAngles[i], currAngles[i]);
                diffAngle = (i == 2) ? (5) : (10); //for ellbow roll lower diffAngle cause of angle-range

                //if diff is less or equal 5°/10° set to currAngle otherwise newAngle
                filteredAngles[i] = (difference <= inRadian(diffAngle)) ? (currAngles[i]) : (newAngles[i]);
            }

            return filteredAngles;
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

        public float findDifference(float nr1, float nr2)
        {
            return Math.Abs(nr1 - nr2);
        }


    }
}
