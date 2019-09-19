using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnContactBehavior : MonoBehaviour
{
    public enum ItemBehavior {moveItem, openItem, closeItem, deleteItem }

    public ItemBehavior itemBehavior;
    public OnContactGameobject onContactEvents;


    public GameObject destructionFX;
    public AudioClip destructionAudio;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        onContactEvents.onCollisionContact += OnContact;
        onContactEvents.onTriggerContact += OnContact;
    }
    private void OnDisable()
    {
        onContactEvents.onCollisionContact -= OnContact;
        onContactEvents.onTriggerContact -= OnContact;
    }



    //What happens when contact is made with the item.
    private void OnContact(ListObjectInstance itemInstance)
    {

        switch (itemBehavior)
        {
            case ItemBehavior.moveItem:
                itemInstance.ThisItemMoved();
                break;
            case ItemBehavior.openItem:
                itemInstance.ThisItemSelected();
                break;
            case ItemBehavior.closeItem:
                itemInstance.ThisItemClosed();
                break;
            case ItemBehavior.deleteItem:
                itemInstance.ThisItemDeleted();
                break;
            default:
                Debug.Log("ItemOnContactBehavior: No Item behavior selected.");
                break;
        }


        if (destructionFX != null) Destroy(Instantiate(destructionFX, transform), 2);
        if (destructionAudio != null) source.PlayOneShot(destructionAudio);

        Destroy(itemInstance.gameObject);
    }






}
