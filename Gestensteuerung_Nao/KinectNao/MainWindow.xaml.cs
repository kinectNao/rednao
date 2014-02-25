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
using KinectNao.Kinect;
using KinectNao.Nao;
using System.ComponentModel;
using System.Threading;



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

        Skeleton[] skeletons;
        KinectSensor mySensor;
        WriteableBitmap myBitmap;
        byte[] myPixelData;



        //Control-Aufgaben
        SkeletonAngleHandler anglehandler = new SkeletonAngleHandler();



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
                //Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);

                //Bild und Skelettstream aktivieren
                mySensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                mySensor.SkeletonStream.Enable();


                myPixelData = new byte[this.mySensor.ColorStream.FramePixelDataLength];
                myBitmap = new WriteableBitmap(this.mySensor.ColorStream.FrameWidth, this.mySensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                videoImage.Source = myBitmap;
                skeletons = new Skeleton[this.mySensor.SkeletonStream.FrameSkeletonArrayLength];


                //Event für Videobild registrieren
                //mySensor.ColorFrameReady += this.runtime_VideoFrameReady;
                //mySensor.SkeletonFrameReady += this.runtime_SkeletonFrameReady;
                mySensor.AllFramesReady += runtime_AllFramesReady;
               
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

        private void runtime_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            bool receivedData = false;


            using (ColorImageFrame c = e.OpenColorImageFrame())
            {
                if (c == null) return;

                using (SkeletonFrame sFrame = e.OpenSkeletonFrame())
                {

                    if (sFrame  == null)
                    {
                        // The image processing took too long. More than 2 frames behind.
                        return;
                    }
                    else
                    {
                        skeletons = new Skeleton[sFrame.SkeletonArrayLength];
                        sFrame.CopySkeletonDataTo(skeletons);
                        receivedData = true;
                    }
                    if (receivedData)
                    {

                        Skeleton currentSkeleton = (from s in skeletons
                                                    where s.TrackingState == SkeletonTrackingState.Tracked
                                                    select s).FirstOrDefault();

                        if (currentSkeleton != null)
                        {
                            //Berechnungen einfügen
                            anglehandler.updateSkeleton(currentSkeleton);
                        }
                    }



                    c.CopyPixelDataTo(myPixelData);
                    sFrame.CopySkeletonDataTo(skeletons);

                    BitmapSource bs = BitmapSource.Create(640, 480, 96, 96, PixelFormats.Bgr32, null, myPixelData, 640 * 4);
                    DrawingVisual drawingVisual = new DrawingVisual();
                    DrawingContext drawingContext = drawingVisual.RenderOpen();
                    drawingContext.DrawImage(bs, new Rect(0, 0, 640, 480));

                    //Rendern
                    Pen armPen = new System.Windows.Media.Pen(new
                          SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0)), 2);
                    Pen legPen = new System.Windows.Media.Pen(new
                          SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255)), 2);
                    Pen spinePen = new System.Windows.Media.Pen(new
                          SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0)), 2);


                        foreach (Skeleton aSkeleton in skeletons)
                        {
                            DrawBone(aSkeleton.Joints[JointType.HandLeft],
      aSkeleton.Joints[JointType.WristLeft], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.WristLeft],
                                  aSkeleton.Joints[JointType.ElbowLeft], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.ElbowLeft],
                                  aSkeleton.Joints[JointType.ShoulderLeft], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.ShoulderLeft],
                                  aSkeleton.Joints[JointType.ShoulderCenter], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.HandRight],
                                  aSkeleton.Joints[JointType.WristRight], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.WristRight],
                                  aSkeleton.Joints[JointType.ElbowRight], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.ElbowRight],
                                  aSkeleton.Joints[JointType.ShoulderRight], armPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.ShoulderRight],
                                  aSkeleton.Joints[JointType.ShoulderCenter], armPen, drawingContext);

                            DrawBone(aSkeleton.Joints[JointType.HipCenter],
                                  aSkeleton.Joints[JointType.HipLeft], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.HipLeft],
                                  aSkeleton.Joints[JointType.KneeLeft], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.KneeLeft],
                                  aSkeleton.Joints[JointType.AnkleLeft], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.AnkleLeft],
                                  aSkeleton.Joints[JointType.FootLeft], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.HipCenter],
                                  aSkeleton.Joints[JointType.HipRight], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.HipRight],
                                  aSkeleton.Joints[JointType.KneeRight], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.KneeRight],
                                  aSkeleton.Joints[JointType.AnkleRight], legPen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.AnkleRight],
                                  aSkeleton.Joints[JointType.FootRight], legPen, drawingContext);

                            DrawBone(aSkeleton.Joints[JointType.Head],
                                  aSkeleton.Joints[JointType.ShoulderCenter], spinePen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.ShoulderCenter],
                                  aSkeleton.Joints[JointType.Spine], spinePen, drawingContext);
                            DrawBone(aSkeleton.Joints[JointType.Spine],
                                  aSkeleton.Joints[JointType.HipCenter], spinePen, drawingContext);
                        }

                    drawingContext.Close();
                    RenderTargetBitmap myTarget = new RenderTargetBitmap(640, 480, 96, 96, PixelFormats.Pbgra32);
                    myTarget.Render(drawingVisual);
                    videoImage.Source = myTarget;


                    


                }

            }
            
            /*olorImageFrame c = e.OpenColorImageFrame();
            SkeletonFrame s = e.OpenSkeletonFrame();

            if (c == null || s == null) return;*/

           
        }


        private void DrawBone(Joint jointFrom, Joint jointTo, Pen aPen, DrawingContext aContext)
        {

            if (jointFrom.TrackingState == JointTrackingState.NotTracked ||
                     jointTo.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            if (jointFrom.TrackingState == JointTrackingState.Inferred ||
            jointTo.TrackingState == JointTrackingState.Inferred)
            {
                ColorImagePoint p1 =
                  mySensor.CoordinateMapper.MapSkeletonPointToColorPoint
                    (jointFrom.Position, ColorImageFormat.RgbResolution640x480Fps30);
                ColorImagePoint p2 =
                  mySensor.CoordinateMapper.MapSkeletonPointToColorPoint
                    (jointTo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                //Thin line
                aPen.DashStyle = DashStyles.Dash;
                aContext.DrawLine(aPen, new Point(p1.X, p1.Y),
                  new Point(p2.X, p2.Y));

            }
            if (jointFrom.TrackingState == JointTrackingState.Tracked ||
            jointTo.TrackingState == JointTrackingState.Tracked)
            {
                ColorImagePoint p1 =
                  mySensor.CoordinateMapper.MapSkeletonPointToColorPoint
                     (jointFrom.Position, ColorImageFormat.RgbResolution640x480Fps30);
                ColorImagePoint p2 =
                  mySensor.CoordinateMapper.MapSkeletonPointToColorPoint
                     (jointTo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                //Thick line
                aPen.DashStyle = DashStyles.Solid;
                aContext.DrawLine(aPen, new Point(p1.X, p1.Y),
                  new Point(p2.X, p2.Y));
            }

        }


        
        //Fenster geschlossen
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("Unloading Programm...");

            //anglehandler.stopCalculation();

            if (null != mySensor)
            {
                mySensor.Stop(); 
            }

           
            Console.WriteLine("Ende");
        

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
                    anglehandler.updateSkeleton(currentSkeleton);
                    
                }
            }
        }





        [System.STAThread()]
        public static void Main()
        {
            Console.WriteLine("###############################################################");
            Console.WriteLine("######### Starte Teleworking-Applikation: Nao-Kinect ##########");
            Console.WriteLine("###############################################################");




            new Application().Run(new MainWindow());
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("Unloading Programm...");


            KinectShutdown shutdown = new KinectShutdown(mySensor);
            Thread shutalldown = new Thread(shutdown.DoWork);
            shutalldown.Start();

            anglehandler.stopCalculation();
               

            Console.WriteLine("Ende");
            Environment.Exit(0);
        }

    }
}
