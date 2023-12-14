using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensitivity = 2f;

    private FPSCamera fPSCamera;
    private float cameraVerticalRotation = 0f; 

    private void Awake()
    {
        fPSCamera = GetComponent<FPSCamera>();
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; 
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            fPSCamera.enabled = false;
        }
    }
    private void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
