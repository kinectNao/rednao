foreach angle in newAngles
 angle = (differenceOf(newAngle,currAngle) <= evenAngle) ? (currAngle) : (newAngle);
return newAngles;