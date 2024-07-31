// File: ItemData.cs
using System;

[Serializable]
public class ItemData
{
    public string itemName;
    public int Quantity;
    public string Rarity;
    public int Value;
    public int Level;
    public string description;
    public string iconPath;
    public string prefabPath;
    public int maxStack;
    public string cardCollectionPath;
    public string cardPrefabPath;
    public string itemType; 
    public int? honeyRestoreAmount; 
    public int? healthRestoreAmount; 
}
