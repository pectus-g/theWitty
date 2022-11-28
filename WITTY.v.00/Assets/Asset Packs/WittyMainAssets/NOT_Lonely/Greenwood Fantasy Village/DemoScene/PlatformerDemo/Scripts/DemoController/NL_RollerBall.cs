using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NL_GroundChecker))]
[RequireComponent(typeof(Rigidbody))]
public class NL_RollerBall : MonoBehaviour
{
    [HideInInspector] public Vector3 forwardDirection;
    private Vector3 movement;
    private Vector3 rightDirection;
    private Vector3 finalDirection;
    private Rigidbody rb;
    private NL_GroundChecker groundChecker;
    private bool jump = false;
    private float velocityMultiplier;

    public float drag = 3;
    public float acceleration = 300;
    public float maxSpeed = 6;
    public float jumpForce = 60;
    public float airModifier = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundChecker = GetComponent<NL_GroundChecker>();
    }

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        forwardDirection = Vector3.ProjectOnPlane(forwardDirection, Vector3.up).normalized;

        rightDirection = Quaternion.AngleAxis(90, Vector3.up) * forwardDirection;

        finalDirection = ((forwardDirection * movement.z) + (rightDirection * movement.x));

        if (groundChecker.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (groundChecker.isGrounded)
        {
            if (jump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jump = false;
            }

            rb.drag = drag;

            velocityMultiplier = 1;
        }
        else
        {
            rb.drag = 0;
            velocityMultiplier = airModifier;
        }

        rb.AddForce(finalDirection * (acceleration * velocityMultiplier), ForceMode.Force);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -150, 150), Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
    }
}
