using System.Diagnostics.CodeAnalysis;
using System.Threading;
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
    // baloon mode variables
    private bool isInBaloonMode = false;
    private float baloonModeDuration = 1;
    private float timeInBaloonMode = 0;
    [SerializeField] private float baloonModeFollowSpeed = 1 / 3;
    private readonly float borderX = 19.2f;
    private readonly float borderZ = 9.85f;
    private float randomPositionX = 0;
    private float randomPositionZ = 0;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        playerContainerRB.velocity = new Vector3(0, startingVelocityY, 0);
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 targetPosition = Input.mousePosition;
            targetPosition.z = -Camera.main.transform.localPosition.z;
            targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
            //this line v makes you move to a random location if you are in baloon mode
            transform.position = Vector3.Lerp(transform.position, !isInBaloonMode? targetPosition : new Vector3(randomPositionX, transform.position.y, randomPositionZ), !isInBaloonMode? FollowSpeed * Time.deltaTime : FollowSpeed * baloonModeFollowSpeed * Time.deltaTime);
        }

        /*if current y velocity is above max velocity,cancel out gravity by adding an equal force the opposite direction*/
        if (playerContainerRB.velocity.y < maxVelocityY)
        {
            playerContainerRB.AddForce(-Physics.gravity);
        }

        if (isInBaloonMode == true) {
            timeInBaloonMode += Time.deltaTime;
            if (timeInBaloonMode > baloonModeDuration) {
                timeInBaloonMode = 0;
                //TODO: add player recovery from baloon mode sound effect here v

                if(isDragging == false)
                {
                    isInBaloonMode = false;
                }
            }
        }

        /*if player is hurt, enter baloon mode*/
        if (Mouse.current.rightButton.isPressed && isInBaloonMode == false)
        {
            OnPlayerHurt();
        }
    }
    private void OnPlayerHurt()
    {
        isInBaloonMode = true;
        // TODO: add player hurt sound (enter baloon mode) effect here v

        GenerateRandomPosition();
    }

    private void GenerateRandomPosition()
    {
        randomPositionX = Random.Range(-borderX, borderX);
        randomPositionZ = Random.Range(-borderZ, borderZ);
    }

    private void OnMouseDrag() {
        if (isInBaloonMode == false) { 
            isDragging = true; 
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }
}
