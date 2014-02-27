using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Threading;

namespace KinectNao.Kinect
{
    class SkeletonAngleCalculator
    {
        private SkeletonAngleHandler skeletonAngleHandler;
        private Skeleton currentSkeleton;
        private Filter filter = new Filter();


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
            float shoulderPitch_Right = 0.0f;
            float shoulderRoll_Right = 0.0f;
            float elbowRoll_Right = 0.0f;
            float elbowYaw_Right = 0.0f;
			
			float shoulderPitch_Left = 0.0f;
            float shoulderRoll_Left = 0.0f;
            float elbowRoll_Left = 0.0f;
            float elbowYaw_Left = 0.0f;

            while (!_shouldStop)
            {

                if (currentSkeleton == null)
                {
                    continue;
                }
                //Hier werden Berechnungen mit CURRENTSKELETON (EVTL PUFFER!!!) getätigt
                //BERECHNE WINKEL a-d
                //updateAngles
                //Sleep
                //Zuverlässigkeit der Joints
                
				//Right
				shoulderPitch_Right = AngleCalculation.getShoulderPitch_Right(currentSkeleton);
                shoulderRoll_Right = AngleCalculation.getShoulderRoll_Right(currentSkeleton);
                elbowRoll_Right = AngleCalculation.getElbowRoll_Right(currentSkeleton);
                elbowYaw_Right = AngleCalculation.getElbowYaw_Right(currentSkeleton);
				
				//Left
				shoulderPitch_Left = AngleCalculation.getShoulderPitch_Left(currentSkeleton);
                shoulderRoll_Left = AngleCalculation.getShoulderRoll_Left(currentSkeleton);
                elbowRoll_Left = AngleCalculation.getElbowRoll_Left(currentSkeleton);
                elbowYaw_Left = AngleCalculation.getElbowYaw_Left(currentSkeleton);

                skeletonAngleHandler.updateAngles(shoulderPitch_Right, shoulderRoll_Right, elbowRoll_Right, elbowYaw_Right, shoulderPitch_Left, shoulderRoll_Left, elbowRoll_Left, elbowYaw_Left);
                Thread.Sleep(30);

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
