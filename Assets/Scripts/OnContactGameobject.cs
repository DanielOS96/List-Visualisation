using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent (typeof(AudioSource),typeof(Collider), typeof(Rigidbody))]
public class OnContactGameobject : MonoBehaviour
{

    public GameObject destructionFX;
    public AudioClip destructionAudio;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        ListObjectInstance itemInstance = collision.gameObject.GetComponent<ListObjectInstance>();

        if (itemInstance != null)
        {
            itemInstance.ItemSelected();
            Destroy(Instantiate(destructionFX, transform),2);
            source.PlayOneShot(destructionAudio);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ListObjectInstance itemInstance = other.gameObject.GetComponent<ListObjectInstance>();

        if (itemInstance != null)
        {
            itemInstance.ItemSelected();
            Destroy(Instantiate(destructionFX, transform), 2);
            source.PlayOneShot(destructionAudio);
            Destroy(other.gameObject);
        }
    }
}
