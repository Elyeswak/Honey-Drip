using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new honey", menuName = "Item/Honey")]
public class Honey : Item
{
    public int honeyRestoreAmount;

    public override void ItemEffect()
    {
        RestoreHoney();
    }

    private void RestoreHoney()
    {
        GameManager.Player.AddHoney(honeyRestoreAmount);
    } 
}
