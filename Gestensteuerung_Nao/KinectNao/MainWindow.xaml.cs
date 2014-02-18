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
        public delegate void UserSkeleton();

        KinectSensor mySensor;
        WriteableBitmap myBitmap;
        byte[] myColorArray;
        Skeleton[] mySkeletonArray;



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


                myColorArray = new byte[this.mySensor.ColorStream.FramePixelDataLength];
                myBitmap = new WriteableBitmap(this.mySensor.ColorStream.FrameWidth, this.mySensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                mySkeletonArray = new Skeleton[this.mySensor.SkeletonStream.FrameSkeletonArrayLength];

                videoImage.Source = myBitmap;
                
                //Event für Videobild registrieren
                mySensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(mySensorAllFramesReady);
               
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

        private void mySensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            ColorImageFrame c = e.OpenColorImageFrame();
           SkeletonFrame s = e.OpenSkeletonFrame();

           

           

                        if (null == c || null == s) return;

                        c.CopyPixelDataTo(myColorArray);
                        s.CopySkeletonDataTo(mySkeletonArray);

                        BitmapSource bs = BitmapSource.Create(640, 480, 96, 96, PixelFormats.Bgr32, null, myColorArray, 640 * 4);
                        DrawingVisual drawingVisual = new DrawingVisual();
                        DrawingContext drawingContent = drawingVisual.RenderOpen();
                        drawingContent.DrawImage(bs, new Rect(0, 0, 640, 480));

                        //Knochen zeichnen

                        drawingContent.Close();
                        RenderTargetBitmap myTarget = new RenderTargetBitmap(640, 480, 96, 96, PixelFormats.Pbgra32);
                        myTarget.Render(drawingVisual);
           
                         if (null != Application.Current)
                                {
                                    Application.Current.Dispatcher.BeginInvoke((UserSkeleton)delegate
                                    {
                                        videoImage.Source = myTarget;
                                    });
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
