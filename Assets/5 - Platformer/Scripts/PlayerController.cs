using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Spawn point
    public GameObject spawnPoint;

    // Info about animiations
    Animator animator;
    float jumpAnimLength;
    
    // Movement variables
    public float maxSpeed = 6;
    float speed;
    public float turnSpeed = 2;
    float forward;
    float speedVector;

    bool recentlyGoingBackwards;
    bool walk;

    // Player choice variables
    public bool freezeRotation = false;
    public bool airControl = false;

    // Physics variables
    Rigidbody rb;
    public float jumpForce;
    new BoxCollider collider;
    public bool onGround;

    void Start()
    {
        transform.position = spawnPoint.transform.position;  //Start on spawn point

        animator = GetComponent<Animator>();
        speed = maxSpeed;

        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAnimations();

        Respawner();
    }

    private void FixedUpdate()
    {
        GroundChecker();

        ManageSpeed(); // For allowing walking/running speed toggling

        ControlCharacter();
    }

    void GroundChecker()
    {
        onGround = Physics.Raycast(collider.bounds.center, Vector3.down, collider.bounds.extents.y + 0.1f);
    }


    void ControlCharacter()
    {
        // Movement/turning toggle for in air (on or off)
        if (airControl)  // Allow air control
        {
            MoveCharacter();
        }
        else // Don't allow air control
        {
            float forwardMotionStored;
            if (onGround)
            {
                MoveCharacter();
                forwardMotionStored = forward;
            }
            else
            {
                transform.Translate(new Vector3(0, 0, forward) * speed * Time.deltaTime);
            }
        }
    }

    void MoveCharacter()
    {
        // Forward motion (always enabled)
        forward = Input.GetAxis("Vertical");

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate((transform.up * turn) * turnSpeed * Time.fixedDeltaTime);

        // Move forward
        //rb.AddRelativeForce(new Vector3(0, 0, forward) * speed * Time.deltaTime, ForceMode.Impulse);
        transform.Translate(new Vector3(0, 0, forward) * speed * Time.deltaTime);

        speedVector = forward * speed;
    }


    void ManageSpeed()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            walk = true;
            speed = maxSpeed / 4;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            walk = false;
            speed = maxSpeed;
        }


        // Code to manage going backwards speeds
        if(speedVector < 0)
        {
            speed = maxSpeed / 4;
            recentlyGoingBackwards = true;
        }
        if(speedVector >= 0 && recentlyGoingBackwards)
        {
            recentlyGoingBackwards = false;
            if(walk)
            {
                speed = maxSpeed / 4;
            }
            else
            {
                speed = maxSpeed;
            }
        }


    }

    void ManageAnimations()
    {
        animator.SetFloat("Running", speedVector); // Manages animation state for forward motion

        // Jumping
        if(Input.GetKeyDown(KeyCode.Space) && speedVector > 2 && onGround)
        {
            animator.SetTrigger("Jump");
            // Apply physics
            rb.AddForce(0, jumpForce, 0);
        }

    }

    void Respawner()
    {
        if(transform.position.y < -10)
        {
            transform.position = spawnPoint.transform.position;
            transform.localRotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        if(freezeRotation)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    public void PlayDanceAnimation()
    {
        animator.Play("Dance", 0, 0);
    }

}
