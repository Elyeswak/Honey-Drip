using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Item : MonoBehaviour
{
    public Image cardimage;
    public TextMeshProUGUI cardTxt;

    public void AssignCardtoUi(Sprite s, string str)
    {
        cardimage.sprite = s;
        cardTxt.text = str;

    }
}
