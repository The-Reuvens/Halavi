using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    private bool isDragging = false;
    public float FollowSpeed = 13;
    private readonly float startingVelocityY = -100;
    private float maxVelocityY = -120;
    [SerializeField][NotNull] private Rigidbody playerContainerRB;
    public Animator Animator { get; private set; }

    private void Start()
    {
        Animator = GetComponent<Animator>();
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
        if (playerContainerRB.velocity.y < maxVelocityY)
        {
            playerContainerRB.AddForce(-Physics.gravity);
        }
    }

    private void OnMouseDrag() => isDragging = true;
    private void OnMouseUp() => isDragging = false;
    public Vector3 GetVelocity() => playerContainerRB.velocity;
}
