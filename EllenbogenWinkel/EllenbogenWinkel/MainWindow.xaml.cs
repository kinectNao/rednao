using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Linq;
using System.Windows.Media.Media3D;
using Aldebaran.Proxies;
using System.Collections;

namespace EllenbogenWinkel
{

    public partial class MainWindow : Window
    {


        //Instantiate the Kinect runtime. Required to initialize the device.
        //IMPORTANT NOTE: You can pass the device ID here, in case more than one Kinect device is connected.
        KinectSensor sensor = KinectSensor.KinectSensors[0];
        byte[] pixelData;
        Skeleton[] skeletons;
        //MotionProxy motionProxy = new MotionProxy("192.168.178.44", 9559);
       // RobotPostureProxy pose = new RobotPostureProxy("192.168.178.44", 9559);

        public MainWindow()
        {
            InitializeComponent();
            //pose.goToPosture("StandZero", 0.5f);
           

            //Runtime initialization is handled when the window is opened. When the window
            //is closed, the runtime MUST be unitialized.
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);

            sensor.ColorStream.Enable();
            sensor.SkeletonStream.Enable();
        }

        void runtime_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            bool receivedData = false;

            using (SkeletonFrame SFrame = e.OpenSkeletonFrame())
            {
                if (SFrame == null)
                {
                    // The image processing took too long. More than 2 frames behind.
                }
                else
                {
                    skeletons = new Skeleton[SFrame.SkeletonArrayLength];
                    SFrame.CopySkeletonDataTo(skeletons);
                    receivedData = true;
                }
            }

            if (receivedData)
            {

                Skeleton currentSkeleton = (from s in skeletons
                                            where s.TrackingState == SkeletonTrackingState.Tracked
                                            select s).FirstOrDefault();

                if (currentSkeleton != null)
                {
                    SetEllipsePosition(shoulderR, currentSkeleton.Joints[JointType.ShoulderRight]);
                    SetEllipsePosition(ellbowR, currentSkeleton.Joints[JointType.ElbowRight]);
                    SetEllipsePosition(wristR, currentSkeleton.Joints[JointType.WristRight]);
                    printAngle(currentSkeleton);

                }
            }
        }



        public void printAngle(Skeleton currentSkeleton)
        {
           
            Vector3D a1 = new Vector3D(currentSkeleton.Joints[JointType.ElbowRight].Position.X, currentSkeleton.Joints[JointType.ElbowRight].Position.Y, currentSkeleton.Joints[JointType.ElbowRight].Position.Z);
            Vector3D a2 = new Vector3D(currentSkeleton.Joints[JointType.ShoulderRight].Position.X, currentSkeleton.Joints[JointType.ShoulderRight].Position.Y, currentSkeleton.Joints[JointType.ShoulderRight].Position.Z);
            Vector3D a3 = new Vector3D(currentSkeleton.Joints[JointType.WristRight].Position.X, currentSkeleton.Joints[JointType.WristRight].Position.Y, currentSkeleton.Joints[JointType.WristRight].Position.Z);

            Console.WriteLine("Arm X: " + a3.X);
            Console.WriteLine("Arm Y: " + a3.Y);

            Vector3D b1 = a3 - a1;
            Vector3D b2 = a2 - a1;

            b1.Normalize();
            b2.Normalize();

            double angle = ( 360 / (2*Math.PI) * AngleBetweenTwoVectors(b1, b2));
            String angles = angle.ToString();
            Console.WriteLine("Angle is: " + angles);
            ellbowAngle.Content = angles;


            ArrayList names = new ArrayList();
            ArrayList angleList = new ArrayList();
            names.Add("LElbowRoll");
            angleList.Add(-(float)AngleBetweenTwoVectors(b1, b2));


           // motionProxy.setAngles(names, angleList, (float)0.2f);
            /*
            ArrayList names = new ArrayList();
            ArrayList angleList = new ArrayList();
            names.Add("LElbowRoll");
            angleList.Add((float)AngleBetweenTwoVectors(b1, b2));
            motionproxy.setAngles(names, angleList, 0.1f);*/


       }


        public double AngleBetweenTwoVectors(Vector3D vectorA, Vector3D vectorB)
        {
            double dotProduct = 0.0;
            dotProduct = Vector3D.DotProduct(vectorA, vectorB);

            return Math.Acos(dotProduct);
          

        }







        //This method is used to position the ellipses on the canvas
        //according to correct movements of the tracked joints.

        //IMPORTANT NOTE: Code for vector scaling was imported from the Coding4Fun Kinect Toolkit
        //available here: http://c4fkinect.codeplex.com/
        //I only used this part to avoid adding an extra reference.
        private void SetEllipsePosition(Ellipse ellipse, Joint joint)
        {
            Microsoft.Kinect.SkeletonPoint vector = new Microsoft.Kinect.SkeletonPoint();
            vector.X = ScaleVector(640, joint.Position.X);
            vector.Y = ScaleVector(480, -joint.Position.Y);
            vector.Z = joint.Position.Z;

            Joint updatedJoint = new Joint();
            updatedJoint = joint;
            updatedJoint.TrackingState = JointTrackingState.Tracked;
            updatedJoint.Position = vector;

            Canvas.SetLeft(ellipse, updatedJoint.Position.X);
            Canvas.SetTop(ellipse, updatedJoint.Position.Y);
        }

        private float ScaleVector(int length, float position)
        {
            float value = (((((float)length) / 1f) / 2f) * position) + (length / 2);
            if (value > length)
            {
                return (float)length;
            }
            if (value < 0f)
            {
                return 0f;
            }
            return value;
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            sensor.Stop();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            sensor.SkeletonFrameReady += runtime_SkeletonFrameReady;
            sensor.ColorFrameReady += runtime_VideoFrameReady;
            sensor.Start();
        }

        void runtime_VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            bool receivedData = false;

            using (ColorImageFrame CFrame = e.OpenColorImageFrame())
            {
                if (CFrame == null)
                {
                    // The image processing took too long. More than 2 frames behind.
                }
                else
                {
                    pixelData = new byte[CFrame.PixelDataLength];
                    CFrame.CopyPixelDataTo(pixelData);
                    receivedData = true;
                }
            }

            if (receivedData)
            {
                BitmapSource source = BitmapSource.Create(640, 480, 96, 96,
                        PixelFormats.Bgr32, null, pixelData, 640 * 4);

                videoImage.Source = source;
            }
        }
    }
}
