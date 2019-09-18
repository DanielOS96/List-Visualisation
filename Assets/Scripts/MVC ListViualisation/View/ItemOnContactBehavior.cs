using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnContactBehavior : MonoBehaviour
{
    public enum ItemBehavior {moveItem, openItem }

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
                MoveItem(itemInstance);
                break;
            case ItemBehavior.openItem:
                OpenItem(itemInstance);
                break;
            default:
                Debug.Log("ItemOnContactBehavior: No Item behavior selected.");
                break;
        }


        
    }


    private void MoveItem(ListObjectInstance itemInstance)
    {
        View viewReferance = FindObjectOfType<View>();

        viewReferance.CallItemMovedEvent(itemInstance.item);

        PlayEffects();

        Destroy(itemInstance.gameObject);
    }

    private void OpenItem(ListObjectInstance itemInstance)
    {
        itemInstance.ItemSelected();

        PlayEffects();

        Destroy(itemInstance.gameObject);
    }



    private void PlayEffects()
    {

        if(destructionFX!=null)Destroy(Instantiate(destructionFX, transform), 2);
        if (destructionAudio != null) source.PlayOneShot(destructionAudio);
    }


}
