using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections;
using Microsoft.Kinect;

namespace KinectNaoHandler
{
    public class SkeletonAngleHandler : ISkeletonAngles
    {

        private SkeletonAngleCalculator angleCalculator;
        private Thread angleCalculatorThread;
        private AngleView view;
        private ArrayList angleSubscribers = new ArrayList();

        public SkeletonAngleHandler()
        {
            view = new AngleView(this);
            view.Show();


            angleCalculator = new SkeletonAngleCalculator(this);
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

        public void addMeToAngleSubscriber(ISkeletonAngles subscriber)
        {
            angleSubscribers.Add(subscriber);
        }

        public void removeMeFromAngleSubscriber(ISkeletonAngles subscriber)
        {
            angleSubscribers.Remove(subscriber);
        }

        public void updateAngles(float shoulderPitch, float shoulderRoll, float elbowRoll, float elbowYaw)
        {
            //For all Subscribers, NAO & GUI
            foreach (ISkeletonAngles currentSubscriber in angleSubscribers){
                currentSubscriber.updateAngles(shoulderPitch, shoulderRoll, elbowRoll, elbowYaw);
            }
        }

        public void updateSkeleton(Skeleton skeleton)
        {
            angleCalculator.updateSkeletonData(skeleton);
        }
    }
}
