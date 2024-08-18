using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool isMouseDrag = false;
    private bool movementEnabled = false;
    public float FollowSpeed = 100f;
    void Start()
    {

    }

    void FixedUpdate()
    {

        if (Mouse.current.leftButton.isPressed == true && isMouseDrag)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.localPosition.z;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = Vector3.Lerp(transform.position, mousePos, FollowSpeed * Time.deltaTime);
        }
    }
    private void OnMouseDrag()
    {
        isMouseDrag = true;
    }
    private void OnMouseUp()
    {
        isMouseDrag = false;
    }
}
