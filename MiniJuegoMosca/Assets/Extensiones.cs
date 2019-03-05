using UnityEngine;
using System.Collections;

public static class Extensiones
{
    public static bool ContienePunto(this BoxCollider box, Vector3 punto)
    {
        
        Vector3 point = box.transform.InverseTransformPoint(punto) - box.center;

        float halfX = (box.size.x * 0.5f);
        float halfY = (box.size.y * 0.5f);
        float halfZ = (box.size.z * 0.5f);

        if (point.x < halfX && point.x > -halfX &&
            point.y < halfY && point.y > -halfY &&
            point.z < halfZ && point.z > -halfZ)
            return true;
        else
            return false;
        
    }


}
