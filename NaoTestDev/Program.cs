using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;
using NaoTestDev;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    public class Program
    {
        //Local or Remote
        const String ip = "127.0.0.1";
        const int port = 9559;

        private MotionProxy mp;
        private RobotPostureProxy rp;

        public Program()
        {
            init();
        }

        private void init()
        {
            Console.ReadLine();
            //make sure nao is ready to move
            try
            {

                rp = new RobotPostureProxy("127.0.0.1", 9559);
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


            Arms RArm = new Arms();
            
         
        }

        public void controlLArm(float LSP, float LSR, float LER, float LEY, float LWY)
        {
            Arms LArm = new Arms();

            NaoTestDev.Arms.controlLArm(mp,  LSP,  LSR,  LER,  LEY,  LWY);
        }

        static void Main(string[] args)
        {
            Program main = new Program();
            Form1 f = new Form1(main);
            f.Show();
            Application.Run();
            
        }



        
    }
}