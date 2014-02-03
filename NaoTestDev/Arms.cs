using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace NaoTestDev
{
    class Arms
    {

        //Ranges of the single joints in radians
        public static Range<float> ShoulderPitch = new Range<float>() {Minimum=-2.0857f, Maximum=2.0857f };

        public static Range<float> LShoulderRoll = new Range<float>() { Minimum = 0.0087f, Maximum = 1.6494f };
        public static Range<float> LElbowRoll = new Range<float>() { Minimum = -1.5621f, Maximum = -0.0087f };

        public static Range<float> RShoulderRoll = new Range<float>() { Minimum = -1.6494f, Maximum = -0.0087f };
        public static Range<float> RElbowRoll = new Range<float>() { Minimum = -0.0087f, Maximum = 1.5621f };


        public static Range<float> ElbowYaw = new Range<float>() { Minimum = -2.0857f, Maximum = -2.0857f };
        public static Range<float> WristYaw = new Range<float>() { Minimum = -1.8238f, Maximum = -1.8238f };


        //Control LArm with all Joints
        private static void LArm(MotionProxy mp)
        {

            //Joint Controll
            //Pitch=Rot(y), Roll=Rot(z), Yaw=Rot(x) 
            //Rotate Arms around 180° in Kinect-Init, Position
            String[] names = { "LElbowYaw", "RElbowYaw", "LWristYaw", "RWristYaw" };
            float[] newangles = { ElbowYaw.Maximum, ElbowYaw, ConvertToRadians(-60.5), ConvertToRadians(60.5) };
            float fractionMaxSpeed = 0.1f;
            mp.setAngles(names, newangles, fractionMaxSpeed);
        }


    }
}
