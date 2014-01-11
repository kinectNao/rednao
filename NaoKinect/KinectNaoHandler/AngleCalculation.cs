using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Media.Media3D;

namespace KinectNaoHandler
{
    static class AngleCalculation
    {


        public static float getShoulderPitch_Right(Skeleton skeleton)
        {
            return -999.0f;
        }

        public static float getShoulderRoll_Right(Skeleton skeleton)
        {
            return -999.0f;
        }

        public static float getElbowRoll_Right(Skeleton skeleton)
        {
            // Get fitting joints
            Joint shoulder = skeleton.Joints[JointType.ShoulderRight];
            Joint elbow = skeleton.Joints[JointType.ElbowRight];
            Joint hand = skeleton.Joints[JointType.HandRight];
            
            // Vektorisierung
            

            // Vektorberechnung

                                    

            return -999.0f;
        }
        public static float getElbowYaw_Right(Skeleton skeleton)
        {
            return -999.0f;
        }


        //4 Punkte -> 2 unabhängige Knochen
        private float getAngle(Vector3D a, Vector3D b, Vector3D x, Vector3D y);

        //3 Punkte -> 2 zusammenhängende Knochen
        private float getAngle(Vector3D a, Vector3D b, Vector3D c);




    }
}
