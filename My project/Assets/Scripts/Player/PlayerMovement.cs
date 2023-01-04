using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float _moveSpeed = 5f;
    public float MaxMoveSpeed { get; private set; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private readonly string _horizontal = "Horizontal";
    private readonly string _vertical = "Vertical";

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        MaxMoveSpeed = _moveSpeed;
        MoveSpeed = MaxMoveSpeed;
    }

    private void Update()
    {
        Vector3 offsetPosition = new Vector3(Input.GetAxisRaw(_horizontal), 0f, Input.GetAxisRaw(_vertical)).normalized;
        Vector3 nextPosition = MoveSpeed * Time.deltaTime * offsetPosition + _rigidBody.position;

        _rigidBody.MovePosition(nextPosition);
    }
}
