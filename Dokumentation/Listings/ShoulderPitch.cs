public static float getShoulderPitch_Right(Skeleton skeleton)
{
    // Get fitting joints
    Joint hip_j = skeleton.Joints[JointType.HipRight];
    Joint shoulder_j = skeleton.Joints[JointType.ShoulderRight];
    Joint elbow_j = skeleton.Joints[JointType.ElbowRight];


    //Vector of Joints
    Vector3D hip = new Vector3D(hip_j.Position.X, hip_j.Position.Y, hip_j.Position.Z);
    Vector3D shoulder = new Vector3D(shoulder_j.Position.X, shoulder_j.Position.Y, shoulder_j.Position.Z);
    Vector3D elbow = new Vector3D(elbow_j.Position.X, elbow_j.Position.Y, elbow_j.Position.Z);


    // Vektorberechnung
    return AngleCalculation.getAngle(hip, shoulder, elbow);
}