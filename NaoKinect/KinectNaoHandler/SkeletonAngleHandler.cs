using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KinectNaoHandler
{
    public class SkeletonAngleHandler
    {

        private SkeletonAngleCalculator angleCalculator;
        private Thread angleCalculatorThread;
        public SkeletonAngleHandler()
        {
            
            angleCalculator = new SkeletonAngleCalculator();
            angleCalculatorThread = new Thread(angleCalculator.DoWork);

            // Start the worker thread.
            angleCalculatorThread.Start();
            Console.WriteLine("Winkelberechnung gestartet!");

            // Loop until worker thread activates.
            //while (!angleCalculatorThread.IsAlive);           
            // Put the main thread to sleep for 1 millisecond to
            // allow the worker thread to do some work:
            //Thread.Sleep(1);      
        }

        public void stopCalculation()
        {
            // Request that the worker thread stop itself:
            angleCalculator.RequestStop();

            // Use the Join method to block the current thread 
            // until the object's thread terminates.
            angleCalculatorThread.Join();
            Console.WriteLine("Angle Calculation shutdown complete...");
        }



    }
}
