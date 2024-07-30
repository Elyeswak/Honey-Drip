using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new item", menuName = "Item")]
public class Item:ScriptableObject
{
    protected string ID ;
    [SerializeField]
    public int Quantity;
    public  Rarity Rarity ;
    public int Value;
    public int Level;
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject prefab;
    public int maxStack;
    public GameObject cardCollection;
    public GameObject cardPrefab;
    protected InventroyManager invManager;
    public int getQuantity()
    {
        return this.Quantity;
    }

    public virtual void ItemEffect()
    {

        Debug.Log("item default effect !");

    }

    public void init()
    {
        
        invManager = GameObject.FindGameObjectWithTag("INmanager").GetComponent<InventroyManager>();

    }

}
