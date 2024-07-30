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
            Debug.Log("item pickedup");
            Item it = ScriptableObject.Instantiate<Item>(item);

            GameManager.PlayerController.inventory.AddItem(it);

           

            Destroy(gameObject);
        }
    }
}
