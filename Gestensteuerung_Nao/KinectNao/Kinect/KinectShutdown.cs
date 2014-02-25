using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Kinect;


namespace KinectNao.Kinect
{
    class KinectShutdown
    {
        KinectSensor sensor;

        public KinectShutdown(KinectSensor mySensor)
        {
            this.sensor = mySensor;
        }

        public void DoWork()
        {
            sensor.Stop();
        }

    }
}
