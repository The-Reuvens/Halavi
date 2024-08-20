
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    [SerializeField] private Transform bg;
    [SerializeField] private CanvasGroup blackBackground;
    [SerializeField] private CanvasGroup content;

    void Start()
    {
        bg.LeanScale(1.05f * Vector3.one, 2).setEaseInCirc().setOnComplete(() =>
        {
            content.gameObject.SetActive(true);
            content.LeanAlpha(1, 1f);
        }).delay = 0.5f;
        blackBackground.LeanAlpha(0, 3f).setEaseInCirc().delay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        bg.transform.position = new(Random.Range(-0.005f, 0.005f), Random.Range(-0.005f, 0.005f), 0);
    }

    public void Quit()
    {
        blackBackground.LeanAlpha(1, 2).setOnComplete(() =>
        {
            SceneManager.LoadScene((int)Scene.MENU);
        });
    }

    public void TryAgain()
    {
        blackBackground.LeanAlpha(1, 2).setOnComplete(() =>
        {
            SceneManager.LoadScene((int)Scene.GAME);
        });
    }
}
