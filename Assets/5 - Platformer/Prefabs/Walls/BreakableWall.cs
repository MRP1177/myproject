using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject breakableWall;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(breakableWall, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
