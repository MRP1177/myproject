using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Rigidbody rb;

    public float trampolineStrength;
    public float collisionForce;

    bool recentlyHit;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MovingObstacle")
        {
            recentlyHit = true;
        }
        else if(collision.gameObject.tag == "Trampoline")
        {
            Debug.Log("On Trampoline!");
            rb.AddForce(0, trampolineStrength, 0);
        }
    }

    
    private void FixedUpdate()
    {
        if(recentlyHit)
        {
            Debug.Log("recently hit!");

            float x = rb.velocity.x;
            float z = rb.velocity.z;
            Debug.Log("X: " + x + "    Z: " + z + "     Y: " + rb.velocity.y);
            //Vector3 fixedForceVec = new Vector3(forceVec.x, 0, forceVec.z);
            rb.AddForce(new Vector3(Random.Range(-collisionForce,collisionForce), collisionForce, -collisionForce), ForceMode.Impulse);

            recentlyHit = false;

            animator.SetTrigger("Hit");
        }
    }

    
}
