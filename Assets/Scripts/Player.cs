using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool isDragging = false;
    public float FollowSpeed = 13;
    private float startingVelocityY = -100;
    private float maxYVelocity = -120;
    [SerializeField] private Rigidbody playerContainerRB;

    private void Start()
    {        
        playerContainerRB.velocity = new Vector3(0, startingVelocityY, 0);
    }
    void FixedUpdate()
    {
        if (isDragging && Mouse.current.leftButton.isPressed)
        {
            Vector3 targetPosition = Input.mousePosition;
            targetPosition.z = -Camera.main.transform.localPosition.z;
            targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
            transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);
        }

        /*if current y velocity is above max velocity,cancel out gravity by adding an equal force the opposite direction*/
        print(playerContainerRB.velocity);
        if (playerContainerRB.velocity.y < maxYVelocity)
        {
            playerContainerRB.AddForce(-Physics.gravity);
        }
    }

    private void OnMouseDrag() => isDragging = true;
    private void OnMouseUp() => isDragging = false;
}
