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

    public bool listActive;             
    public bool buildListOnAwake;       //Build list on awake.


    private void Start()
    {
        if (buildListOnAwake) modelReferance.BuildList();
    }


    #region Event Subscriptions
    private void OnEnable()
    {
        //Model Events
        modelReferance.onListReady += SetListView;
        modelReferance.onInvalidRequest += OnInvalidRequest;
        //View Item Events
        viewReferance.onSelected += OnItemSelected;
        viewReferance.onHovered += OnItemHovered;
        viewReferance.onUnHovered += OnItemUnHovered;
        viewReferance.onMoved += OnItemMoved;
        viewReferance.onClosed += OnItemClosed;
        viewReferance.onDeleted += OnItemDeleted;
        viewReferance.onGrabbed += OnItemGrabbed;
        //View Control Panel Events
        viewReferance.onPreviousList += PreviousList;
        viewReferance.onLeftRotatateList += RotateListLeft;
        viewReferance.onRightRotatateList += RotateListRight;
        viewReferance.onMoveListUp += MoveListUp;
        viewReferance.onMoveListDown += MoveListDown;
    }
    private void OnDisable()
    {
        //Model Events
        modelReferance.onListReady -= SetListView;
        modelReferance.onInvalidRequest -= OnInvalidRequest;
        //View Item Events
        viewReferance.onSelected -= OnItemSelected;
        viewReferance.onHovered -= OnItemHovered;
        viewReferance.onUnHovered -= OnItemUnHovered;
        viewReferance.onMoved -= OnItemMoved;
        viewReferance.onClosed -= OnItemClosed;
        viewReferance.onDeleted -= OnItemDeleted;
        viewReferance.onGrabbed -= OnItemGrabbed;
        //View Control Panel Events
        viewReferance.onPreviousList -= PreviousList;
        viewReferance.onLeftRotatateList -= RotateListLeft;
        viewReferance.onRightRotatateList -= RotateListRight;
        viewReferance.onMoveListUp -= MoveListUp;
        viewReferance.onMoveListDown -= MoveListDown;
    }
    #endregion



    


    #region Item Event Responses
    private void OnItemSelected(ListObjectInfo objectInfo)
    {
        objectInfo.ObjectIsActive = true;

        modelReferance.ItemSelected(objectInfo);
    }

    private void OnItemMoved(ListObjectInfo objectInfo)
    {
        objectInfo.ObjectIsActive = false;

        modelReferance.ItemMoved(objectInfo);
    }
    private void OnItemHovered(ListObjectInfo objectInfo)
    {
        modelReferance.ItemHovered(objectInfo);
        viewReferance.ClearItemInfo();
        viewReferance.DisplayItemInfo(objectInfo, true);
    }
    private void OnItemUnHovered(ListObjectInfo objectInfo)
    {
        modelReferance.ItemUnhovered(objectInfo);
        
    }
    private void OnItemClosed(ListObjectInfo objectInfo)
    {
        objectInfo.ObjectIsActive = false;


        viewReferance.ReturnItemToOrigin(objectInfo);
    }
    private void OnItemDeleted(ListObjectInfo objectInfo)
    {
        objectInfo.ObjectIsActive = false;

        modelReferance.ItemDeleted(objectInfo);
    }
    private void OnItemGrabbed(ListObjectInfo objectInfo)
    {

    }
    #endregion


    #region Control Panel Event Responses
    private void PreviousList()
    {
        modelReferance.PreviousList();
        viewReferance.ClearItemInfo();
    }
    private void RotateListLeft()
    {
        viewReferance.RotateListLeft();
    }
    private void RotateListRight()
    {
        viewReferance.RotateListRight();
    }
    private void MoveListUp()
    {
        viewReferance.MoveListUp();
    }
    private void MoveListDown()
    {
        viewReferance.MoveListDown();
    }

    #endregion




    private void OnInvalidRequest(string invalidItem = null)
    {
        viewReferance.InvalidRequestMade(invalidItem);
    }
    private void SetListView(List<ListObjectInfo> passedList)
    {
        viewReferance.ClearList();
        viewReferance.InitializeList(passedList);
    }

}
