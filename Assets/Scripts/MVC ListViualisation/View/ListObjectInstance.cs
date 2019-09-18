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
    public void ItemSelected()
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


        View viewReferance = FindObjectOfType<View>();

        if (viewReferance != null) viewReferance.CallItemSelectedEvent(item);
    }

    public void ItemHovered()
    {
        View viewReferance = FindObjectOfType<View>();

        if (viewReferance != null) viewReferance.CallItemHoveredEvent(item);
    }
    public void ItemUnHovered()
    {
        View viewReferance = FindObjectOfType<View>();

        if (viewReferance != null) viewReferance.CallItemUnHoveredEvent(item);
    }
}
