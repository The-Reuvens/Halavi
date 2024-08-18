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
            Vector3 targetPos;
            Vector3 mousePos = Mouse.current.position.value;
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.value.x, Mouse.current.position.value.y, -Camera.main.transform.position.z));
            targetPos.z = transform.position.z;
            transform.position = targetPos;
        }
    }
}
