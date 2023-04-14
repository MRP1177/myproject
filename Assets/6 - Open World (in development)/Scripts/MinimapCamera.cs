using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For more info on how to use this script go to https://sharpcoderblog.com/blog/unity-3d-minimap-tutorial

public class MinimapCamera : MonoBehaviour
{
    public Transform target;

    float defaultPosY;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply position
        transform.position = new Vector3(target.position.x, defaultPosY, target.position.z);
        // Apply rotation
        transform.rotation = Quaternion.Euler(90, target.eulerAngles.y, 0);
    }
}