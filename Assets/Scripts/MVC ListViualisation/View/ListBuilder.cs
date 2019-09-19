using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script controls the physical presentation
/// of the list.
/// <para/>(This is a sub-script of View)
/// </summary>
public class ListBuilder : MonoBehaviour
{
    public GameObject listItemObjectPrefab;     //The visual gameobject representation of the list item.
    [Header("Visual Settings")]
    public GameObject spawnEffectPrefab;        //The effect that will play when the item is spawned.
    [Range(0.1f, 4)]
    public float distBetweenItemCenter = 1;     //The distance between the center of the items.
    [Range(1, 10)]
    public int itemsInRow = 3;                  //The number of items in the row.
    [Header("Audio")]
    public AudioSource source;                  //The source the sound will play from.
    public AudioClip spawnSound;                //The sound an item will make when it spawns.

    private Vector3 originPos;                  //The starting position of the spawner.
    private Quaternion originRot;               //The starting rotation of the spawner.

    /// <summary>
    /// Build list of 'ListObjectInfo' objects in physical gameobject form.
    /// </summary>
    /// <param name="listToBuild">List of objects to build</param>
    public void BuildList(List<ListObjectInfo> listToBuild)
    {
        StartCoroutine(ShowList(listToBuild));

        //Reset Position
        gameObject.transform.position = originPos;
        gameObject.transform.rotation = originRot;
    }



    /// <summary>
    /// Delete each gameobejct in physical list.
    /// </summary>
    public void ClearList()
    {

        /*
         * NOTE this could be improved by keeping
         * a referance to each gameobject and deleting
         * the referance so that if the gameobject
         * gets unparented it will still be deleted.
         */
        StopAllCoroutines();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }



    private void Start()
    {
        originPos = gameObject.transform.position;
    }

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
            if ((count % itemsInRow) == 0)
            {
                spawnX = 0;
                spawnZ += distBetweenItemCenter;
            }

            //Play the spawning sound.
            source.PlayOneShot(spawnSound);

            //Wait for spawn to finish.
            yield return StartCoroutine(SpawnItem(new Vector3(spawnX, spawnY, spawnZ), item, spawnTime));

            spawnX += distBetweenItemCenter;
            count++;
        }

    }

    private IEnumerator SpawnItem(Vector3 spawnPosition, ListObjectInfo item, float spawnTime)
    {
        //The drop start position is slightly above spawn position by the ammount of the distance between items
        GameObject itemVisualRepresentation = Instantiate(listItemObjectPrefab, transform.position + spawnPosition, transform.localRotation);
        Vector3 dropStartPos = new Vector3(spawnPosition.x, spawnPosition.y + distBetweenItemCenter, spawnPosition.z);

        itemVisualRepresentation.transform.SetParent(gameObject.transform);
        itemVisualRepresentation.name = item.ObjectName;
        itemVisualRepresentation.transform.localPosition = dropStartPos;

        //Give the item information about what it is.
        itemVisualRepresentation.GetComponent<ListObjectInstance>().item = item;
        //Give the item a referance to the view. **Dude this is stupid wise up and fix this**
        itemVisualRepresentation.GetComponent<ListObjectInstance>().viewReferance = GetComponent<View>();

        //Show 3D model of icon.
        if (item.IconModel != null) ShowItemIconGameObject(item, itemVisualRepresentation);

        //Wait for move to finish.
        yield return StartCoroutine(MoveOverSeconds(itemVisualRepresentation.transform, dropStartPos, spawnPosition, spawnTime));

        //Instantiate particle effects.
        if (spawnEffectPrefab != null) Destroy(Instantiate(spawnEffectPrefab, itemVisualRepresentation.transform), 0.5f);
    }




    private IEnumerator MoveOverSeconds(Transform objectToMove, Vector3 start, Vector3 end, float seconds)
    {
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(start, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.localPosition = end;
    }


    private void ShowItemIconGameObject(ListObjectInfo item, GameObject itemGameObject)
    {
        GameObject itemIcon = item.IconModel;
        Vector3 scale = item.IconModel.transform.localScale;
        GameObject spawnedIcon = Instantiate(itemIcon, gameObject.transform);
        spawnedIcon.transform.SetParent(itemGameObject.transform);
        spawnedIcon.transform.localPosition = Vector3.zero;
        spawnedIcon.transform.localScale = scale;
    }

}
