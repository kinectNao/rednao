using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Kinect;

namespace KinectNaoHandler
{
    class SkeletonAngleCalculator
    {
        private SkeletonAngleHandler skeletonAngleHandler;
        private Skeleton currentSkeleton;

        public SkeletonAngleCalculator(SkeletonAngleHandler skeletonAngleHandler)
        {
            this.skeletonAngleHandler = skeletonAngleHandler;
        }

        public void updateSkeletonData(Skeleton skeleton)
        {
            this.currentSkeleton = skeleton;
        }


        
        // This method will be called when the thread is started.
        public void DoWork()   
        {
            float shoulderPitch_Rigt = 0.0f;
            float shoulderRoll_Right = 0.0f;
            float elbowRoll_Right = 0.0f;
            float elbowYaw_Right = 0.0f;

            while (!_shouldStop)
            {
                //Hier werden Berechnungen mit CURRENTSKELETON (EVTL PUFFER!!!) getätigt
                //BERECHNE WINKEL a-d
                //updateAngles
                //Sleep
                shoulderPitch_Rigt = AngleCalculation.getShoulderPitch_Right(currentSkeleton);
                shoulderRoll_Right = AngleCalculation.getShoulderRoll_Right(currentSkeleton);
                elbowRoll_Right = AngleCalculation.getElbowRoll_Right(currentSkeleton);
                elbowYaw_Right = AngleCalculation.getElbowYaw_Right(currentSkeleton);

                skeletonAngleHandler.updateAngles(shoulderPitch_Rigt, shoulderRoll_Right, elbowRoll_Right, elbowYaw_Right);
                Thread.Sleep(500);
                skeletonAngleHandler.updateAngles(0.0f, 0.0f, 0.0f, 0.0f);
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
