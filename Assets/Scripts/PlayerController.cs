using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField, Tooltip("rigidbody of a player")] private Rigidbody _rb;
    [SerializeField] private float _movementSpeed;
    [SerializeField, Tooltip("2d vector of input")] private Vector2 _direction;

    [Space]
    [SerializeField] private float _turnSmoothTime;
    [SerializeField] private float _turnSmoothVelocity;
    [SerializeField] private float _jumpHeight;

    private bool _isJumping;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _inputReader.MoveEvent += HandleMovement;
        _inputReader.JumpEvent += HandleJump;
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            //float smoothing = Mathf.SmoothDampAngle(_rb.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _rb.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            _rb.AddForce(_direction.magnitude * moveDirection * _movementSpeed, ForceMode.Force);
            //_rb.velocity = _direction.magnitude * _movementSpeed * moveDirection;
        }
    }

    private void Jump()
    {
        if (_isJumping)
        {
            _rb.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            _isJumping = false;
        }
    }

    private void HandleMovement(Vector2 dir)
    {
        _direction = dir;
    }

    private void HandleJump()
    {
        _isJumping = true;
    }
}