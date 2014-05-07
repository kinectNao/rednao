using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using Microsoft.Kinect;
using KinectNao.Nao;


namespace KinectNao.Kinect
{
 
       public class SkeletonAngleHandler : ISkeletonAngles
    {

        private SkeletonAngleCalculator angleCalculator;
        private NaoHandler naoHandler;

        private Thread angleCalculatorThread;
        private AngleView view_left;
        private AngleView view_right;
        private ArrayList angleSubscribers = new ArrayList();

        public SkeletonAngleHandler(String naoIP)
        {
           
            view_left = new AngleView(this, 'l');
            view_left.Text = "LEFT";
           

            view_right = new AngleView(this, 'r');
            view_right.Text = "RIGHT";


            view_left.Show();
            view_right.Show();


            naoHandler = new NaoHandler(this, naoIP);

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

            naoHandler.endNao();
        }

        public void addMeToAngleSubscriber(ISkeletonAngles subscriber)
        {
            angleSubscribers.Add(subscriber);
        }

        public void removeMeFromAngleSubscriber(ISkeletonAngles subscriber)
        {
            angleSubscribers.Remove(subscriber);
        }

        public void updateSkeleton(Skeleton skeleton)
        {
            angleCalculator.updateSkeletonData(skeleton);
        }

        public void updateAngles(float r_shoulderPitch, float r_shoulderRoll, float r_ellbowRoll, float r_ellbowYaw, float l_shoulderPitch, float l_shoulderRoll, float l_ellbowRoll, float l_ellbowYaw)
        {

            //For all Subscribers, NAO & GUI
            foreach (ISkeletonAngles currentSubscriber in angleSubscribers)
            {
                currentSubscriber.updateAngles(r_shoulderPitch, r_shoulderRoll, r_ellbowRoll, r_ellbowYaw, l_shoulderPitch, l_shoulderRoll, l_ellbowRoll, l_ellbowYaw);
            }
        }
    }
}
