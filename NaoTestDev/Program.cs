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
        public String ip { get; set; }
        const int port = 9559;

        private MotionProxy mp;
        private RobotPostureProxy rp;

        public Program()
        {
            
        }

        public void init()
        {
          
            //make sure nao is ready to move
            try
            {

                rp = new RobotPostureProxy(ip, 9559);
                rp.goToPosture("StandZero", 1);

                Console.WriteLine("Connected to Nao with IP: " + ip);

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