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

        private static String[] joints = { "LShoulderPitch", "LShoulderRoll", "LElbowRoll", "LElbowYaw", "LWristYaw" };
        enum l
        {
            ShoulderPitch, ShoulderRoll, EllbowRoll, EllbowYaw
        }




        public LArm(Aldebaran.Proxies.MotionProxy mp)
        {
            this.mp = mp;
        }

      
        public override void controlArm( float SP, float SR, float ER, float EY, float WY)
        {
            float[] newangles = { SP, SR, ER, EY, WY };

            newangles = convertAngles(newangles); //Convert in Nao-Kinematic

            //set angles is non-blacking call!
            //mp.setAngles(names, newangles, fractionMaxSpeed);

            //angleInterpolation is a blocking call!
            mp.post.angleInterpolationWithSpeed(joints, newangles, fractionMaxSpeed);

            

        }




        public override float[] verifyAngles(float[] angles)
        {
            throw new NotImplementedException();
        }

        public override float[] convertAngles(float[] angles)
        {
            angles[(int)l.ShoulderPitch] = invertGreaterThan90(angles[(int)l.ShoulderPitch]); //SP
            angles[(int)l.ShoulderRoll] = invertLowerThan90(angles[(int)l.ShoulderRoll]); //SR
            //ER = invertLowerThan90(ER);
            angles[(int)l.EllbowYaw] = invertGreaterThan90(angles[(int)l.EllbowYaw]); //EY

            //Formel für Ellbow Roll
            float m = -0.7457f;
            float b = 2.1563f;
            angles[(int)l.EllbowRoll] = m * angles[(int)l.EllbowRoll] + b; //ER
            angles[(int)l.EllbowRoll] *= -1f;

            return angles;
        }
    }
}
