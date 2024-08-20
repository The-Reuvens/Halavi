using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    private bool isDragging = false;
    public float FollowSpeed = 13;
    private readonly float startingVelocityY = -100;
    private float maxVelocityY = -120;
    [SerializeField][NotNull] private Rigidbody playerContainerRB;
    public Animator Animator { get; private set; }

    // Hurt Mode
    private bool isHurt;
    private bool lockDrag = false;

    [SerializeField] private Vector2 hurtModeCooldownRangeInSec = new(0.7f, 1.2f);
    [SerializeField] private float minHurtBounceDistance = 5;

    private readonly float[] BorderXRange = { -18.75f, 19.7f };
    private readonly float[] BorderYRange = { -10.8f, 8.8f };

    private void Start()
    {
        Animator = GetComponent<Animator>();
        playerContainerRB.velocity = new Vector3(0, startingVelocityY, 0);
    }

    void FixedUpdate()
    {
        if (isDragging && !isHurt && !lockDrag)
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

        /*if player is hurt, enter baloon mode*/
        if (Mouse.current.rightButton.isPressed && !isHurt)
        {
            Hurt();
        }
    }

    private void OnMouseDrag()
    {
        if (!lockDrag)
        {
            isDragging = true;
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
        if (lockDrag && !isHurt)
        {
            lockDrag = false;

            // Stop tween midway
            LeanTween.cancel(gameObject);
        }
    }

    public void Hurt()
    {
        isHurt = true;
        isDragging = false;
        lockDrag = true;

        //TODO: Change animator/sprite of player

        float duration = Random.Range(hurtModeCooldownRangeInSec.x, hurtModeCooldownRangeInSec.y);

        Vector3 newPosition = new(
            Random.Range(BorderXRange[0], BorderXRange[1]),
            Random.Range(BorderYRange[0], BorderYRange[1]),
            transform.localPosition.z
        );

        while (Vector3.Distance(newPosition, transform.localPosition) < minHurtBounceDistance)
        {
            newPosition = new(
                Random.Range(BorderXRange[0], BorderXRange[1]),
                Random.Range(BorderYRange[0], BorderYRange[1]),
                transform.localPosition.z
            );
        }

        transform.LeanMoveLocal(newPosition, duration).setEaseOutCirc().setOnComplete(() =>
        {
            isHurt = false;
            //TODO: Change back animator/sprite of player
        });
    }
}
