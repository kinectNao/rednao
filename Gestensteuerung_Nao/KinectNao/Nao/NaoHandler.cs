using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;
using KinectNao.Kinect;

namespace KinectNao.Nao
{
    public class NaoHandler : ISkeletonAngles
    {

        //Local or Remote
        const String ip = "127.0.0.1";
        const int port = 9559;

        private MotionProxy mp;
        private RobotPostureProxy rp;
        private RArm RArm;
        private LArm LArm;
        private SkeletonAngleHandler handler;

        public NaoHandler(SkeletonAngleHandler _handler)
        {
            handler = _handler;
            handler.addMeToAngleSubscriber(this);
            init();
        }


        private void init()
        {
            try
            {

                rp = new RobotPostureProxy(ip, 9559);
                rp.goToPosture("StandZero", 1);

            }
            catch (Exception)
            {

                throw new Exception("Cannot connect to Proxy");
            }

            try
            {
                mp = new MotionProxy(ip, port);
            }
            catch (Exception)
            {

                throw new Exception("Cannot connect to Proxy");
            }


            RArm = new RArm();
            LArm = new LArm();

        }

  




        public void updateAngles(float shoulderPitch, float shoulderRoll, float ellbowRoll, float ellbowYaw)
        {
            //RArm.controlRArm(mp, shoulderPitch, shoulderRoll, ellbowRoll, ellbowYaw, 1.0f);
            RArm.controlArm(mp, shoulderPitch, 0, 0, 0, 0);
        }
    }
}
