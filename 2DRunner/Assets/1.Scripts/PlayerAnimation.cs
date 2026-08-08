using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [Header("컴포넌트 연결")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHealth _playerHealth;
    
    [Header("피격 애니메이션")]
    [SerializeField] private float _hitStopTime;
    
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnDamaged += TriggerHitStop;
        }
        
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnDamaged -= TriggerHitStop;
            
        }
    }

    private void TriggerHitStop()
    {
        StartCoroutine(HitStopRoutine());
    }

    private IEnumerator HitStopRoutine()
    {
        _animator.speed = 0;
        yield return new WaitForSeconds(_hitStopTime);
        _animator.speed = 1;
    }

    
}
