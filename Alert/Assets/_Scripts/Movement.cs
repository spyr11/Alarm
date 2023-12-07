using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _walkSpeed;

    private readonly int Speed=Animator.StringToHash(nameof(Speed));
    private RaycastHit _raycastHit;
    private Vector3 _targetPosition;
    private float _currentSpeed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _targetPosition.z = transform.position.z;
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        SetNewPosition();
        SetSpeed();
        MoveNewPosition();
    }

    private void SetNewPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _raycastHit))
            {
                _targetPosition.x = _raycastHit.point.x;
                _targetPosition.y = transform.position.y;
                _targetPosition.z = _raycastHit.point.z;
            }
        }
    }

    private void MoveNewPosition()
    {
        transform.LookAt(_targetPosition);
        _rigidbody.velocity = transform.forward * _currentSpeed;

        _animator.SetFloat(Speed, _currentSpeed);
    }

    private void SetSpeed()
    {
        float offset = 0.05f;

        if (Mathf.Abs(transform.position.z - _targetPosition.z) < offset)
        {
            _currentSpeed = 0;
        }
        else
        {
            _currentSpeed = _walkSpeed;
        }
    }
}