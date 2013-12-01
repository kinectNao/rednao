using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KinectNaoHandler
{
    class SkeletonAngleCalculator
    {
        private SkeletonAngleHandler skeletonAngleHandler;

        public SkeletonAngleCalculator(SkeletonAngleHandler skeletonAngleHandler)
        {
            this.skeletonAngleHandler = skeletonAngleHandler;
        }

        //AngleView view = new AngleView();
        
        // This method will be called when the thread is started.
        public void DoWork()   
        {
            while (!_shouldStop)
            {
               // Hier werden Berechnungen getätigt
                //Console.WriteLine("ERGEBNIS = DUMMY!!!");
                skeletonAngleHandler.updateAngles(1.0f, 0.1f, 0.5f, -2.0f);
                Thread.Sleep(500);
                skeletonAngleHandler.updateAngles(0.0f, 1.1f, 7.5f, -20.0f);
                Thread.Sleep(500);
                
            }
            Console.WriteLine("Angle Calculation shutted down");
        }



        public void RequestStop()
        {
            _shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;

    
    }
}
