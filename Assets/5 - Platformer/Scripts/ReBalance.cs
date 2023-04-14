using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBalance : MonoBehaviour
{
    bool playerOn;

    float prevTime;
    float currTime;
    float timeDiff;

    float timeTracker;
    Quaternion prevRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerOn)
        {
            timeTracker += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(prevRotation, Quaternion.identity, timeTracker/timeDiff);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = true;
            prevTime = Time.time;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            timeTracker = 0;
            prevRotation = transform.rotation;

            playerOn = false;
            currTime = Time.time;
            timeDiff = currTime - prevTime;
        }
    }
}
