public static float getAngle(Vector3D a, Vector3D b, Vector3D x, Vector3D y)
{
    Vector3D bone1 = a - b;
    Vector3D bone2 = x - y;

    bone1.Normalize();
    bone2.Normalize();

    float dotProduct = (float)Vector3D.DotProduct(bone1, bone2);

    return (float)Math.Acos(dotProduct);
}