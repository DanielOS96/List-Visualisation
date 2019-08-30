using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script Controls the information panel.
/// <para/>(This is a sub-script of View)
/// </summary>
public class InfoPanel : MonoBehaviour
{

    public Text uiNameText;
    public Text uiTypeText;
    public Text uiInfoText;

    /// <summary>
    /// Set atleast one of the 3 text components.
    /// </summary>
    public void SetText(string nameText, string typeText = null, string infoText =null)
    {
        uiNameText.text = nameText;

        if (typeText != null) uiTypeText.text = typeText;
        if (infoText != null) uiInfoText.text = infoText;
    }


    /// <summary>
    /// Clear all 3 text components.
    /// </summary>
    public void ClearText()
    {
        uiNameText.text = "";
        uiTypeText.text = "";
        uiInfoText.text = "";
    }
}
