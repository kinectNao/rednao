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


        public override void controlArm(Aldebaran.Proxies.MotionProxy mp, float SP, float SR, float ER, float EY, float WY)
        {

            SP = invertGreaterThan90(SP);
            SR = invertGreaterThan90(SR);
            //ER = invertGreaterThan90(ER);
            EY = invertLowerThan90(EY);


            //Formel
            float m = -0.7457f;
            float b = 2.1563f;

            ER = m * ER + b;
             
            //Joint Controll
            //Pitch=Rot(y), Roll=Rot(z), Yaw=Rot(x) 
            String[] names = { "RShoulderPitch", "RShoulderRoll", "RElbowRoll", "RElbowYaw", "RWristYaw" };
            float[] newangles = { SP, SR, ER, EY, WY };
            
            //be careful, too fast, too dangerous ;)
            float fractionMaxSpeed = 0.5f;
            mp.setAngles(names, newangles, fractionMaxSpeed);
        }

    }
}
