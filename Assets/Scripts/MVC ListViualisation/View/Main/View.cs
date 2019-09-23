using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* 
 * ---------Class Description----------------------------------
 * 
 * This is the main script of the 'View' structure
 * of the Model View Controller. 
 * 
 * This script can be directly referanced by the controller.
 * It cannot directly referance the controller so any
 * communucation with the controller will be via events.
 * ------------------------------------------------------------
 */

/// <summary>
/// This script will:
/// <para/>Handle the graphical side of spawning the list.
/// </summary>
public class View : MonoBehaviour
{

    public delegate void OnItemInteractEvents(ListObjectInfo itemInfo);
    public OnItemInteractEvents onSelected;
    public OnItemInteractEvents onHovered;
    public OnItemInteractEvents onUnHovered;
    public OnItemInteractEvents onMoved;
    public OnItemInteractEvents onClosed;
    public OnItemInteractEvents onDeleted;
    public OnItemInteractEvents onGrabbed;

    public delegate void OnControlPanelEvents();
    public OnControlPanelEvents onPreviousList;
    public OnControlPanelEvents onLeftRotatateList;
    public OnControlPanelEvents onRightRotatateList;
    public OnControlPanelEvents onMoveListUp;
    public OnControlPanelEvents onMoveListDown;


    public InfoPanel itemInfoPanel;             //Referance to the information panel script.
    public ListBuilder listBuilder;             //Referance to the list builder script.
    public ListMovement listMovement;           //Referance to the list movement script.


    #region Item Controls
    /// <summary>
    /// Display the information of the item i.e. Name.
    /// </summary>
    /// <param name="item">The list item whos information to show.</param>
    /// <param name="showAllInfo">Show </param>
    public void DisplayItemInfo(ListObjectInfo item, bool showAllInfo = false)
    {
        itemInfoPanel.SetText(item.ObjectName);

        if (showAllInfo) itemInfoPanel.SetText(item.ObjectName, item.ObjectType, item.ObjectInfo);
    }
    /// <summary>
    /// Clear the list item information on display.
    /// </summary>
    public void ClearItemInfo()
    {
        itemInfoPanel.ClearText();
    }

    public void ReturnItemToOrigin(ListObjectInfo objectInfo)
    {
        objectInfo.ObjectTransform.SetParent(objectInfo.ParentTransform);
        objectInfo.ObjectTransform.localPosition = objectInfo.OriginPos;
        objectInfo.ObjectTransform.localRotation = objectInfo.OriginRot;
    }

    #endregion


    #region List Controls
    /// <summary>
    /// Delete each gameobejct in physical list.
    /// </summary>
    public void ClearList()
    {
        listBuilder.ClearList();
    }

    /// <summary>
    /// Build list of 'ListObjectInfo' objects in physical gameobject form.
    /// </summary>
    /// <param name="listToBuild">List of objects to build</param>
    public void InitializeList(List<ListObjectInfo> listToBuild)
    {
        //Build the list.
        listBuilder.BuildList(listToBuild);
        //Reset the position of list.
        listMovement.ResetPosition();
    }

    /// <summary>
    /// Called when an invalid request has been made.
    /// </summary>
    public void InvalidRequestMade(string invalidItem =null)
    {
        Debug.Log("Ayyy makin invalid requests I see... well... don't be at that. " +
            "Maybe I should play a sound or something to indicate your doing something dumb. " +invalidItem);
    }

    #endregion

    //Called from the controller.
    #region ListMovement Controls
    public void RotateListLeft()
    {
        listMovement.RotateLeft();
    }
    public void RotateListRight()
    {
        listMovement.RotateRight();
    }
    public void MoveListUp()
    {
        listMovement.MoveUp();
    }
    public void MoveListDown()
    {
        listMovement.MoveDown();
    }
    #endregion


    //These are the events that comunicate with controller.
    #region Individual List Item Interacted Event Callers
    /// <summary>
    /// Fire the list item selected event. 
    /// Called by individual item.
    /// </summary>
    /// <param name="item">Referance to specific list item.</param>
    public void CallItemSelectedEvent(ListObjectInfo item)
    {
        if (onSelected != null) onSelected(item);
    }
    /// <summary>
    /// Fire the list item hoverd event.
    /// Called by individual item.
    /// </summary>
    /// <param name="item">Referance to specific list item.</param>
    public void CallItemHoveredEvent(ListObjectInfo item)
    {
        if (onHovered != null) onHovered(item);

    }
    /// <summary>
    /// Fire the list item unhoverd event.
    /// Called by individual item.
    /// </summary>
    /// <param name="item">Referance to specific list item.</param>
    public void CallItemUnHoveredEvent(ListObjectInfo item)
    {
        if (onUnHovered != null) onUnHovered(item);

    }
    /// <summary>
    /// Fire the list item moved event.
    /// </summary>
    /// <param name="item">Item to be moved.</param>
    public void CallItemMovedEvent(ListObjectInfo item)
    {
        if (onMoved != null) onMoved(item);
    }

    /// <summary>
    /// Fire the list item grabbed event.
    /// </summary>
    /// <param name="item">Item to be moved.</param>
    public void CallItemGrabbedEvent(ListObjectInfo item)
    {
        if (onGrabbed != null) onGrabbed(item);
    }

    /// <summary>
    /// Fire the list item closed event.
    /// </summary>
    /// <param name="item">Item to be moved.</param>
    public void CallItemClosedEvent(ListObjectInfo item)
    {
        if (onClosed != null) onClosed(item);
    }

    /// <summary>
    /// Fire the list item deleted event.
    /// </summary>
    /// <param name="item">Item to be moved.</param>
    public void CallItemDeletedEvent(ListObjectInfo item)
    {
        if (onDeleted != null) onDeleted(item);
    }
    #endregion


    //These are the events that communicate with the controller
    #region Control Panel Interacted Event Callers
    public void CallPreviousList()
    {
        if (onPreviousList != null) onPreviousList.Invoke();
    }
    public void CallLeftRotateList()
    {
        if (onLeftRotatateList != null) onLeftRotatateList.Invoke();
    }
    public void CallRightRotateList()
    {
        if (onRightRotatateList != null) onRightRotatateList.Invoke();
    }
    public void CallMoveListUp()
    {
        if (onMoveListUp != null) onMoveListUp.Invoke();
    }
    public void CallMoveListDown()
    {
        if (onMoveListDown != null) onMoveListDown.Invoke();
    }
    #endregion

}
