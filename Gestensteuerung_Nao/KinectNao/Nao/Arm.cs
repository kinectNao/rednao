using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public abstract class  Arm
    {
        protected static float fractionMaxSpeed = 0.3f;

        //Control LArm with all Joints

        public abstract void controlArm(float LSP, float LSR, float LER, float LEY, float LWY);

        public abstract float[] convertAngles(float[] angles);

        public abstract float[] verifyAngles(float[] convertedAngles);



    }
}
