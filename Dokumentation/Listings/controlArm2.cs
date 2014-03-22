public override void controlArm( float SP, float SR, float ER, float EY, float WY)
{
	float[] newangles = { SP, SR, ER, EY, WY };
	newangles = convertAngles(newangles);   
	newangles = verifyAngles(newangles);    
	newangles = angles.checkDifference(newangles);     

	mp.post.angleInterpolationWithSpeed(joints, newangles, Arm.fractionMaxSpeed);
}