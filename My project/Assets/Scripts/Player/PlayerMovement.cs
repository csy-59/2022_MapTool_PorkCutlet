using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float _moveSpeed = 5f;
    public float MaxMoveSpeed { get; private set; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private float _horizontalInput;
    private float _verticalInput;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        MaxMoveSpeed = _moveSpeed;
        MoveSpeed = MaxMoveSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 offsetPosition = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;
        Vector3 nextPosition = MoveSpeed * Time.deltaTime * offsetPosition + _rigidBody.position;

        _rigidBody.MovePosition(nextPosition);
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw(_horizontal);
        _verticalInput = Input.GetAxisRaw(_vertical);
    }
}
