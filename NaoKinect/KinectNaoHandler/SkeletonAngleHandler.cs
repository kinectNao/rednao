using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KinectNaoHandler
{
    public class SkeletonAngleHandler
    {

        public SkeletonAngleHandler()
        {
            Console.WriteLine("IT WORKED!!!!");
            // Create the thread object. This does not start the thread.
            SkeletonAngleCalculator angleCalculator = new SkeletonAngleCalculator();
            Thread angleCalculatorThread = new Thread(angleCalculator.DoWork);

            // Start the worker thread.
            angleCalculatorThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");

            // Loop until worker thread activates.
            while (!angleCalculatorThread.IsAlive) ;

            // Put the main thread to sleep for 1 millisecond to
            // allow the worker thread to do some work:
            Thread.Sleep(1);

            // Request that the worker thread stop itself:
            angleCalculator.RequestStop();

            // Use the Join method to block the current thread 
            // until the object's thread terminates.
            angleCalculatorThread.Join();
            Console.WriteLine("main thread: Worker thread has terminated.");
        }

    }
}
