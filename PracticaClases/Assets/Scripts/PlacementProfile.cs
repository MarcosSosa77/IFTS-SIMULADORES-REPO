using UnityEngine;
public struct PlacementProfile
{
    public Vector3 extraOffset;
    public Quaternion rotationOffset;
    public float forwardOffset;

    public PlacementProfile(Vector3 offset, Quaternion rot, float forward)
    {
        extraOffset = offset;
        rotationOffset = rot;
        forwardOffset = forward;
    }
}
