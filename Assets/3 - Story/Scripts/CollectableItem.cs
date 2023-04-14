using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;
    InventoryManager player;

    Renderer renderer;
    Color baseColor;
    public Color pickupRangeColor = Color.green;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseColor = renderer.material.color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            renderer.material.color = pickupRangeColor;
            player = other.gameObject.GetComponent<InventoryManager>();
            player.AddItemInRange(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            renderer.material.color = baseColor;
            player = other.gameObject.GetComponent<InventoryManager>();
            player.RemoveItemOutOfRange(this);
        }
    }

    private void OnDisable()
    {
        if(player)
        {
            player.RemoveItemOutOfRange(this);
        }
    }
}
