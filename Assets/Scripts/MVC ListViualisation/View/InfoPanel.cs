using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{


    public Text uiNameText;
    public Text uiTypeText;
    public Text uiInfoText;


    public void SetText(string nameText, string typeText = null, string infoText =null)
    {
        uiNameText.text = nameText;

        if (typeText != null) uiTypeText.text = typeText;
        if (infoText != null) uiInfoText.text = infoText;
    }

    public void ClearText()
    {
        uiNameText.text = "";
        uiTypeText.text = "";
        uiInfoText.text = "";
    }
}
