using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClickGameobject : MonoBehaviour
{
    public UnityEvent leftClicked;
    public UnityEvent rightClicked;
    public UnityEvent middleClicked;
    public UnityEvent mouseHover;
    public UnityEvent mouseUnHover;

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) leftClicked.Invoke();
        if (Input.GetMouseButtonDown(1)) rightClicked.Invoke();
        if (Input.GetMouseButtonDown(2)) middleClicked.Invoke();


    }

    private void OnMouseEnter()
    {
        mouseHover.Invoke();

    }
    private void OnMouseExit()
    {
        mouseUnHover.Invoke();
    }



}
