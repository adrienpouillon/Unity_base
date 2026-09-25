using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveComponent : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] bool _useForward;
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;

    [Header("Component required")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] GameObject _foodPosition;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _player;
    [SerializeField] Animator _animator;

    bool _isGrounded;
    Vector3 _direction;
    

    void Reset()
    {
        _jumpForce = 1000;
        _useForward = false;
        _speed = 1;
        _rb = GetComponent<Rigidbody>();
        _moveInput = GetComponent<InputActionReference>();
        _camera = GetComponent<Camera>();
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        _isGrounded = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveInput.action.started += UpdateMoveInput;
        _moveInput.action.performed += UpdateMoveInput;
        _moveInput.action.canceled += EndMoveInput;
    }

    // Update is called once per frame
    void Update()
    {
        //_isGrounded = Physics.Raycast(_foodPosition.transform.position, Vector3.down, 1f);// , 10, QueryTriggerInteraction.Collide);
        if(_jumpInput.action.WasPressedThisFrame())// && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce);
        }

        Vector3 cameraForward = _camera.transform.forward;
        cameraForward.y = 0f;
        var cameraRight = _camera.transform.right;

        if (_useForward)
        {
            _player.transform.Translate(transform.forward * (Time.deltaTime * _speed));
        }
        else
        {
            var dir = (cameraForward * _direction.z) + (cameraRight * _direction.x);
            _player.transform.Translate(dir * (Time.deltaTime * _speed), Space.Self);

            //bool isWalking = dir.magnitude > 0.1f;
            //_animator.SetBool("isWalking", false);
        }

        // Rotation du personnage
        _player.transform.forward = cameraForward;
    }

    /*void FixedUpdate()
    {
        _rb.AddForce(10, 0, 0);
    }*/

    void OnDestroy()
    {
        _moveInput.action.started -= UpdateMoveInput;
        _moveInput.action.canceled -= EndMoveInput;
    }

    void UpdateMoveInput(InputAction.CallbackContext obj)
    {
        var inputDirection = obj.ReadValue<Vector2>();
        _direction = new Vector3(inputDirection.x, 0f, inputDirection.y);
    }

    void EndMoveInput(InputAction.CallbackContext obj)
    {
        _direction = Vector3.zero;
    }

}

