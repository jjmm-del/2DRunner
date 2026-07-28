using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("점프 설정")]
    [Tooltip("캐릭터가 점프하는 힘")]
    [SerializeField] private float _jumpForce = 15f;
    [Tooltip("최대 점프 가능 횟수")]
    [SerializeField] private int _maxJumpCount = 2;
    
    [Header("바닥 감지 설정")]
    [Tooltip("바당을 인식할 캐릭터 발밑 Transform")]
    [SerializeField]private Transform _groundCheckPoint;
    [Tooltip("바닥으로 인식할 레이어 설정")]
    [SerializeField] private LayerMask _groundLayer;
    [Tooltip("바닥을 감지할 원형 범위의 반지름")]
    [SerializeField] private float _groundCheckRadius = 0.2f;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private int _currentJumpCount;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        if (_isGrounded && !wasGrounded)
        {
            _currentJumpCount = 0;
        }
    }

    public void Jump()
    {
        if (_isGrounded || _currentJumpCount <= _maxJumpCount)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, 0);
            
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            
            _currentJumpCount++;
        }
    }
    

}
