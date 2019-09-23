using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotate gameobject around its center pivot.
/// </summary>
public class ListMovement : MonoBehaviour
{
    public float rotationDegrees = 20;


    private int maxVerticalMoves = 10;
    private int index;
    private Vector3 originPos;                  //The starting position of the list.
    private Quaternion originRot;               //The starting rotation of the list.


    public void ResetPosition()
    {
        index = 0;
        gameObject.transform.position = originPos;
        gameObject.transform.rotation = originRot;

    }

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



   
    public void MoveUp()
    {

        if (index <= 0) return;

        index--;

        //Debug.Log(index);

        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }
    public void MoveDown()
    {
        if (index >= maxVerticalMoves) return;

        index++;

        //Debug.Log(index);

        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

    }







    private void Start()
    {
        originPos = gameObject.transform.position;
        originRot = gameObject.transform.rotation;
    }

    //Wait to compensate for spawning delay.
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(5);

        float height = CalculateHeight();

        

        if (height > 2.5)
        {
            Debug.Log(CalculateHeight());
            maxVerticalMoves = (int)height;
        }

    }

    //Calculate height by getting highest position child.
    private float CalculateHeight()
    {
        float tallestHeight = 0;

        foreach (Transform child in transform)
        {
            float height = child.localPosition.y;
            //Debug.Log(child.gameObject.name + "  Height:" + height);
            if (height > tallestHeight) tallestHeight = height;
        }

        return tallestHeight;
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
