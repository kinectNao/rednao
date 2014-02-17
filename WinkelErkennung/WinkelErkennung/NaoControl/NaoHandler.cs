using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;


namespace NaoControl
{
    public class NaoHandler : ISkeletonAngles
    {

         //Local or Remote
        const String ip = "127.0.0.1";
        const int port = 9559;

        private MotionProxy mp;
        private RobotPostureProxy rp;
        private Arms RArm;
        private Arms LArm;


        public NaoHandler()
        {
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


            RArm = new Arms(); 
            LArm = new Arms();
         
        }

        public void controlLArm(float LSP, float LSR, float LER, float LEY, float LWY)
        {

            NaoControl.Arms.controlLArm(mp,  LSP,  LSR,  LER,  LEY,  LWY);
        }




        public void updateAngles(float shoulderPitch, float shoulderRoll, float ellbowRoll, float ellbowYaw)
        {
            NaoControl.Arms.controlLArm(mp, shoulderPitch, shoulderRoll, ellbowRoll, ellbowYaw, 0.0f);
        }
    }
}
