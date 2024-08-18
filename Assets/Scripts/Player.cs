using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool isDragging = false;
    public float FollowSpeed = 13;

    void FixedUpdate()
    {
        if (isDragging && Mouse.current.leftButton.isPressed)
        {
            Vector3 targetPosition = Input.mousePosition;
            targetPosition.z = -Camera.main.transform.localPosition.z;
            targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);

            transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDrag() => isDragging = true;
    private void OnMouseUp() => isDragging = false;
}
