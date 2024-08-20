using System;
using UnityEngine;

[RequireComponent(typeof(WeightManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("------- Managers -------")]
    public Player Player;
    public WeightManager WeightManager { get; private set; }

    [Header("------- GameObjects -------")]
    [SerializeField] private Transform obstacalesContainer;
    [SerializeField] private GameObject[] foodPrefabs;
    [SerializeField] private GameObject[] enemyPrefabs;
    public GameObject BloodParticles;

    [Header("------- Modifiers -------")]
    public int SlowMotionDurationInMS = 200;

    [SerializeField] private float EndSpwanRate = 40;
    [SerializeField] private float MaxAmountPerSpawn = 4;
    [SerializeField] private float FoodEndSpawnChance = 0.2f;

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

        for (float y = 100; y <= 11500; y += EndSpwanRate + (float)OMath.RandomDouble(-50, 50), EndSpwanRate += EndSpwanRate > 30 ? 1.5f : 0f, FoodEndSpawnChance += 0.005f, MaxAmountPerSpawn -= MaxAmountPerSpawn - 0.1f < 1 ? 0 : 0.1f)
        {
            bool isEnemy = OMath.rnd.NextDouble() >= FoodEndSpawnChance;
            var obstacalePool = isEnemy ? enemyPrefabs : foodPrefabs;

            Vector3 previousObstaclePosition = Vector3.back;

            for (ushort amount = 1; amount <= OMath.rnd.Next(1, 4); amount++)
            {
                Vector3 position = new(OMath.rnd.Next(-17, 17), y, OMath.rnd.Next(-7, 7));

                while (previousObstaclePosition != Vector3.back && Vector3.Distance(position, previousObstaclePosition) < 9)
                {
                    position = new(OMath.rnd.Next(-17, 17), y, OMath.rnd.Next(-7, 7));
                };

                var obstacle = Instantiate(
                    obstacalePool[OMath.rnd.Next(0, obstacalePool.Length)],
                    position,
                    Quaternion.Euler(new Vector3(90, 0, 0)),
                    obstacalesContainer
                );
                obstacle.SetActive(true);

                previousObstaclePosition = position;
            }
        }
    }
}
