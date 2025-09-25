using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 50;
    Transform playerBody;
    float pitch = 0;

    void Start()
    {
        //references slimon
        playerBody = transform.parent.transform;

        //invisible and locked mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //user input mouse movement
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yaw (left/right)
        playerBody.Rotate(Vector3.up * moveX); 

        //pitch (up/down)
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
