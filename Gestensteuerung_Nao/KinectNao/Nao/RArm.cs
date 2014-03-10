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

        const String[] joints = { "RShoulderPitch", "RShoulderRoll", "RElbowRoll", "RElbowYaw", "RWristYaw" };

        int threadId;
        private Aldebaran.Proxies.MotionProxy mp;

        public RArm(Aldebaran.Proxies.MotionProxy mp)
        {
            this.mp = mp;
        }

        public override void controlArm( float SP, float SR, float ER, float EY, float WY)
        {
            float[] newangles = { SP, SR, ER, EY, WY };
            newangles = convertAngles(newangles);
           
            //set angles is non-blacking call!
            //mp.setAngles(names, newangles, fractionMaxSpeed);

            //angleInterpolation is a blocking call; post is used to create each a thread for the arms
            mp.post.angleInterpolationWithSpeed(joints, newangles, fractionMaxSpeed);



        }

        // TODO: verify that angles are in naos space, else return current angle
        public override float[] verifyAngles(float[] newAngles)
        {
            List<float> currentAngles = mp.getAngles(joints, true);


            throw new NotImplementedException();
        }

        public override float[] convertAngles(float[] angles)
        {
            angles[(int)r.ShoulderPitch] = invertGreaterThan90(angles[(int)r.ShoulderPitch]); //SP
            angles[(int)r.ShoulderRoll] = invertGreaterThan90(angles[(int)r.ShoulderRoll]); //SR
            //ER = invertGreaterThan90(ER);
            angles[(int)r.EllbowYaw] = invertLowerThan90(angles[(int)r.EllbowYaw]); //EY


            //Formel ER
            float m = -0.7457f;
            float b = 2.1563f;

            angles[(int)r.EllbowRoll] = m * angles[(int)r.EllbowRoll] + b; //ER

            return angles;
        }
    }
}
