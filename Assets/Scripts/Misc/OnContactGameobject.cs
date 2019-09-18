using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent (typeof(AudioSource),typeof(Collider), typeof(Rigidbody))]
public class OnContactGameobject : MonoBehaviour
{
    public delegate void OnContact(ListObjectInstance itemInstance);
    public OnContact onTriggerContact;
    public OnContact onCollisionContact;


    private void OnCollisionEnter(Collision collision)
    {
        

        ListObjectInstance itemInstance = collision.gameObject.GetComponent<ListObjectInstance>();

        if (itemInstance != null)
        {
            if (onCollisionContact != null) onCollisionContact.Invoke(itemInstance);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        

        ListObjectInstance itemInstance = other.gameObject.GetComponent<ListObjectInstance>();

        if (itemInstance != null)
        {
            if (onTriggerContact != null) onTriggerContact.Invoke(itemInstance);

        }
    }
}
