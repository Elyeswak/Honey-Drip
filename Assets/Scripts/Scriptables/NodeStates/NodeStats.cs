using System;
using UnityEngine;
public class NodeStats : ScriptableObject
{
    public Tier tier;
    public int cost;
}
[Serializable]
public enum Tier
{
    T1, T2, T3, T4, T5
}