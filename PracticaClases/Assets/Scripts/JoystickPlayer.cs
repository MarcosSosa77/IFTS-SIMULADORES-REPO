using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed = 5f;
    public FixedJoystick Joystick;       // Movement joystick
    public FixedJoystick LookJoystick;   // Look joystick (right stick)
    public Rigidbody rb;
    public Collider playerCol;

    private bool isMoving = false;

    public static bool IsJoystickActive = false;

    // Rotation variables
    private float yaw = 0f;    // horizontal rotation (Y axis)
    private float pitch = 0f;  // vertical rotation (X axis)

    public float lookSensitivity = 3f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    private void Awake()
    {
        playerCol.enabled = false;
        rb.isKinematic = true;

        // Initialize yaw and pitch from current rotation
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void FixedUpdate()
    {

        Move();
        LookAround();
    }

   /* public void ToggleMovement(bool move)
    {
        isMoving = move;
        IsJoystickActive = move;
    }*/

    public void Move()
    {
        if (!isMoving)
            return;

        playerCol.enabled = true;
        rb.isKinematic = false;

        Vector3 input = new Vector3(Joystick.Horizontal, 0f, Joystick.Vertical);

        if (input.magnitude < 0.1f)
            return;

        // Get camera's forward and right on the horizontal plane
        Vector3 camForward = transform.forward;
        Vector3 camRight = transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * input.z + camRight * input.x;

        rb.AddForce(moveDirection * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    public void LookAround()
    {
        float lookX = LookJoystick.Horizontal;
        float lookY = LookJoystick.Vertical;

        if (Mathf.Abs(lookX) < 0.1f && Mathf.Abs(lookY) < 0.1f)
            return;

        yaw += lookX * lookSensitivity;
        pitch -= lookY * lookSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Apply rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }


    public void ToggleMovement(bool move)
    {
        isMoving = move;
        IsJoystickActive = move;

        if (move)
        {
            playerCol.enabled = true;
            rb.isKinematic = false;
        }
        else
        {
            playerCol.enabled = false;
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero; // Stop any ongoing movement
        }
    }

}
