using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementConstrrainer : MonoBehaviour
{
    float initialX;
    float initialY;
    float initialZ;


    // Start is called before the first frame update
    void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initialX, initialY, initialZ);
    }
}
