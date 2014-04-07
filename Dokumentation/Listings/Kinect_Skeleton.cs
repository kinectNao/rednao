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
		Skeleton currentSkeleton =
		 ( from s in skeletons
                   where s.TrackingState == SkeletonTrackingState.Tracked
                   select s).FirstOrDefault();

  	if (currentSkeleton != null)
        {
           //Handler mit neuen Skelettdaten versorgen
           anglehandler.updateSkeleton(currentSkeleton);
        }
    }
}
