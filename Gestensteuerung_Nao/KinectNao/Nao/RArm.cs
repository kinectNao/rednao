using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Nao
{
    class RArm : Arm
    {

        //Ranges of the single joints in radians
        public static Range<float> ShoulderPitch = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };

        public static Range<float> RShoulderRoll = new Range<float>() { Minimum = -1.6494f, Maximum = -0.0087f };
        public static Range<float> RElbowRoll = new Range<float>() { Minimum = -0.0087f, Maximum = 1.5621f };

        public static Range<float> ElbowYaw = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };
        public static Range<float> WristYaw = new Range<float>() { Minimum = -1.8238f, Maximum = 1.8238f };


        public void controlArm(Aldebaran.Proxies.MotionProxy mp, float SP, float SR, float ER, float EY, float WY)
        {

            SP = getSP(SP);

            //Joint Controll
            //Pitch=Rot(y), Roll=Rot(z), Yaw=Rot(x) 
            String[] names = { "RShoulderPitch", "RShoulderRoll", "RElbowRoll", "RElbowYaw", "RWristYaw" };
            float[] newangles = { SP, SR, ER, EY, WY };
            //be careful, too fast, too dangerous ;)
            float fractionMaxSpeed = 0.5f;
            mp.setAngles(names, newangles, fractionMaxSpeed);
        }

        public float getSP(float RSP)
        {
            if (RSP < ConvertToRadians(90)) return RSP = ConvertToRadians(90) - RSP;
            else if (RSP > ConvertToRadians(90)) return RSP = (-1) * (RSP - ConvertToRadians(90));
            return 0; //RSP = 90°
        }

        public float ConvertToRadians(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }
    }
}
