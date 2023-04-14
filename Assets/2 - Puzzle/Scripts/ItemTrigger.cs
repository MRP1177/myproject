using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTrigger : MonoBehaviour
{
    public string ItemTypeText;
    TMP_Text textObject;

    public bool StartOn = true;

    public GameObject connectedObject;

    // Start is called before the first frame update
    void Start()
    {
        // Setup floating text
        textObject = GetComponentInChildren<TMP_Text>();
        textObject.text = ItemTypeText;

        // Turn object on or off, based on set parameters
        if(StartOn)
        {
            connectedObject.SetActive(true);
        }
        else
        {
            connectedObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger!");
        SC_PickItem item = other.gameObject.GetComponent<SC_PickItem>();  // Get access to the script
        Debug.Log("Time for name!");
        string itemName = "";
        if(item)
        {
            itemName = item.itemName;
        }
        else
        {
            item = other.gameObject.GetComponentInParent<SC_PickItem>();
            itemName = item.itemName;
        }

        // Process whether or not this collision should trigger a specific response!
        if(ItemTypeText == itemName)
        {
            connectedObject.SetActive(!StartOn);
        }
    }

    public void TurnBackOff()
    {
         connectedObject.SetActive(StartOn);
    }
}
