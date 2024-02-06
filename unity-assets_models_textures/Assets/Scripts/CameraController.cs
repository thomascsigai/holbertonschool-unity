using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float cameraDistance = 2.0f;

    private PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.PlayerActionMap.Enable();
    }

    private void OnDisable()
    {
        playerInputs.PlayerActionMap.Disable();
    }

    private void LateUpdate()
    {
        MoveCameraAroundPlayer();
    }

    private void MoveCameraAroundPlayer()
    {
        Vector2 camMove = playerInputs.PlayerActionMap.RotateCamera.ReadValue<Vector2>();

        camMove *= Time.deltaTime * 100 * rotationSpeed;

        transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x - camMove.y, 0, 80), transform.rotation.eulerAngles.y + camMove.x, 0);
        transform.position = player.position - transform.forward * cameraDistance;

        transform.LookAt(player);
    }
}
