using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Player;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 250.0f;
    [SerializeField] private float lookXSensitivity = 2;
    [SerializeField] private float lookYSensitivity = 1.5f;
    [SerializeField] private float zoomSensitivity = 3;

    [SerializeField] private float minZoom = 5;
    [SerializeField] private float maxZoom = 35;

    private void Start()
    {
        PlayerMovement.PlayerMoveEvent.AddListener(OnPlayerMove);
    }


    private void OnPlayerMove(Vector3 moveDist)
    {
        transform.position += moveDist;
    }

    private void LateUpdate()
    {
        Vector3 offset = transform.position - target.position;
        float offsetLength = offset.magnitude;

        Vector3 axis = offset;

        axis.y = 0;
        axis = Quaternion.Euler(0, -90, 0) * axis;
        axis.Normalize();
        offset.Normalize();

        CameraZoom(offset, offsetLength);

        float horizontal = Input.GetAxis("Mouse X") * UserSettings.lookXSensitivity;
        float vertical = Mathf.Clamp(-Input.GetAxis("Mouse Y") * UserSettings.lookYSensitivity, -3f, 3f);
        float angle = transform.rotation.eulerAngles.x;

        if (angle > 60)
        {
            vertical = Mathf.Clamp(vertical, -10f, 0f);
        }
        if (angle < 30)
        {
            vertical = Mathf.Clamp(vertical, 0f, 10f);
        }

        transform.LookAt(target);
        transform.RotateAround (target.position,new Vector3(0.0f,1.0f,0.0f),horizontal * Time.deltaTime * UserSettings.CameraRotationSpeed);
        transform.RotateAround(target.position, axis, vertical * Time.deltaTime * UserSettings.CameraRotationSpeed);
    }


    private void CameraZoom(Vector3 offset, float offsetLength)
    {
        float CameraInput = -Input.GetAxis("Mouse ScrollWheel");
        if (CameraInput < 0 && offsetLength < minZoom) CameraInput = 0;
        if (CameraInput > 0 && offsetLength > maxZoom) CameraInput = 0;
        transform.position += offset * (CameraInput * UserSettings.cameraZoomSensitivity);
    }
}