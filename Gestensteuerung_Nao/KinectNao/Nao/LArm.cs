using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Nao
{
    public class LArm : Arm
    {
        //Ranges of the single joints in radians
        public static Range<float> ShoulderPitch = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };

        public static Range<float> LShoulderRoll = new Range<float>() { Minimum = 0.0087f, Maximum = 1.6494f };
        public static Range<float> LElbowRoll = new Range<float>() { Minimum = -1.5621f, Maximum = -0.0087f };

        public static Range<float> ElbowYaw = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };
        public static Range<float> WristYaw = new Range<float>() { Minimum = -1.8238f, Maximum = 1.8238f };
        
        private Aldebaran.Proxies.MotionProxy mp;
        private Angles angles;

        private static String[] joints = { "LShoulderPitch", "LShoulderRoll", "LElbowRoll", "LElbowYaw", "LWristYaw" };
        enum l
        {
            ShoulderPitch, ShoulderRoll, EllbowRoll, EllbowYaw
        }




        public LArm(Aldebaran.Proxies.MotionProxy mp)
        {
            this.mp = mp;
            angles = new Angles(mp, joints);
        }

      
        public override void controlArm( float SP, float SR, float ER, float EY, float WY)
        {
            float[] newangles = { SP, SR, ER, EY, WY };

            newangles = convertAngles(newangles);   //convert into nao-kinematic
            newangles = verifyAngles(newangles);    //verify angles are in range
            newangles = angles.checkDifference(newangles);     //check difference between old & new angle


            //set new current angles
            angles.currAngles = newangles.OfType<float>().ToList<float>();

            //set angles is non-blacking call!
            //mp.setAngles(joints, newangles, fractionMaxSpeed);

            //angleInterpolation is a blocking call!
            mp.post.angleInterpolationWithSpeed(joints, newangles, Arm.fractionMaxSpeed);
            
            

        }
      

        public override float[] convertAngles(float[] kinectAngles)
        {
            kinectAngles[(int)l.ShoulderPitch] = angles.invertGreaterThan90(kinectAngles[(int)l.ShoulderPitch]); //SP
            kinectAngles[(int)l.ShoulderRoll] = angles.invertLowerThan90(kinectAngles[(int)l.ShoulderRoll]); //SR
            //ER = invertLowerThan90(ER);
            kinectAngles[(int)l.EllbowYaw] = angles.invertGreaterThan90(kinectAngles[(int)l.EllbowYaw]); //EY

            //Formel für Ellbow Roll
            float m = -0.7457f;
            float b = 2.1563f;
            kinectAngles[(int)l.EllbowRoll] = m * kinectAngles[(int)l.EllbowRoll] + b; //ER
            kinectAngles[(int)l.EllbowRoll] *= -1f;

            return kinectAngles;
        }


        public override float[] verifyAngles(float[] convertedAngles)
        {
            
            //if convertedAngle is outside range, take old angle
            if (!ShoulderPitch.ContainsValue(convertedAngles[(int)l.ShoulderPitch]))
                convertedAngles[(int)l.ShoulderPitch] = angles.currAngles[(int)l.ShoulderPitch];

            if (!LShoulderRoll.ContainsValue(convertedAngles[(int)l.ShoulderRoll]))
                convertedAngles[(int)l.ShoulderRoll] = angles.currAngles[(int)l.ShoulderRoll];

            if (!LElbowRoll.ContainsValue(convertedAngles[(int)l.EllbowRoll]))
                convertedAngles[(int)l.EllbowRoll] = angles.currAngles[(int)l.EllbowRoll];

            if (!ElbowYaw.ContainsValue(convertedAngles[(int)l.EllbowYaw]))
                convertedAngles[(int)l.EllbowYaw] = angles.currAngles[(int)l.EllbowYaw];

            return convertedAngles;
        }
    }
}
