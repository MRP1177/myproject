using UnityEngine;
using System.Collections;

// Credit to https://sharpcoderblog.com/blog/easy-split-screen-multiplayer-in-unity-3d for script!

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class RigidbodyPlayerController : MonoBehaviour
{

    public enum PlayerControls { WASD, Arrows }
    public PlayerControls playerControls = PlayerControls.WASD;

    // Speed logic
    public float movementSpeed = 3f;
    float currentSpeed;
    public float rotationSpeed = 5f;

    // Jump logic
    bool isGrounded = false;
    float groundTuning = 0.1f;
    public float jumpAmount = 2.5f;
    float currentJumpAmount;

    Rigidbody r;
    float gravity = 10.0f;

    void Awake()
    {
        currentSpeed = movementSpeed;
        currentJumpAmount = jumpAmount;
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
        r.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Ground check
        CheckIfGrounded();

        // Move Front/Back
        Vector3 targetVelocity = Vector3.zero;
        if ((playerControls == PlayerControls.WASD && Input.GetKey(KeyCode.W)) || (playerControls == PlayerControls.Arrows && Input.GetKey(KeyCode.UpArrow)))
        {
            targetVelocity.z = 1;
        }
        else if ((playerControls == PlayerControls.WASD && Input.GetKey(KeyCode.S)) || (playerControls == PlayerControls.Arrows && Input.GetKey(KeyCode.DownArrow)))
        {
            targetVelocity.z = -1;
        }
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= currentSpeed;

        // Jump logic
        if (isGrounded)
        {
            if ((playerControls == PlayerControls.WASD && Input.GetKey(KeyCode.Space)) || (playerControls == PlayerControls.Arrows && Input.GetKey(KeyCode.RightControl)))
            {
                r.AddForce(Vector3.up * currentJumpAmount, ForceMode.Impulse);
            }
        }


        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = r.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        float maxVelocityChange = 10.0f;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;


        r.AddForce(velocityChange, ForceMode.VelocityChange);

        // We apply gravity manually for more tuning control
        r.AddForce(new Vector3(0, -gravity * r.mass, 0));


        // Rotate Left/Right
        if ((playerControls == PlayerControls.WASD && Input.GetKey(KeyCode.A)) || (playerControls == PlayerControls.Arrows && Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Rotate(new Vector3(0, -14, 0) * Time.deltaTime * rotationSpeed, Space.Self);
        }
        else if ((playerControls == PlayerControls.WASD && Input.GetKey(KeyCode.D)) || (playerControls == PlayerControls.Arrows && Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Rotate(new Vector3(0, 14, 0) * Time.deltaTime * rotationSpeed, Space.Self);
        }
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundTuning);
    }

    public void ChangeSpeed(float speedToAdd, float speedPowerupTime)
    {
        currentSpeed += speedToAdd;
        Invoke("ResetSpeed", speedPowerupTime);
    }

    void ResetSpeed()
    {
        currentSpeed = movementSpeed;
    }

    public void ChangeJumpHeight(float jumpHeightToAdd, float jumpPowerupTime)
    {
        Debug.Log("Before: " + currentJumpAmount);
        currentJumpAmount += jumpHeightToAdd;
        Debug.Log("After: " + currentJumpAmount);
        Invoke("ResetJump", jumpPowerupTime);
    }

    void ResetJump()
    {
        currentJumpAmount = jumpAmount;
    }
}