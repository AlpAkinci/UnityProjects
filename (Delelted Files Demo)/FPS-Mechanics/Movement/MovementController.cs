using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private float yaw = 0.0f, pitch = 0.0f;
    private Rigidbody rb;

    [SerializeField] float walkSpeed = 5.0f, runningSpeed = 10.0f, sensitivity = 2.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKey(KeyCode.Space) && Physics.Raycast(rb.transform.position, Vector3.down, 1 + 0.001f))
                rb.velocity = new Vector3(rb.velocity.x, 5.0f, rb.velocity.z);
            Look();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        pitch -= Input.GetAxisRaw("Mouse Y")* sensitivity;
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

    void Move()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * walkSpeed;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);
        Vector3 wishDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = wishDirection;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * runningSpeed;
            forward = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);
            wishDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
            rb.velocity = wishDirection;
        }
    }

}
