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

        float ConvertToRadians(double angle);

        //Control LArm with all Joints
        void controlArm(MotionProxy mp, float LSP, float LSR, float LER, float LEY, float LWY);
        





      


    }
}
