using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [Header("컴포넌트 연결")]
    [Tooltip("캐릭터의 이미지를 바꿔줄 SpriteRenderer")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [Tooltip("캐릭터의 애니메이션을 바꿔줄 Animator")]
    [SerializeField] private Animator _animator;
    
    [Tooltip("캐릭터의 체력을 세팅해 줄 PlayerHealth")]
    [SerializeField] private PlayerHealth _playerHealth;

    public void SetupCharacter(CharacterData data)
    {
        if (data == null)
        {
            Debug.LogError("캐릭터 데이터가 없습니다.");
            return;
        }

        if (_spriteRenderer != null && data.CharacterSprite != null)
        {
            _spriteRenderer.sprite = data.CharacterSprite;
        }

        if (_animator != null && data.CharacterAnimator != null)
        {
            _animator.runtimeAnimatorController = data.CharacterAnimator;
        }

        if (_playerHealth != null)
        {
            _playerHealth.InitializeHealth(data.BaseHealth);
        }
        Debug.Log($"{data.CharacterName} 캐릭터가 메인 씬에 성공적으로 세팅");
    }
    
}
