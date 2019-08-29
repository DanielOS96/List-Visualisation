using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script component will be added to the gameobject listobject.
/// <para/> (This is part of the 'View' structure)
/// </summary>
public class ListObjectInstance : MonoBehaviour
{

    public ListObjectInfo item; //The item information for this object.



    /*
     * As this is part of the 'View' structure
     * this instance can directly referance methods 
     * within the main script of the view structure 'VisualiseList'.
     * 
     * The item interacted methods below call
     * events to be sent out from the main 'View' script 'VisualiseList'.
     * 
     * The 'Controller' picks up on these events and preforms
     * the necassary actions.
     */
    public void ItemSelected()
    {
        VisualiseList viewReferance = FindObjectOfType<VisualiseList>();

        if (viewReferance != null) viewReferance.CallItemSelectedEvent(item);
    }

    public void ItemHovered()
    {
        VisualiseList viewReferance = FindObjectOfType<VisualiseList>();

        if (viewReferance != null) viewReferance.CallItemHoveredEvent(item);
    }
    public void ItemUnHovered()
    {
        VisualiseList viewReferance = FindObjectOfType<VisualiseList>();

        if (viewReferance != null) viewReferance.CallItemUnHoveredEvent(item);
    }
}
