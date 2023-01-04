using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    public float MoveSpeed { get; set; }
    Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 nextPosition = _rigidBody.position;

        if (Input.GetKey(KeyCode.D))
        {
            nextPosition.x += _moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            nextPosition.x -= _moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            nextPosition.z += _moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            nextPosition.z -= _moveSpeed * Time.deltaTime;
        }

        _rigidBody.MovePosition(nextPosition);
    }
}
