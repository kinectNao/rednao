using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KinectNao;
using KinectNao.Kinect;


namespace FilterTest.cs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void addCurrentArmPos()
        {
            Filter filter = new Filter();
            ArmAngles[] armAngle = new ArmAngles[3];

            armAngle[0] = new ArmAngles(1.5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[1] = new ArmAngles(1.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[2] = new ArmAngles(10.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);

            filter.addCurrentArmPos(armAngle[0]);
            Assert.AreEqual(armAngle[0].shoulderPitch_Right, filter.armAngle[2].shoulderPitch_Right);
            Assert.AreEqual(null, filter.armAngle[1]);
            Assert.AreEqual(null, filter.armAngle[0]);


            filter.addCurrentArmPos(armAngle[1]);
            Assert.AreEqual(armAngle[0].shoulderPitch_Right, filter.armAngle[1].shoulderPitch_Right);
            Assert.AreEqual(armAngle[1].shoulderPitch_Right, filter.armAngle[2].shoulderPitch_Right);
            Assert.AreEqual(null, filter.armAngle[0]);

            filter.addCurrentArmPos(armAngle[2]);
            Assert.AreEqual(armAngle[0].shoulderPitch_Right, filter.armAngle[0].shoulderPitch_Right);
            Assert.AreEqual(armAngle[1].shoulderPitch_Right, filter.armAngle[1].shoulderPitch_Right);
            Assert.AreEqual(armAngle[2].shoulderPitch_Right, filter.armAngle[2].shoulderPitch_Right);
       
        }


        [TestMethod]
        public void TestMedian()
        {
            Filter filter = new Filter();
            ArmAngles[] armAngle = new ArmAngles[3];

            armAngle[0] = new ArmAngles(1.5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[1] = new ArmAngles(1.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[2] = new ArmAngles(10.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);

            filter.addCurrentArmPos(armAngle[0]);
            filter.addCurrentArmPos(armAngle[1]);
            filter.addCurrentArmPos(armAngle[2]);

            ArmAngles median = filter.getArmValue();


            Assert.AreEqual(1.5f, median.shoulderPitch_Right);
        }


        [TestMethod]
        public void TestMedianWithNull()
        {
            Filter filter = new Filter();
            ArmAngles[] armAngle = new ArmAngles[3];

            armAngle[0] = new ArmAngles(1.5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[1] = new ArmAngles(1.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);

            filter.addCurrentArmPos(armAngle[0]);
            filter.addCurrentArmPos(armAngle[1]);


            ArmAngles median = filter.getArmValue();


            Assert.AreEqual(1.0f, median.shoulderPitch_Right);
        }


        [TestMethod]
        public void TestshiftAngleArray()
        {

            Filter filter = new Filter();
            ArmAngles[] armAngle = new ArmAngles[3];

            armAngle[0] = new ArmAngles(1.5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[1] = new ArmAngles(1.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            armAngle[2] = new ArmAngles(10.0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);

            filter.armAngle = armAngle;

            ArmAngles[] shifted = filter.shiftAngleArray();


            Assert.AreEqual(1.0f, shifted[0].shoulderPitch_Right);
            Assert.AreEqual(10.0f, shifted[1].shoulderPitch_Right);
            Assert.AreEqual(null, shifted[2]);
        }




        [TestMethod]
        public void MedianTest()
        {
            float[] angles = { -1.5f, 5f,  40f};
            float result = Filter.GetMedian(angles);

            Assert.AreEqual(5f, result);


            angles[0] = -1.5f;
            angles[1] = -1.51f;
            angles[2] = -1.3f;

            result = Filter.GetMedian(angles);

            Assert.AreEqual(-1.5f, result);
        }



    }
}
