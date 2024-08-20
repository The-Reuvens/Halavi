using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup.LeanAlpha(1, 2f).setEaseInCirc().setOnComplete(() =>
        {
            canvasGroup.interactable = true;
        });
    }

    public void StartGameLoop()
    {
        SceneManager.LoadScene((int)Scene.GAME);
    }
}
