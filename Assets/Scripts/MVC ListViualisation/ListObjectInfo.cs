using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ListObjectInfo
{
    public string ObjectName;   //i.e. file name
    public string ObjectType;   //i.e file type
    public string ObjectInfo;   //i.e. path of file.
    public GameObject IconModel;//3D model to represent this object.

    public ListObjectInfo(string objectName, string objectType, string objectInfo, GameObject iconModel)
    {
        this.ObjectName = objectName;
        this.ObjectType = objectType;
        this.ObjectInfo = objectInfo;
        this.IconModel = iconModel;
    }

    public ListObjectInfo(string objectName, string objectType) : this(objectName, objectType, "No Info", null) { }

    public ListObjectInfo(string objectName, string objectType, string objectInfo) : this(objectName, objectType, objectInfo, null) { }

}
