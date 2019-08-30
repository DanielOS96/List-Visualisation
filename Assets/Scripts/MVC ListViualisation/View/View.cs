﻿using System.Collections;
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

    public delegate void OnInteractEvents(ListObjectInfo item);
    public OnInteractEvents onSelected;
    public OnInteractEvents onHovered;
    public OnInteractEvents onUnHovered;


    public InfoPanel itemInfoPanel;             //Referance to the information panel script.
    public ListBuilder listBuilder;             //Referance to the list builder script.



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
        listBuilder.BuildList(listToBuild);
    }

    #endregion


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
    #endregion




}
