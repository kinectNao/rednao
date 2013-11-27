using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;
using NaoTestDev;

namespace ConsoleApplication1
{
    class Program
    {
        //Local or Remote
        const String ip = "127.0.0.1";
        const int port = 9559;

        public static float ConvertToRadians(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }

        static void Main(string[] args)
        {

            //make sure nao is ready to move
            try
            {
                RobotPostureProxy rp = new RobotPostureProxy("127.0.0.1", 9559);
                rp.goToPosture("StandZero", 1);
            }
            catch (Exception )
            {
                
                throw new Exception("Cannot connect to Proxy");
            }
           
            


            //NavigationProxy np = new NavigationProxy(ip, port);
            //if (np.moveTo(10, 0, 0)) Console.WriteLine("no obstacle");
            //else Console.WriteLine("obstacle!!!");

            MotionProxy mp;
            try
            {
                mp = new MotionProxy(ip, port);
            }
            catch (Exception)
            {

                throw new Exception("Cannot connect to Proxy");
            }


            Arms RArm = new Arms();
            Arms LArm = new Arms();

            

            

            //cartesian control
            //List: x,y,z,dx,dy,dz
            //List<float> change = new List<float> { -0.19855f, 0.19855f, 0, 0, 0, 0 };
            //mp.setPosition("LArm", 2, change, 0.3f, 63);

            //Console.ReadLine();
            
        }



        
    }
}