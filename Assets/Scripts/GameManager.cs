using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Header("------- UI -------")]
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private TMP_Text counter;

    [Header("------- Modifiers -------")]
    public int SlowMotionDurationInMS = 200;

    [SerializeField] private float endSpwanRate = 40;
    [SerializeField][Range(0.01f, 0.2f)] private float amountPerSpawnChangeRate = 0.1f;
    [SerializeField] private float maxAmountPerSpawn = 4;
    [SerializeField] private float foodEndSpawnChance = 0.2f;
    [SerializeField][Range(0, 0.1f)] private float foodEndSpawnChanceChangeRate = 0.005f;
    [SerializeField] private float distanceFromFirstSpawn = 200;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance = this;
    }

    private async void Start()
    {
        await Task.Delay(1000);

        for (int i = 2; i >= 0; i--)
        {
            //TODO: Play counter audio
            counter.SetText($"{i}");
            await Task.Delay(1000);
        }

        blackScreen.alpha = 0;
        Destroy(counter);

        var playerContainerRb = Player.transform.parent.GetComponent<Rigidbody>();
        playerContainerRb.useGravity = true;
        playerContainerRb.isKinematic = false;

        Vector3 playerContainerStartingPosition = Player.transform.parent.transform.position;
        WeightManager = GetComponent<WeightManager>();

        for (float y = 100; y <= playerContainerStartingPosition.y - distanceFromFirstSpawn; y += endSpwanRate + (float)OMath.RandomDouble(-50, 50), endSpwanRate += endSpwanRate > 30 ? 1.5f : 0f, foodEndSpawnChance += foodEndSpawnChanceChangeRate, maxAmountPerSpawn -= maxAmountPerSpawn - amountPerSpawnChangeRate < 1 ? 0 : amountPerSpawnChangeRate)
        {
            bool isEnemy = OMath.rnd.NextDouble() >= foodEndSpawnChance;
            var obstacalePool = isEnemy ? enemyPrefabs : foodPrefabs;

            Vector3 previousObstaclePosition = Vector3.back;

            for (ushort amount = 1; amount <= OMath.rnd.Next(1, (int)maxAmountPerSpawn); amount++)
            {
                print(maxAmountPerSpawn);
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

    public async void Win()
    {
        blackScreen.alpha = 1;

        //TODO: Play win sound and set length
        int soundLengthInMS = 0;

        await Task.Delay(soundLengthInMS);

        SceneManager.LoadScene((int)Scene.WIN);
    }

    public async void Lose()
    {
        blackScreen.alpha = 1;

        //TODO: Play death sound and set length
        int soundLengthInMS = 0;

        await Task.Delay(soundLengthInMS);

        SceneManager.LoadScene((int)Scene.GAMEOVER);
    }
}