using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.IO;

namespace KinectNao
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    ///     Verwaltet Kinectobjekt
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void PixelData();
        public delegate void SkeletonData();

        KinectSensor mySensor;
        WriteableBitmap myBitmap;
        byte[] myPixelData;
    
        Skeleton[] skeletons;

        public MainWindow()
        {
            Console.WriteLine("Starte MainWindow");
            InitializeComponent();

            //Warte bis Kinect-Sensor verbunden
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                //Sensorreferenz erstellen
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    mySensor = potentialSensor;
                    break;
                }
            }

            //Wenn Sensorreferenz vorhanden
            if (null != mySensor)
            {
                //Window Handler hinzufügen
                this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);

                //Bild und Skelettstream aktivieren
                mySensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                mySensor.SkeletonStream.Enable();


                myPixelData = new byte[this.mySensor.ColorStream.FramePixelDataLength];
                myBitmap = new WriteableBitmap(this.mySensor.ColorStream.FrameWidth, this.mySensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                videoImage.Source = myBitmap;
                
                //Event für Videobild registrieren
                mySensor.ColorFrameReady += this.runtime_VideoFrameReady;
                mySensor.SkeletonFrameReady += this.runtime_SkeletonFrameReady;
               
                try
                {
                    this.mySensor.Start();
                }
                catch (IOException)
                {
                    this.mySensor = null;
                }
            }
        }

        
        //Fenster geschlossen
        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (null != mySensor)
            {
                mySensor.Stop();
            }
        }

 
        void runtime_VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (null != colorFrame)
                {
                    colorFrame.CopyPixelDataTo(myPixelData);
                    if (null != Application.Current)
                    {
                         Application.Current.Dispatcher.BeginInvoke((PixelData)delegate
                         {
                             myBitmap.WritePixels(new Int32Rect(0, 0, myBitmap.PixelWidth, myBitmap.PixelHeight), myPixelData, myBitmap.PixelWidth * sizeof(int), 0);
                         });
                    }
                   
                   
                }
            }
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
                    //Berechnungen einfügen
                    //anglehandler.updateSkeleton(currentSkeleton);
                    if (null != Application.Current)
                    {
                        Application.Current.Dispatcher.BeginInvoke((PixelData)delegate
                        {
                            SetEllipsePosition(head, currentSkeleton.Joints[JointType.Head]);
                            SetEllipsePosition(leftHand, currentSkeleton.Joints[JointType.HandLeft]);
                            SetEllipsePosition(rightHand, currentSkeleton.Joints[JointType.HandRight]);
                        });
                    }
                }
            }
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




        [System.STAThread()]
        public static void Main()
        {
            Console.WriteLine("###############################################################");
            Console.WriteLine("######### Starte Teleworking-Applikation: Nao-Kinect ##########");
            Console.WriteLine("###############################################################");

            new Application().Run(new MainWindow());
        }

    }
}
