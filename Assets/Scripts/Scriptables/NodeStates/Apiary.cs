using UnityEngine;

[CreateAssetMenu(fileName = "Apiary", menuName = "ScriptableObjects/NodeStats/Apiary")]
public class Apiary : NodeStats
{
    public int harvestingTime;
    public int honeyAmount;
    public int honeyMax;
    public Building _Prefab;
}