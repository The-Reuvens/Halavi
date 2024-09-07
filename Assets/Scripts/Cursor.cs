using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomCursor : MonoBehaviour
{
    public Texture2D IdleTex, ClickTex;
    private Texture2D currentTexture;

    void Start()
    {
        Cursor.visible = false;
        Cursor.SetCursor(IdleTex, Vector2.zero, CursorMode.Auto);
        currentTexture = IdleTex;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Cursor.visible) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && currentTexture != ClickTex)
        {
            Cursor.SetCursor(ClickTex, Vector2.zero, CursorMode.Auto);
            currentTexture = ClickTex;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame && currentTexture != IdleTex)
        {
            Cursor.SetCursor(IdleTex, Vector2.zero, CursorMode.Auto);
            currentTexture = IdleTex;
        }
    }
}
