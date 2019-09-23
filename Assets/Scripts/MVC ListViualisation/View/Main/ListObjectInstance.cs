using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * As this is part of the 'View' structure
 * this instance can directly referance methods 
 * within the main script of the view structure 'View'.
 * 
 * The item interacted methods below call
 * events to be sent out from the main 'View' script.
 * 
 * The 'Controller' picks up on these events and preforms
 * the necassary actions.
 */

/// <summary>
/// This script component will be added to the gameobject listobject.
/// <para/> (This is a sub-script of View)
/// </summary>
public class ListObjectInstance : MonoBehaviour
{


    public ListObjectInfo itemInfo; //The item information for this object.
    public View viewReferance;      //The referance to the view.



    
    public void ThisItemSelected()
    {
        viewReferance.CallItemSelectedEvent(itemInfo);
    }

    public void ThisItemHovered()
    {
        viewReferance.CallItemHoveredEvent(itemInfo);
    }
    public void ThisItemUnHovered()
    {
        viewReferance.CallItemUnHoveredEvent(itemInfo);
    }
    public void ThisItemMoved()
    {
        viewReferance.CallItemMovedEvent(itemInfo);
    }
    public void ThisItemClosed()
    {
        viewReferance.CallItemClosedEvent(itemInfo);
    }
    public void ThisItemDeleted()
    {
        viewReferance.CallItemDeletedEvent(itemInfo);
    }
    public void ThisItemGrabbed()
    {
        viewReferance.CallItemGrabbedEvent(itemInfo);
    }
}
