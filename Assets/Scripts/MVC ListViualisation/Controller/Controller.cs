using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ------Class Description--------------------------
 * 
 * This is the main script of the 'Controller'
 * structure of the Model View Controller.
 * 
 * This script can directly referance both
 * the 'Model' Structure and the 'View' structure.
 * 
 * This script will also listen for events from
 * the 'Model' and the 'View' and act on them.
 * -------------------------------------------------
 */

 /// <summary>
 /// This script will:
 /// <para/>Handle communication between the 'Model' & 'View'.
 /// </summary>
public class Controller : MonoBehaviour
{


    public View viewReferance;          //Referance to the View script.
    public Model modelReferance;        //Referance to the Model script.

    public bool buildListOnAwake;       //Build list on awake.


    private void Start()
    {
        if (buildListOnAwake) modelReferance.BuildList();
    }

    #region Event Subscriptions
    private void OnEnable()
    {
        modelReferance.onListReady += SetListView;
        viewReferance.onSelected += OnItemSelected;
        viewReferance.onHovered += OnItemHovered;
        viewReferance.onUnHovered += OnItemUnHovered;

    }
    private void OnDisable()
    {
        modelReferance.onListReady -= SetListView;
        viewReferance.onSelected -= OnItemSelected;
        viewReferance.onHovered -= OnItemHovered;
        viewReferance.onUnHovered -= OnItemUnHovered;


    }
    #endregion


    

    public void PreviousList()
    {
        modelReferance.PreviousList();
        viewReferance.ClearItemInfo();
    }

    private void OnItemSelected(ListObjectInfo item)
    {
        //Debug.Log("Name: "+item.ObjectName+"    Type: "+item.ObjectType+"   Info: "+item.ObjectInfo);
        modelReferance.ItemSelected(item);
        viewReferance.DisplayItemInfo(item, true);
    }
    private void OnItemHovered(ListObjectInfo item)
    {
        //Debug.Log("Name: " + item.ObjectName + "    Type: " + item.ObjectType + "   Info: " + item.ObjectInfo);
        viewReferance.DisplayItemInfo(item);

    }
    private void OnItemUnHovered(ListObjectInfo item)
    {
        //Debug.Log("Name: " + item.ObjectName + "    Type: " + item.ObjectType + "   Info: " + item.ObjectInfo);
        viewReferance.ClearItemInfo();
    }

    //This is called once the controller hears the list is ready
    //from the model. Then it gets the list from the model.
    //and passes it of into the view.
    private void SetListView(List<ListObjectInfo> passedList)
    {
        viewReferance.ClearList();
        viewReferance.InitializeList(passedList);
    }

}
