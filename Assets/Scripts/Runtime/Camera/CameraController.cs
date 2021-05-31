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
        float horizontal = Input.GetAxis("Mouse X");

        transform.LookAt(target);

        transform.RotateAround (target.position,new Vector3(0.0f,1.0f,0.0f),horizontal * Time.deltaTime * rotationSpeed);

    }
}