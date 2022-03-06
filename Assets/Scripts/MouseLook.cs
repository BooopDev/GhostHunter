using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(0.1f,5)]
    public float mouseSensitivity;
    public Transform player;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameOver.hasGameEnded)
        {
            HeadRotate();
        }
    }

    void HeadRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity * 100) * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity * 100) * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up, mouseX);
    }

}
