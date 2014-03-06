using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Nao
{
    class LArm : Arm
    {
        //Ranges of the single joints in radians
        public static Range<float> ShoulderPitch = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };

        public static Range<float> LShoulderRoll = new Range<float>() { Minimum = 0.0087f, Maximum = 1.6494f };
        public static Range<float> LElbowRoll = new Range<float>() { Minimum = -1.5621f, Maximum = -0.0087f };

        public static Range<float> ElbowYaw = new Range<float>() { Minimum = -2.0857f, Maximum = 2.0857f };
        public static Range<float> WristYaw = new Range<float>() { Minimum = -1.8238f, Maximum = 1.8238f };





        public override void controlArm(Aldebaran.Proxies.MotionProxy mp, float SP, float SR, float ER, float EY, float WY)
        {
            SP = invertGreaterThan90(SP);
            SR = invertLowerThan90(SR);
            //ER = invertLowerThan90(ER);
            EY = invertGreaterThan90(EY);


            //Formel
            float m = -0.7457f;
            float b = 2.1563f;

            ER = m * ER + b;
            ER *= -1f;

            //Joint Controll
            //Pitch=Rot(y), Roll=Rot(z), Yaw=Rot(x) 
            String[] names = { "LShoulderPitch", "LShoulderRoll", "LElbowRoll", "LElbowYaw", "LWristYaw" };
            float[] newangles = { SP, SR, ER, EY, WY };
            //float[] newangles = { 0, 0, ER, 0, 0 };
            float fractionMaxSpeed = 0.3f;

            mp.setAngles(names, newangles, fractionMaxSpeed);


        }



    }
}
