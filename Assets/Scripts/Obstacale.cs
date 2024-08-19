using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacale : MonoBehaviour
{
    [SerializeField] private ushort weightFactor;
    [SerializeField] private string clipID;

    private void Start()
    {
        if (!GetComponent<BoxCollider>().isTrigger)
        {
            throw new Exception("Obstacale's box collider must be a trigger!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            //TODO: Play Audio based on clipID
            // FMODUnity.RuntimeManager.PlayOneShot($"event:/${clipID}", transform.position);
            GameManager.Instance.WeightManager.Weight += weightFactor;
            Destroy(gameObject);
        }
    }
}