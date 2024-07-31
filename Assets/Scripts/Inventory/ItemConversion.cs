using System;
using UnityEngine;

public static class ItemConversion
{
    public static ItemData ToItemData(Item item)
    {
        int? honeyRestoreAmount = null;
        int? healthRestoreAmount = null;

        if (item is Honey honey)
        {
            honeyRestoreAmount = honey.honeyRestoreAmount;
        }

        if (item is Potion potion)
        {
            healthRestoreAmount = potion.healthRestoreAmount;
        }

        return new ItemData
        {
            itemName = item.itemName,
            Quantity = item.Quantity,
            Rarity = item.Rarity.ToString(),
            Value = item.Value,
            Level = item.Level,
            description = item.description,
            iconPath = item.icon != null ? GetAssetPath(item.icon) : "",
            prefabPath = item.prefab != null ? GetAssetPath(item.prefab) : "",
            maxStack = item.maxStack,
            cardCollectionPath = item.cardCollection != null ? GetAssetPath(item.cardCollection) : "",
            cardPrefabPath = item.cardPrefab != null ? GetAssetPath(item.cardPrefab) : "",
            itemType = item.GetType().Name,
            honeyRestoreAmount = honeyRestoreAmount,
            healthRestoreAmount = healthRestoreAmount
        };
    }

    public static Item ToItem(ItemData itemData)
    {
        Item item;
        if (itemData.honeyRestoreAmount.HasValue)
        {
            item = ScriptableObject.CreateInstance<Honey>();
        }
        else if (itemData.healthRestoreAmount.HasValue)
        {
            item = ScriptableObject.CreateInstance<Potion>();
        }
        else
        {
            item = ScriptableObject.CreateInstance<Item>();
        }

        item.itemName = itemData.itemName;
        item.Quantity = itemData.Quantity;
        item.Rarity = (Rarity)Enum.Parse(typeof(Rarity), itemData.Rarity);
        item.Value = itemData.Value;
        item.Level = itemData.Level;
        item.description = itemData.description;
        item.icon = LoadAsset<Sprite>(itemData.iconPath);
        item.prefab = LoadAsset<GameObject>(itemData.prefabPath);
        item.maxStack = itemData.maxStack;
        item.cardCollection = LoadAsset<GameObject>(itemData.cardCollectionPath);
        item.cardPrefab = LoadAsset<GameObject>(itemData.cardPrefabPath);

        if (item is Honey honey && itemData.honeyRestoreAmount.HasValue)
        {
            honey.honeyRestoreAmount = itemData.honeyRestoreAmount.Value;
        }

        if (item is Potion potion && itemData.healthRestoreAmount.HasValue)
        {
            potion.healthRestoreAmount = itemData.healthRestoreAmount.Value;
        }

        return item;
    }

    private static string GetAssetPath(UnityEngine.Object asset)
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.GetAssetPath(asset);
#else
        // Implement runtime path retrieval if needed, or return empty string
        return "";
#endif
    }

    private static T LoadAsset<T>(string path) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
#else
        return Resources.Load<T>(path);
#endif
    }
}
