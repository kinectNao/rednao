using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public interface Arm
    {

        public static float ConvertToRadians(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }

        //Control LArm with all Joints
        public void controlArm(MotionProxy mp, float LSP, float LSR, float LER, float LEY, float LWY);
        





      


    }
}
