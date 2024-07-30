using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new honey", menuName = "Item/Honey")]
public class Honey : Item
{
    public int honeyRestoreAmount;

    public override void ItemEffect()
    {
        base.ItemEffect();
        RestoreHoney();
    }

    private void RestoreHoney()
    {
        Debug.Log("honey effect!");
    }
}
