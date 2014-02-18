using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Media.Media3D;

namespace KinectNao.Kinect
{
    static class AngleCalculation
    {


        public static float getShoulderPitch_Right(Skeleton skeleton)
        {
            // Get fitting joints
            Joint hip_j = skeleton.Joints[JointType.HipRight];
            Joint shoulder_j = skeleton.Joints[JointType.ShoulderRight];
            Joint elbow_j = skeleton.Joints[JointType.ElbowRight];


            //Vector of Joints
            Vector3D hip = new Vector3D(hip_j.Position.X, hip_j.Position.Y, hip_j.Position.Z);
            Vector3D shoulder = new Vector3D(shoulder_j.Position.X, shoulder_j.Position.Y, shoulder_j.Position.Z);
            Vector3D elbow = new Vector3D(elbow_j.Position.X, elbow_j.Position.Y, elbow_j.Position.Z);


            // Vektorberechnung
            return AngleCalculation.getAngle(hip, shoulder, elbow);
        }

        public static float getShoulderRoll_Right(Skeleton skeleton)
        {
            // Get fitting joints
            Joint hip_right_j = skeleton.Joints[JointType.HipRight];
            Joint hip__left_j = skeleton.Joints[JointType.HipLeft];
            Joint shoulder_j = skeleton.Joints[JointType.ShoulderRight];
            Joint elbow_j = skeleton.Joints[JointType.ElbowRight];

            //Vector of Joints
            Vector3D hip_right = new Vector3D(hip_right_j.Position.X, hip_right_j.Position.Y, hip_right_j.Position.Z);
            Vector3D hip_left = new Vector3D(hip__left_j.Position.X, hip__left_j.Position.Y, hip__left_j.Position.Z);
            Vector3D shoulder = new Vector3D(shoulder_j.Position.X, shoulder_j.Position.Y, shoulder_j.Position.Z);
            Vector3D elbow = new Vector3D(elbow_j.Position.X, elbow_j.Position.Y, elbow_j.Position.Z);

            // Vektorberechnung
            return AngleCalculation.getAngle(hip_right, hip_left, shoulder, elbow);
        }

        public static float getElbowRoll_Right(Skeleton skeleton)
        {
            // Get fitting joints
            Joint shoulder_j = skeleton.Joints[JointType.ShoulderRight];
            Joint elbow_j = skeleton.Joints[JointType.ElbowRight];
            Joint hand_j = skeleton.Joints[JointType.HandRight];

            //Vector of Joints
            Vector3D shoulder = new Vector3D(shoulder_j.Position.X, shoulder_j.Position.Y, shoulder_j.Position.Z);
            Vector3D elbow = new Vector3D(elbow_j.Position.X, elbow_j.Position.Y, elbow_j.Position.Z);
            Vector3D hand = new Vector3D(hand_j.Position.X, hand_j.Position.Y, hand_j.Position.Z);

            // Vektorberechnung
            return AngleCalculation.getAngle(shoulder, elbow, hand);
        }
        public static float getElbowYaw_Right(Skeleton skeleton)
        {

            // Get fitting joints
            Joint shoulder_j = skeleton.Joints[JointType.ShoulderRight];
            Joint hip_j = skeleton.Joints[JointType.HipRight];
            Joint elbow_j = skeleton.Joints[JointType.ElbowRight];
            Joint hand_j = skeleton.Joints[JointType.HandRight];

            //Vector of Joints
            Vector3D shoulder = new Vector3D(shoulder_j.Position.X, shoulder_j.Position.Y, shoulder_j.Position.Z);
            Vector3D hip = new Vector3D(hip_j.Position.X, hip_j.Position.Y, hip_j.Position.Z);
            Vector3D elbow = new Vector3D(elbow_j.Position.X, elbow_j.Position.Y, elbow_j.Position.Z);
            Vector3D hand = new Vector3D(hand_j.Position.X, hand_j.Position.Y, hand_j.Position.Z);


            // Vektorberechnung
            return AngleCalculation.getAngle(shoulder, hip, elbow, hand);
        }


        //4 Punkte -> 2 unabhängige Knochen
        public static float getAngle(Vector3D a, Vector3D b, Vector3D x, Vector3D y)
        {
            Vector3D bone1 = a - b;
            Vector3D bone2 = x - y;

            bone1.Normalize();
            bone2.Normalize();

            float dotProduct = (float)Vector3D.DotProduct(bone1, bone2);

            return (float)Math.Acos(dotProduct);
        }

        //3 Punkte -> 2 zusammenhängende Knochen
        public static float getAngle(Vector3D a, Vector3D b, Vector3D c)
        {
            Vector3D bone1 = a - b;
            Vector3D bone2 = c - b;

            bone1.Normalize();
            bone2.Normalize();

            float dotProduct = (float)Vector3D.DotProduct(bone1, bone2);

            return (float)Math.Acos(dotProduct);
        }





    }
}
