using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationDegrees = 20;

    public void RotateLeft()
    {
        //Debug.Log("Rotate Left");
        transform.RotateAround(CalculateCenter(), Vector3.up, rotationDegrees);
    }
    public void RotateRight()
    {
        //Debug.Log("Rotate Right");
        transform.RotateAround(CalculateCenter(), Vector3.up, -rotationDegrees);

    }


    //Calculate the center by averaging all the gameobjects positions.
    private Vector3 CalculateCenter()
    {
        Vector3 center = Vector3.zero;

        List<Vector3> positions = new List<Vector3>();

        foreach (Transform child in transform)
        {
            positions.Add(child.position);
        }

        return GetMeanVector(positions);
    }

    private Vector3 GetMeanVector(List<Vector3> positions)
    {
        if (positions.Count == 0)
            return Vector3.zero;
        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }
        return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
    }

}
