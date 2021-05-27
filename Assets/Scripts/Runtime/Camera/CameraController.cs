using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Player;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;

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

        transform.position += offset * -Input.GetAxis("Mouse ScrollWheel");
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = -Input.GetAxis("Mouse Y");
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
        transform.RotateAround (target.position,new Vector3(0.0f,1.0f,0.0f),horizontal * Time.deltaTime * rotationSpeed);
        transform.RotateAround(target.position, axis, vertical * Time.deltaTime * rotationSpeed);
        
    }
}