using Microsoft.Kinect;


static void Main(string[] args)
{
// instantiate the sensor instance
KinectSensor sensor = KinectSensor.KinectSensors[0];

// initialize the cameras
sensor.DepthStream.Enable();
sensor.DepthFrameReady += sensor_DepthFrameReady;

// make it look like The Matrix
Console.ForegroundColor = ConsoleColor.Green;

// start the data streaming
sensor.Start();
while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }
}

static void sensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
{
	using (var depthFrame = e.OpenDepthImageFrame())
	{
		if (depthFrame == null)
		return;

		short[] bits = new short[depthFrame.PixelDataLength];
		depthFrame.CopyPixelDataTo(bits);
		foreach (var bit in bits)
			Console.Write(bit);
	}
}
