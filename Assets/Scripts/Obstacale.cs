using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Obstacale : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool slowMotion = true;
    [SerializeField] private short weightFactor;
    [SerializeField] private string audioClipID;
    [SerializeField] private string collisionAudioClipID;
    private Rigidbody rb;

    private bool hasEnteredVision = false;
    private bool hasSlowMotioned = false;
    private readonly float Buffer = 5;
    private bool hasCollided;

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
        if (hasCollided) return;

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
            hasCollided = true;

            //TODO: Play Audio based on clipID - Collision
            // FMODUnity.RuntimeManager.PlayOneShot($"event:/${collisionAudioClipID}", transform.position);

            GameManager.Instance.WeightManager.Weight += weightFactor;

            if (name.StartsWith("Bird"))
            {
                rb.useGravity = true;
                rb.isKinematic = false;

                var bloodParticles = Instantiate(GameManager.Instance.BloodParticles, transform);
                bloodParticles.transform.localPosition = Vector3.zero;
                rb.velocity = GameManager.Instance.Player.GetVelocity() * 1.3f;
                LeanTween.alpha(gameObject, 0, 0.2f).delay = 1f;
                LeanTween.alpha(bloodParticles, 0, 0.2f).delay = 1f;
            }
            else
            {
                transform.LeanScale(1.2f * Vector3.one, 0.3f).setEaseOutCirc();
                LeanTween.alpha(transform.gameObject, 0, 0.3f).setEaseOutCirc().setOnComplete(() => Destroy(gameObject));
            }
        }
    }
}