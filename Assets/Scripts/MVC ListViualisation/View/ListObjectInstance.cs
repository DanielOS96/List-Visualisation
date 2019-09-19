using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script component will be added to the gameobject listobject.
/// <para/> (This is a sub-script of View)
/// </summary>
public class ListObjectInstance : MonoBehaviour
{

    public ListObjectInfo item; //The item information for this object.
    public View viewReferance;  //The referance to the view.

    public bool itemSelected;



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
    public void ThisItemSelected()
    {
        //Select this item.
        itemSelected = true;

        //unselect all other items.
        /*
        ListObjectInstance[] instances;
        instances  = FindObjectsOfType<ListObjectInstance>();
        foreach (ListObjectInstance i in instances)
        {
            if (i != this)
            {
                i.itemSelected = false;
            }
        }*/

        viewReferance.CallItemSelectedEvent(item);
    }

    public void ThisItemHovered()
    {
        viewReferance.CallItemHoveredEvent(item);
    }
    public void ThisItemUnHovered()
    {
        viewReferance.CallItemUnHoveredEvent(item);
    }
    public void ThisItemMoved()
    {
        viewReferance.CallItemMovedEvent(item);
    }
    public void ThisItemClosed()
    {
        viewReferance.CallItemClosedEvent(item);
    }
    public void ThisItemDeleted()
    {
        viewReferance.CallItemDeletedEvent(item);
    }
}
