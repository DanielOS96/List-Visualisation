using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * -------------Class Description---------------------------------------------------------
 *
 * This scipt can be thought of as a gateway between the model backend and the controller.
 * This script will remain the same even if the rest of the model backend is swapped out.
 * i.e. the directory list generator is swapped for a manufacturing part list generator.
 * Subscribe to onListReady to get the list when it is ready.
 * So basically Mr Model holds up a copy of the list and says
 * "Ok guys heres the list if anyone wants it"
 * And Mr Controller had been listening for that announcement from Mr Model
 * So Mr Controller is all like "Aye i'll take that thanks"
 * and Mr Controller grabs himself the list and takes it back home with him.
 * So technically this is not the Model interacting with the controller
 * the model is just letting anyone know he has a list and its the
 * controller thats actually interacting with the model as to comply with
 * how a Model View Controller should function.
 * As you can see there is no referance to the controller in here
 * ---------------------------------------------------------------------------------------
 */

/// <summary>
/// This script will be overwritten to add custom data processing functionality. 
/// i.e. file browsing.
/// </summary>
public class Model : MonoBehaviour
{
    public delegate void ListReadyEvent(List<ListObjectInfo> listOfItems);
    public ListReadyEvent onListReady;


    public virtual void BuildList()
    {
        //Debug.Log("Building List...");
    }

    public virtual void PreviousList()
    {
        //Debug.Log("Previous List...");
    }

    public virtual void ItemMoved(ListObjectInfo item)
    {
        //Debug.Log("Move List..."+originList+" to "+destinationList);
    }

    public virtual void ItemSelected(ListObjectInfo item)
    {
        //Debug.Log("Item Selected... "+ item.ObjectName);
    }
    public virtual void ItemHovered(ListObjectInfo item)
    {
        //Debug.Log("Item Hovered... " + item.ObjectName);
    }
    public virtual void ItemDeleted(ListObjectInfo item)
    {
        //Debug.Log("Item Deleted... " + item.ObjectName);
    }

    //Let anyone listning have a copy of the list of items.
    //Pass the list to any subscribed methods.
    internal void ListIsReady(List<ListObjectInfo> thisList)
    {
        //listOfItems = thisList;
        if (onListReady != null) onListReady.Invoke(thisList);
    }


    

}
