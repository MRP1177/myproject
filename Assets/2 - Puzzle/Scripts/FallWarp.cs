using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWarp : MonoBehaviour
{
    public float invisibleFloor = -10f;
    public GameObject warpPoint;

    // Update is called once per frame
    void Update()
    {
       
        if(transform.position.y < invisibleFloor)
        {
            Debug.Log(transform.position.y);
            CharacterController controller = GetComponent<CharacterController>();
            controller.enabled = false;
            transform.position = warpPoint.transform.position;
            controller.enabled = true;
        }
    }
}
