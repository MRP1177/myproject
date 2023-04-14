//You are free to use this script in Free or Commercial projects
//sharpcoderblog.com @2019

using UnityEngine;

public class SC_PickItem : MonoBehaviour
{
    public string itemName = "Some Item"; //Each item must have an unique name
    public Texture itemPreview;

    GameObject contactedObject;

    void Start()
    {
        //Change item tag to Respawn to detect when we look at it
        gameObject.tag = "Collectable";
    }

    public void PickItem()
    {
        if(contactedObject)
        {
            ItemTrigger itemTrigger = contactedObject.GetComponent<ItemTrigger>();
            if(itemTrigger)
            {
                itemTrigger.TurnBackOff();
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        contactedObject = other.gameObject;
    }
}