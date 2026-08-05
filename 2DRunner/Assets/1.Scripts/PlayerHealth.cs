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
    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    public void InitializeHealth(float characterMaxHealth)
    {
        _maxHealth = characterMaxHealth;
        _currentHealth = _maxHealth;
        _isInvincible = false;
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
        Debug.Log($"플레이어 피격! 현재 체력: {_currentHealth}/{_maxHealth}");

        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Die();
        }
        else
        {
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
        // 무적 시각 효과 추가
        yield return new WaitForSeconds(_invincibilityDuration);
        _isInvincible = false;
    }
}
