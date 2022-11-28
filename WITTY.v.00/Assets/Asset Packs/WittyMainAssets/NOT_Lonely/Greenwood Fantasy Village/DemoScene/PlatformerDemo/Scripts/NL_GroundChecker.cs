using UnityEngine;

public class NL_GroundChecker : MonoBehaviour
{
    public float sphereCastRadius = 0.5f;
    public float rayLength = 0.01f;
    public bool isGrounded = true;

    void FixedUpdate()
    {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, sphereCastRadius, Vector3.down, out hit, rayLength);
    }
}
