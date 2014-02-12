using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaoControl
{
    /**
     * Beschreibt die Methoden zum Erhalt der Skelett Winkel
     */
    public interface ISkeletonAngles
    {
        void updateAngles(float shoulderPitch, float shoulderRoll, float ellbowRoll, float ellbowYaw);
    }
}
