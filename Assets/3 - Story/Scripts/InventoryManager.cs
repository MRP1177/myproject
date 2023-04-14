using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // Conversation Management
    ConversationManager npc;

    // Item management
    public List<CollectableItem> heldItems;
    public TMP_Text heldItemUI;
    public List<CollectableItem> itemsInRange;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHeldItemSummary();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessHeldItemUI();
        ProcessPickupItem();
    }

    public void AddItemInRange(CollectableItem item)
    {
        itemsInRange.Add(item);
    }

    public void RemoveItemOutOfRange(CollectableItem item)
    {
        itemsInRange.Remove(item);
    }

    void ProcessHeldItemUI()
    {
        if (heldItemUI)
        {
            if (Input.GetKeyDown("i"))
            {
                if (heldItemUI.IsActive())
                {
                    heldItemUI.gameObject.SetActive(false);
                }
                else
                {
                    heldItemUI.gameObject.SetActive(true);
                }
            }
        }
    }

    public void PickupItem(CollectableItem item)
    {
        heldItems.Add(item);
        UpdateHeldItemSummary();
    }

    public void RemoveItem(CollectableItem item)
    {
        for(int i = 0; i < heldItems.Count; i++)
        {
            if(item.itemName == heldItems[i].itemName)
            {
                heldItems.RemoveAt(i);
                i--;
            }
        }
        UpdateHeldItemSummary();
    }

    public void UpdateHeldItemSummary()
    {
        List<string> itemSummary = new List<string>();

        // Prepare for counting individual items
        List<string> justItemNames = new List<string>();
        foreach(CollectableItem item in heldItems)
        {
            justItemNames.Add(item.itemName);
        }
        List<string> onlyOne = justItemNames.Distinct().ToList();

        foreach (string item in onlyOne)
        {
            int count = 0;
            foreach (CollectableItem subItem in heldItems)
            {
                if (item == subItem.itemName)
                {
                    count++;
                }
            }
            if (count == 1)
            {
                itemSummary.Add(item);
            }
            else if (count > 1)
            {
                itemSummary.Add(item + " x" + count);
            }
        }

        // Generate report for UI
        string itemReport = "Held Items:\n";
        foreach (string entry in itemSummary)
        {
            itemReport += entry + '\n';
        }

        // Update UI
        heldItemUI.text = itemReport;

    }

    void ProcessPickupItem()
    {
        if(itemsInRange.Count > 0 && Input.GetKeyDown("e"))
        {
            for(int i = 0; i < itemsInRange.Count; i++)
            {
                heldItems.Add(itemsInRange[i]);
                itemsInRange[i].gameObject.SetActive(false);
            }
            UpdateHeldItemSummary();
        }
        
    }
}
