using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifySize : MonoBehaviour
{
    [Range(1,100)]
    public int percentSize = 10;

    private float originalX;
    private float originalY;
    private float originalZ;

    public void Enlarge()
    {
        originalX = gameObject.transform.localScale.x;
        originalY = gameObject.transform.localScale.y;
        originalZ = gameObject.transform.localScale.z;

        float x = originalX;
        float y = originalY;
        float z = originalZ;

        x += x * percentSize / 100;
        y += y * percentSize / 100;
        z += z * percentSize / 100;

        //Debug.Log(x+" _ "+ y+" _ " +z);

        gameObject.transform.localScale = new Vector3(x, y, z);
    }

    public void Shrink()
    {
        originalX = gameObject.transform.localScale.x;
        originalY = gameObject.transform.localScale.y;
        originalZ = gameObject.transform.localScale.z;

        float x = originalX;
        float y = originalY;
        float z = originalZ;

        x -= x * percentSize / 100;
        y -= y * percentSize / 100;
        z -= z * percentSize / 100;

        //Debug.Log(x + " _ " + y + " _ " + z);

        gameObject.transform.localScale = new Vector3(x, y, z);
    }


    //Saftye should be added so this cant be called b4 any other method.
    public void ReturnToOriginalSize()
    {
        gameObject.transform.localScale = new Vector3(originalX, originalY, originalZ);
    }
}
