// File: PInventoryData.cs
using System;
using System.Collections.Generic;

[Serializable]
public class PInventoryData
{
    public List<ItemData> itemsList;

    public PInventoryData()
    {
        itemsList = new List<ItemData>();
    }
}
