using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Nao
{
    public class RArm : Arm
    {

        //Ranges of the single joints in radians
        public static Range<float> ShoulderPitch = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };

        public static Range<float> RShoulderRoll = new Range<float>() { Minimum = -1.6494f, Maximum = -0.0087f };
        public static Range<float> RElbowRoll = new Range<float>() { Minimum = 0.0087f, Maximum = 1.5621f };

        public static Range<float> ElbowYaw = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };
        public static Range<float> WristYaw = new Range<float>() { Minimum = -1.8238f, Maximum = 1.8238f };

        enum r {
            ShoulderPitch , ShoulderRoll, EllbowRoll, EllbowYaw
        }

        private static String[] joints = { "RShoulderPitch", "RShoulderRoll", "RElbowRoll", "RElbowYaw", "RWristYaw" };

        int threadId;
        private Aldebaran.Proxies.MotionProxy mp;
        public Angles angles;

        public RArm(Aldebaran.Proxies.MotionProxy mp)
        {
            this.mp = mp;
            angles = new Angles(mp, joints);
        }

        public RArm()
        {
            // TODO: Complete member initialization
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

            //angleInterpolation is a blocking call; post is used to create each a thread for the arms
            mp.post.angleInterpolationWithSpeed(joints, newangles, Arm.fractionMaxSpeed);

        }
 

        public override float[] convertAngles(float[] kinectAngles)
        {
            kinectAngles[(int)r.ShoulderPitch] = angles.invertGreaterThan90(kinectAngles[(int)r.ShoulderPitch]); //SP
            kinectAngles[(int)r.ShoulderRoll] = angles.invertGreaterThan90(kinectAngles[(int)r.ShoulderRoll]); //SR
            //ER = invertGreaterThan90(ER);
            kinectAngles[(int)r.EllbowYaw] = angles.invertLowerThan90(kinectAngles[(int)r.EllbowYaw]); //EY


            //Formel ER
            float m = -0.7457f;
            float b = 2.1563f;

            kinectAngles[(int)r.EllbowRoll] = m * kinectAngles[(int)r.EllbowRoll] + b; //ER

            return kinectAngles;
        }


        public override float[] verifyAngles(float[] convertedAngles)
        {
            //if convertedAngle is outside range, take old angle
            if (!ShoulderPitch.ContainsValue(convertedAngles[(int)r.ShoulderPitch]))
                convertedAngles[(int)r.ShoulderPitch] = angles.currAngles[(int)r.ShoulderPitch];

            if(!RShoulderRoll.ContainsValue(convertedAngles[(int)r.ShoulderRoll]))
                convertedAngles[(int)r.ShoulderRoll] = angles.currAngles[(int)r.ShoulderRoll];

            if (!RElbowRoll.ContainsValue(convertedAngles[(int)r.EllbowRoll]))
                convertedAngles[(int)r.EllbowRoll] = angles.currAngles[(int)r.EllbowRoll];

            if (!ElbowYaw.ContainsValue(convertedAngles[(int)r.EllbowYaw]))
                convertedAngles[(int)r.EllbowYaw] = angles.currAngles[(int)r.EllbowYaw];

            return convertedAngles;
        }
    }
}
