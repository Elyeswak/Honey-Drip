using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new potion", menuName = "Item/Potion")]
public class Potion : Item
{
    public int healthRestoreAmount;

    public override void ItemEffect()
    {
        base.ItemEffect();
        RestoreHealth();
    }

    private void RestoreHealth()
    {
    
    }
}
