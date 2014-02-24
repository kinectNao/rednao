using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aldebaran.Proxies;

namespace KinectNao.Nao
{
    public abstract class Arm
    {

        public float inRadian(double angle)
        {
            var rad = (Math.PI / 180) * angle;
            return (float)rad;
        }

        //Control LArm with all Joints
        public abstract void controlArm(MotionProxy mp, float LSP, float LSR, float LER, float LEY, float LWY);


        //SP Winkel sind für beide Arme gleich
        public float getSPAngle(float sp)
        {
            if (sp < inRadian(90)) return sp = inRadian(90) - sp;
            else if (sp > inRadian(90)) return sp = (-1) * (sp - inRadian(90));
            return 0; //RSP = 90°
        }

        //EY Winkel sind für beide Arme gleich
        public float getEYAngle(float ey)
        {
            if (ey < inRadian(90)) return ey = (inRadian(90) - ey) *(-1);
            else if (ey > inRadian(90)) return ey = (ey - inRadian(90));
            return 0; //RSP = 90°
        }

        //Winkel hier sind je Arm untersch.
        public abstract float getAngleForArm(float angle);

     




      


    }
}
