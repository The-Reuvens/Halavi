using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Obstacale : MonoBehaviour
{
    [SerializeField] private bool slowMotion = true;
    [SerializeField] private short weightFactor;
    [SerializeField] private string audioClipID;
    [SerializeField] private string collisionAudioClipID;
    private Rigidbody rb;

    private bool hasEnteredVision = false;
    private bool hasSlowMotioned = false;
    private readonly float Buffer = 5;

    private void Start()
    {
        if (!GetComponent<BoxCollider>().isTrigger)
        {
            throw new Exception("Obstacale's box collider must be a trigger!");
        }

        rb = GetComponent<Rigidbody>();
    }

    private async void FixedUpdate()
    {
        var player = GameManager.Instance.Player;

        if (!slowMotion && !hasEnteredVision && Vector3.Distance(player.transform.position, transform.position) <= 300)
        {
            hasEnteredVision = true;
            //TODO: Play Audio based on clipID - Longlasting obstacale sound
            // FMODUnity.RuntimeManager.PlayOneShot($"event:/${audioClipID}", transform.position);
        }
        else if (
            slowMotion &&
            !hasSlowMotioned &&
            transform.position.y >= player.transform.position.y - Buffer &&
            transform.position.y <= player.transform.position.y + Buffer
        )
        {
            hasSlowMotioned = true;

            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = player.GetVelocity() * 0.9f;

            player.FollowSpeed = 100;

            //TODO: Play Audio based on clipID - One shot obstacale sound
            // FMODUnity.RuntimeManager.PlayOneShot($"event:/${audioClipID}", transform.position);

            await Task.Delay(GameManager.Instance.SlowMotionDurationInMS);

            player.FollowSpeed = 13;

            if (rb)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            //TODO: Play Audio based on clipID - Collision
            // FMODUnity.RuntimeManager.PlayOneShot($"event:/${collisionAudioClipID}", transform.position);
            GameManager.Instance.WeightManager.Weight += weightFactor;
            Destroy(gameObject);
        }
    }
}