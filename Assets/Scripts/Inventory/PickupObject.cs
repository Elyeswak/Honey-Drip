using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            Item it = ScriptableObject.Instantiate<Item>(item);

            GameManager.PlayerController.inventory.AddItem(it);
            Debug.Log("Ontrigger item");
            Destroy(gameObject);
        }
    }
}
