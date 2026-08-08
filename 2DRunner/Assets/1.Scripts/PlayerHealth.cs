using UnityEngine;
using System;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    private bool _isInvincible;
    
    [Header("피격 설정")]
    [SerializeField]private float _invincibilityDuration = 1.0f;

    private Color _hitColor = Color.red;
    private float _blinkInterval = 0.1f;
    private SpriteRenderer _spriteRenderer;
    public event Action<float, float> OnHealthChanged;
    public event Action OnDamaged;
    public event Action OnDied;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void InitializeHealth(float characterMaxHealth)
    {
        _maxHealth = characterMaxHealth;
        _currentHealth = _maxHealth;
        _isInvincible = false;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = Color.white;
        }
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        Debug.Log($"플레이어 체력 세팅 완료:{_maxHealth}");
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth <= 0f || _isInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        OnDamaged?.Invoke();
        
        Debug.Log($"플레이어 피격! 현재 체력: {_currentHealth}/{_maxHealth}");
        
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayHit();
        }
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Die();
        }
        else
        {
            float healthRatio = _currentHealth / _maxHealth;
            if (healthRatio < 0.2f && SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayDangerBGM();
            }
            StartCoroutine(InvincibilityRoutine());
            
        }
    }

    private void Die()
    {
        Debug.Log("체력이 0, 사망");
        OnDied?.Invoke();
    }

    private IEnumerator InvincibilityRoutine()
    {
        _isInvincible = true;

        float elapsedTime = 0f;
        Color originColor = Color.white;
        while (elapsedTime < _invincibilityDuration)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _hitColor;
            }
            yield return new WaitForSeconds(_blinkInterval);
            elapsedTime += _blinkInterval;
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = originColor;
            }
            yield return new WaitForSeconds(_blinkInterval);
            elapsedTime += _blinkInterval;
        }

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = originColor;
        }
        // 무적 시각 효과 추가
        
        _isInvincible = false;
    }
}
