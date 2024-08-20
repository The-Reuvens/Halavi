using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField] private Transform bg;
    [SerializeField] private CanvasGroup blackBackground;
    [SerializeField] private Transform credits;

    void Start()
    {
        bg.LeanScale(1 * Vector3.one, 15f).setEaseOutCirc().delay = 0.5f;
        blackBackground.LeanAlpha(0, 3f).setEaseInCirc().delay = 0.5f;
        credits.LeanMoveLocalY(975f, 20).setOnComplete(() =>
        {
            blackBackground.LeanAlpha(1, 3).setOnComplete(() =>
            {
                SceneManager.LoadScene((int)Scene.MENU);
            }).delay = 1f;
        }).delay = 4.5f;
    }
}
