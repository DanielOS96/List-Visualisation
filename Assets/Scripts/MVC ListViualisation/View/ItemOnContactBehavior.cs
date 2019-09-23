using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnContactBehavior : MonoBehaviour
{
    public enum ItemBehavior {moveItem, openItem, closeItem, deleteItem }

    public ItemBehavior itemBehavior;
    public OnContactGameobject onContactEvents;


    public GameObject contactFX;
    public AudioClip contactAudio;
    public bool destroyAfter;

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
                Debug.Log("ItemOnContactBehavior: No item behavior selected.");
                break;
        }


        if (contactFX != null) Destroy(Instantiate(contactFX, transform), 2);
        if (contactAudio != null) source.PlayOneShot(contactAudio);

        if (destroyAfter) Destroy(itemInstance.gameObject);
    }






}
