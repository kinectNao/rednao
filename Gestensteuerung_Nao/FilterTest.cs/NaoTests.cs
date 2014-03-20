using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KinectNao.Nao;
using System.Collections.Generic;
using Aldebaran.Proxies;

namespace FilterTest.cs
{
    [TestClass]
    public class NaoTests
    {
        [TestMethod]
        public void testConvertAngles()
        {

            RArm rArm = new RArm();
            rArm.angles = new Angles();

            rArm.angles.currAngles = new List<float> { Angles.inRadian(90), Angles.inRadian(90), Angles.inRadian(90), Angles.inRadian(90), Angles.inRadian(90) };
            float ninety = Angles.inRadian(90);

            float[] convertedAngles = rArm.convertAngles(new float[] { ninety, ninety, ninety, ninety });


            Assert.AreEqual(convertedAngles[0], 0);
            Assert.AreEqual(convertedAngles[1], 0);
            Assert.AreEqual(convertedAngles[2], 0.9849572f);
            Assert.AreEqual(convertedAngles[3], 0);

        }

        [TestMethod]
        public void testFilterAngles()
        {
            Angles angles = new Angles();
            angles.currAngles = new List<float> { Angles.inRadian(90), Angles.inRadian(90), Angles.inRadian(90), Angles.inRadian(90), Angles.inRadian(90) };

            float[] newAngles = { Angles.inRadian(105), Angles.inRadian(105), Angles.inRadian(95), Angles.inRadian(105), Angles.inRadian(105) };

            float[] filteredAngles = angles.checkDifference(newAngles);

            for (int i = 0; i < filteredAngles.Length; i++)
            {
                Assert.AreEqual(angles.currAngles[i], filteredAngles[i]);  
            }
           

        }



    }
}
