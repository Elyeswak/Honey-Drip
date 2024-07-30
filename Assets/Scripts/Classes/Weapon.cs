using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    public int damage;
    public float range;
    public float attackSpeed;

    public override void ItemEffect()
    {
        base.ItemEffect();
        EquipWeapon();
    }

    private void EquipWeapon()
    {
       
    }
}
