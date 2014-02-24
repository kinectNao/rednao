using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectNao.Kinect
{

    /**
     * Beschreibt die Methoden zum Erhalt der Skelett Winkel
     */
    public interface ISkeletonAngles
    {
        void updateArmAngles(float r_shoulderPitch, float r_shoulderRoll, float r_ellbowRoll, float r_ellbowYaw, float l_shoulderPitch, float l_shoulderRoll, float l_ellbowRoll, float l_ellbowYaw);
    }
	
}
