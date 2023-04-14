using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float speedToAdd;
    public float speedPowerupTime;

    private void OnTriggerEnter(Collider other)
    {

        RigidbodyPlayerController player = other.gameObject.GetComponent<RigidbodyPlayerController>();
        if(!player)
        {
            player = other.gameObject.GetComponentInParent<RigidbodyPlayerController>();
        }

        if(player)
        {
            Debug.Log("Speed added!");
            player.ChangeSpeed(speedToAdd, speedPowerupTime);
            gameObject.SetActive(false);
        }
        
    }


}
