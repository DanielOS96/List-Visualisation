using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ListObjectInfo
{
    public string ObjectName;   //i.e. file name
    public string ObjectType;   //i.e file type
    public string ObjectInfo;   //i.e. path of file.

    public bool ObjectIsActive;         //Is this current object active.

    public GameObject IconModel;        //3D model to represent this object.
    public Transform ObjectTransform;   //The transfrom this object is associated with.
    public Transform ParentTransform;   //The parent transfrom this object is associated with.
    public Vector3 OriginPos;           //The origin position of this object instance.
    public Quaternion OriginRot;        //The origin rotatino of this object instance.


    public ListObjectInfo(string objectName, string objectType, string objectInfo, bool objectIsActive,
        GameObject iconModel, Transform objectTranform, Transform parentTransform, Vector3 originPos, Quaternion originRot)
    {
        this.ObjectName = objectName;
        this.ObjectType = objectType;
        this.ObjectInfo = objectInfo;

        this.ObjectIsActive = objectIsActive;

        this.IconModel = iconModel;
        this.ObjectTransform = objectTranform;
        this.ParentTransform = parentTransform;
        this.OriginPos = originPos;
        this.OriginRot = originRot;
    }


    public ListObjectInfo(string objectName, string objectType, string objectInfo) 
        : this(objectName, objectType, objectInfo,false, null, null,null, Vector3.zero, new Quaternion(0, 0, 0, 0)) { }

    public ListObjectInfo(string objectName, string objectType, string objectInfo, GameObject iconModel)
        : this(objectName, objectType, objectInfo, false, iconModel, null, null, Vector3.zero, new Quaternion(0, 0, 0, 0)) { }



}
