using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public void SetText(string description, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        contentField.text = description;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }
  
}
