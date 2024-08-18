using UnityEngine;

[RequireComponent(typeof(WeightManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player;
    public WeightManager WeightManager { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance = this;
    }

    private void Start()
    {
        WeightManager = GetComponent<WeightManager>();
    }
}
