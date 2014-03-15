using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public interface  Arm
    {
        protected static float fractionMaxSpeed = 0.3f;

        //Control LArm with all Joints
        
        public void controlArm( float LSP, float LSR, float LER, float LEY, float LWY);
        
        public float[] convertAngles(float[] angles);

        public float[] verifyAngles(float[] convertedAngles);



    }
}
