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
        modelReferance.onInvalidRequest += OnInvalidRequest;
        viewReferance.onSelected += OnItemSelected;
        viewReferance.onHovered += OnItemHovered;
        viewReferance.onUnHovered += OnItemUnHovered;
        viewReferance.onMoved += OnItemMoved;
        viewReferance.onClosed += OnItemClosed;
        viewReferance.onDeleted += OnItemDeleted;
    }
    private void OnDisable()
    {
        modelReferance.onListReady -= SetListView;
        modelReferance.onInvalidRequest -= OnInvalidRequest;
        viewReferance.onSelected -= OnItemSelected;
        viewReferance.onHovered -= OnItemHovered;
        viewReferance.onUnHovered -= OnItemUnHovered;
        viewReferance.onMoved -= OnItemMoved;
        viewReferance.onClosed -= OnItemClosed;
        viewReferance.onDeleted -= OnItemDeleted;
    }
    #endregion



    //Called by a button gameobject.
    public void PreviousList()
    {
        modelReferance.PreviousList();
        viewReferance.ClearItemInfo();
    }


    private void OnItemSelected(ListObjectInfo item)
    {
        modelReferance.ItemSelected(item);
        viewReferance.DisplayItemInfo(item, true);
    }

    private void OnItemMoved(ListObjectInfo item)
    {
        modelReferance.ItemMoved(item);
    }
    private void OnItemHovered(ListObjectInfo item)
    {
        modelReferance.ItemHovered(item);
        viewReferance.DisplayItemInfo(item);
    }
    private void OnItemUnHovered(ListObjectInfo item)
    {
        modelReferance.ItemUnhovered(item);
        viewReferance.ClearItemInfo();
    }
    private void OnItemClosed(ListObjectInfo item)
    {
        modelReferance.BuildList();
    }
    private void OnItemDeleted(ListObjectInfo item)
    {
        modelReferance.ItemDeleted(item);
    }





    private void OnInvalidRequest(string invalidItem = null)
    {
        viewReferance.InvalidRequestMade(invalidItem);
    }
    private void SetListView(List<ListObjectInfo> passedList)
    {
        print("zdfgzdfgzdfg");

        viewReferance.ClearList();
        viewReferance.InitializeList(passedList);
    }

}
