using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [SerializeField] private float mouseSensitivity = 1f;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateMouseLook();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //rotate player body
        transform.Rotate(Vector3.up, mouseDelta.x * mouseSensitivity);

        //rotate camera
        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);

    }
}
