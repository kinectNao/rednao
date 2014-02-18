using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    class Arms
    {

        //Ranges of the single joints in radians
        public static Range<float> ShoulderPitch = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };

        public static Range<float> LShoulderRoll = new Range<float>() { Minimum = 0.0087f, Maximum = 1.6494f };
        public static Range<float> LElbowRoll = new Range<float>() { Minimum = -1.5621f, Maximum = -0.0087f };

        public static Range<float> RShoulderRoll = new Range<float>() { Minimum = -1.6494f, Maximum = -0.0087f };
        public static Range<float> RElbowRoll = new Range<float>() { Minimum = -0.0087f, Maximum = 1.5621f };


        public static Range<float> ElbowYaw = new Range<float>() { Minimum = -2.0857f, Maximum = -2.0857f };
        public static Range<float> WristYaw = new Range<float>() { Minimum = -1.8238f, Maximum = -1.8238f };

        public static float ConvertToRadians(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }

        //Control LArm with all Joints
        public static void controlLArm(MotionProxy mp, float LSP, float LSR, float LER, float LEY, float LWY)
        {
            //Joint Controll
            //Pitch=Rot(y), Roll=Rot(z), Yaw=Rot(x) 
            String[] names = { "LShoulderPitch", "LShoulderRoll", "LElbowRoll", "LElbowYaw", "LWristYaw" };
            float[] newangles = { LSP, LSR, LER, LEY, LWY };
            float fractionMaxSpeed = 0.1f;

            mp.setAngles(names, newangles, fractionMaxSpeed);
        }

        //Control RArm with all Joints
        public static void controlRArm(MotionProxy mp, float RSP, float RSR, float RER, float REY, float RWY)
        {

            //Joint Controll
            //Pitch=Rot(y), Roll=Rot(z), Yaw=Rot(x) 
            String[] names = { "RShoulderPitch", "RShoulderRoll", "RElbowRoll", "RElbowYaw", "RWristYaw" };
            float[] newangles = { RSP, RSR, RER, REY, RWY };
            float fractionMaxSpeed = 0.1f;
            mp.setAngles(names, newangles, fractionMaxSpeed);
        }


    }
}
