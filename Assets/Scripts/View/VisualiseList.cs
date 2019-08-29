using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class VisualiseList : MonoBehaviour
{

    public delegate void OnInteractEvents(ListObjectInfo item);
    public OnInteractEvents onSelected;
    public OnInteractEvents onHovered;
    public OnInteractEvents onUnHovered;


    public InfoPanel itemInfoPanel;             //Referance to the information panel script.
    public GameObject listItemObjectPrefab;     //The visual gameobject representation of the list item.
    [Header("Visual Settings")]
    public GameObject spawnEffectPrefab;        //The effect that will play when the item is spawned.
    [Range (0.1f, 4)]
    public float distBetweenItemCenter = 1;     //The distance between the center of the items.
    [Range(1, 10)]
    public int itemsInRow = 3;                  //The number of items in the row.




    #region List Controls

    public void ClearList()
    {
        StopAllCoroutines();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void InitializeList(List<ListObjectInfo> thisList)
    {
        StartCoroutine(ShowList(thisList));
    }

    public void DisplayItemInfo(ListObjectInfo item, bool showAllInfo = false)
    {
        itemInfoPanel.SetText(item.ObjectName);


        if (showAllInfo)
        {
            itemInfoPanel.SetText(item.ObjectName, item.ObjectType, item.ObjectInfo);
        }
    }
    public void ClearItemInfo()
    {
        itemInfoPanel.ClearText();
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


    #region List Graphical Visualisation Methods

    private IEnumerator ShowList(List<ListObjectInfo> thisList)
    {
        int count = 0;
        float spawnX = 0;
        float spawnY = 0;
        float spawnZ = 0;

        //Calculate spawn time for each item in list.
        float spawnTime = (float)1.5f / thisList.Count;
        if (thisList.Count <= 3) spawnTime = 0.2f;

        //Check each item in given list.
        foreach (ListObjectInfo item in thisList)
        {
            //If end of row is reached.
            if (count == itemsInRow * itemsInRow)
            {
                count = 0;
                spawnX = 0;
                spawnY += distBetweenItemCenter;
                spawnZ = 0;

            }

            //If end of layer is reached.
            if ((count % itemsInRow)==0)
            {
                spawnX = 0;
                spawnZ += distBetweenItemCenter;
            }

            //Wait for spawn to finish.
            yield return StartCoroutine(SpawnItem(new Vector3(spawnX,spawnY,spawnZ),item, spawnTime));

            spawnX+=distBetweenItemCenter;
            count++;
        }
    }

    private IEnumerator SpawnItem(Vector3 spawnPosition, ListObjectInfo item, float spawnTime)
    {
        //The drop start position is slightly above spawn position by the ammount of the distance between items
        GameObject itemVisualRepresentation = Instantiate(listItemObjectPrefab, transform.position+ spawnPosition, listItemObjectPrefab.transform.rotation);
        Vector3 dropStartPos = new Vector3(spawnPosition.x, spawnPosition.y + distBetweenItemCenter, spawnPosition.z);

        itemVisualRepresentation.transform.SetParent(gameObject.transform);
        itemVisualRepresentation.name = item.ObjectName;
        itemVisualRepresentation.transform.position = transform.position + dropStartPos;

        //Give the item information about what it is.
        itemVisualRepresentation.GetComponent<ListObjectInstance>().item = item;

        //Show 3D model of icon.
        if (item.IconModel != null) ShowItemIconGameObject(item, itemVisualRepresentation);

        //Wait for move to finish.
        yield return StartCoroutine(MoveOverSeconds(itemVisualRepresentation.transform, spawnPosition, spawnTime));

        //Instantiate particle effects.
        if (spawnEffectPrefab!=null)Destroy(Instantiate(spawnEffectPrefab, itemVisualRepresentation.transform),0.5f);
    }


    


    private IEnumerator MoveOverSeconds(Transform objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, transform.position + end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = transform.position+end;
    }


    private void ShowItemIconGameObject(ListObjectInfo item, GameObject itemGameObject)
    {
        GameObject itemIcon = item.IconModel;
        GameObject spawnedIcon = Instantiate(itemIcon, gameObject.transform);
        spawnedIcon.transform.SetParent(itemGameObject.transform);
        spawnedIcon.transform.localPosition = Vector3.zero;
        spawnedIcon.transform.localScale = new Vector3 (1,1,1);
    }

    #endregion





}
