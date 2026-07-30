using UnityEngine;
using System;
public class PlayerHealth : MonoBehaviour
{
    [Header("체력 설정")]
    [Tooltip("플레이어의 기본 최대 체력")]
    [SerializeField] private float _maxHealth;

    private float _currentHealth;
    
    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    public void InitializeHealth(float characterMaxHealth)
    {
        _maxHealth = characterMaxHealth;
        _currentHealth = _maxHealth;
        
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        Debug.Log($"플레이어 체력 세팅 완료:{_maxHealth}");
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth <= 0f)
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
    }

    private void Die()
    {
        Debug.Log("체력이 0, 사망");
        OnDied?.Invoke();
    }
}
