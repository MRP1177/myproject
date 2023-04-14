using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public List<GameObject> objectsInRange;
    public List<GameObject> interactablesInRange;

    bool checkDisabled = true;
    float checkDisabledDelay = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkDisabled)
        {
            Invoke("DisabledObjectManagement", checkDisabledDelay);
        }
        
    }

    void DisabledObjectManagement()
    {
        for(int i = 0; i < objectsInRange.Count; i++)
        {
            if (objectsInRange[i].gameObject.active == false)
            {
                objectsInRange.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < interactablesInRange.Count; i++)
        {
            if (interactablesInRange[i].gameObject.active == false)
            {
                interactablesInRange.RemoveAt(i);
                i--;
            }
        }
        checkDisabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInRange.Add(other.gameObject);
        if (other.gameObject.tag == "Interactable Object")
        {
            interactablesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInRange.Remove(other.gameObject);
        if (other.gameObject.tag == "Interactable Object")
        {
            interactablesInRange.Remove(other.gameObject);
        }
    }
}
