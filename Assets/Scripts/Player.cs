using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Mouse.current.leftButton.isPressed == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.localPosition.z;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = mousePos;
        }
    }
}
