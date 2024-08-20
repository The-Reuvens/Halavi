
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    [SerializeField] private Transform bg;
    [SerializeField] private CanvasGroup blackBackground;

    void Start()
    {
        blackBackground.LeanAlpha(0, 10f).delay = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        bg.transform.position = new(Random.Range(-0.005f, 0.005f), Random.Range(-0.005f, 0.005f), 0);

    }
}
