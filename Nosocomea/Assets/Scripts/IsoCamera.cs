using UnityEngine;

public class IsoCamera : MonoBehaviour
{
    //[HideInInspector]
    public Transform target;
    public Transform player;

    public Vector3 offset;
    public float moveSpeed = 5f;

    void FixedUpdate()
    {
        // Move the camera to follow the target object
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

        // Adjust the camera's height to keep the target object in view
        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, target.position.y), transform.position.z);

        // Allow the camera to move left, right, up, and down
        if (target == player)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            transform.position += new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        }      
    }

    public void ResumeControl()
    {
        target = player;
    }
}
