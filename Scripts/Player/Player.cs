using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField] private float _moving;

    private Animator _animator;

    private Vector3 _velocity;

    private Rigidbody _rb;

    private Vector3 movement;

    private SpriteRenderer _spriteRenderer;

    private Vector2 _movementInput;

    private SpriteRenderer _toolSprRdr;

    [SerializeField] private Transform _attackPoint;

    public GameObject _tool;

    private float flipspeed = 5;

    public float xFacing;

    [SerializeField] private bool isGrounded; 

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Player Animator not found");
        }

        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody 2D not found");
        }

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Mesh renderer not found");
        }

        _toolSprRdr = _tool.GetComponent<SpriteRenderer>();
        if (_toolSprRdr == null)
        {
            Debug.LogError("Tool's Sprite Renderer from player script cannot be found");
        }
        isGrounded = false;
        xFacing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving == 0 && isGrounded == true) 
        {
            _rb.velocity = Vector3.zero;
        }

        _animator.SetFloat("Xaxis", movement.x);
        _animator.SetFloat("Zaxis", movement.z);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * _speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();

        movement.x = _movementInput.x;
        movement.z = _movementInput.y;

        if (_movementInput.x > 0)
        {
            xFacing = 1;
        }
        else if (_movementInput.x < 0)
        {
            xFacing = -1;
        }

        _moving = movement.sqrMagnitude;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
