using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Item
{
    public string Name { get; set; }
    public string EffectReference { get; set; }
    public string ArtReference { get; set; }
    public CardSkinChart SkinChart { get; set; }
}
