using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Kinect
{
    public class Filter
    {
        // Queue fuer Mittelwertfilter
        private static int filtersize = 3;
        public  ArmAngles[] armAngle = new ArmAngles[filtersize];


        public Filter()
        {
            for (int i = 0; i < filtersize; i++)
            {
                armAngle[i] = new ArmAngles(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            }
        }



        //Neue Armposition hinzufuegen, alte verwerfen
        public void addCurrentArmPos(ArmAngles currentAnglePos){

            this.armAngle = shiftAngleArray();
            armAngle[filtersize - 1] = currentAnglePos;
           
        }


        //Shift Array right -> neues Element ist null
        public ArmAngles[] shiftAngleArray()
        {
            ArmAngles[] newAr = new ArmAngles[armAngle.Length];
            for (int i = 1; i < armAngle.Length; i++)
                newAr[i - 1] = armAngle[i];

            return newAr;
        }



        public static float GetMedian(float[] sourceNumbers)
        {
            //Framework 2.0 version of this method. there is an easier way in F4        
            if (sourceNumbers == null || sourceNumbers.Length == 0)
                throw new Exception();

            //make sure the list is sorted, but use a new array
            float[] sortedPNumbers = (float[])sourceNumbers.Clone();
            sourceNumbers.CopyTo(sortedPNumbers, 0);
            Array.Sort(sortedPNumbers);

            //get the median
            int size = sortedPNumbers.Length;
            int mid = size / 2;
            float median = (size % 2 != 0) ? (float)sortedPNumbers[mid] : ((float)sortedPNumbers[mid] + (float)sortedPNumbers[mid - 1]) / 2;
            return median;
        }



        public ArmAngles getArmValue()
        {
            int x = 0;
            float[] elbowRoll_Left = new float[filtersize];
            float[] elbowRoll_Right = new float[filtersize];
            float[] elbowYaw_Left = new float[filtersize];
            float[] elbowYaw_Right = new float[filtersize];

            float[] shoulderPitch_Left = new float[filtersize];
            float[] shoulderPitch_Right = new float[filtersize];
            float[] shoulderRoll_Left = new float[filtersize];
            float[] shoulderRoll_Right = new float[filtersize]; 

            // Initialisiere Werte
            for(int i = 0; i<filtersize;i++){
                  elbowRoll_Left[i] = armAngle[i].elbowRoll_Left;
                  elbowRoll_Right[i] = armAngle[i].elbowRoll_Right;
                  elbowYaw_Left[i] = armAngle[i].elbowYaw_Left;
                  elbowYaw_Right[i] = armAngle[i].elbowYaw_Right;

                  shoulderPitch_Left[i] = armAngle[i].shoulderPitch_Left;
                  shoulderPitch_Right[i] = armAngle[i].shoulderPitch_Right;
                  shoulderRoll_Left[i] = armAngle[i].shoulderRoll_Left;
                  shoulderRoll_Right[i] = armAngle[i].shoulderRoll_Right;
            }

            
            return new ArmAngles(
                Filter.GetMedian(shoulderPitch_Right), 
                Filter.GetMedian(shoulderRoll_Right), 
                Filter.GetMedian(elbowRoll_Right), 
                Filter.GetMedian(elbowYaw_Right), 
                Filter.GetMedian(shoulderPitch_Left), 
                Filter.GetMedian(shoulderRoll_Left), 
                Filter.GetMedian(elbowRoll_Left),
                Filter.GetMedian(elbowYaw_Left));


        }

       

        

    }
}
