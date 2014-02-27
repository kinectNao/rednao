using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Kinect
{
    class Filter
    {
        // Queue fuer Mittelwertfilter
        private static int filtersize = 3;
        private ArmAngles[] armAngle = new ArmAngles[filtersize];
                


        public void addCurrentArmPos(ArmAngles currentAnglePos){
            throw new NotImplementedException();
        }


        public ArmAngles getArmValue()
        {
            throw new NotImplementedException();
        }

        

        

    }
}
