using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float cameraDistance = 2.0f;

    private PlayerInputs playerInputs;
    private Vector2 cameraInput;

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
        cameraInput = playerInputs.PlayerActionMap.RotateCamera.ReadValue<Vector2>();

        MoveCameraAroundPlayer(cameraInput);
        PlayerLookRotation(cameraInput);
    }

    private void MoveCameraAroundPlayer(Vector2 camMove)
    {
        camMove *= Time.deltaTime * 100 * rotationSpeed;

        transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x - camMove.y, 0, 80), transform.rotation.eulerAngles.y + camMove.x, 0);
        transform.position = player.position - transform.forward * cameraDistance;

        transform.LookAt(player);
    }

    private void PlayerLookRotation(Vector2 camMove)
    {
        camMove *= Time.deltaTime * 100 * rotationSpeed;
        player.Rotate(Vector3.up * camMove.x);
    }
}
