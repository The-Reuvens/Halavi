using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackBackground;

    void Start()
    {
        blackBackground.LeanAlpha(0, 3f).setEaseInCirc();
    }

    public void StartGame()
    {
        blackBackground.LeanAlpha(1, 2).setOnComplete(() =>
        {
            SceneManager.LoadScene((int)Scene.INSTRUCTIONS);
        });
    }
}
