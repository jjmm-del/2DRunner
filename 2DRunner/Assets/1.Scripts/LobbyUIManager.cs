using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LobbyUIManager : MonoBehaviour
{
    [Header("캐릭터 정보 텍스트 UI")]
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _speedText;
    
    [Header("플레이어 기록 텍스트 UI")]
    [SerializeField] private TextMeshProUGUI _highestScoreText;
    
    [Header("캐릭터 시각 UI")]
    [Tooltip("캐릭터 이미지를 띄울 Image")]
    [SerializeField] private Image _characterDisplayImage;
    [Tooltip("캐릭터 이미지에 애니메이션을 재생할 Animator")]
    [SerializeField] private Animator _characterAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float highestScore = DataManager.Instance.CurrentPlayerData.HighestScore;

        _highestScoreText.text = $"최고 점수 : {Mathf.RoundToInt(highestScore)}점";
    }

    public void UpdateCharacterInfo(CharacterData characterData)
    {
        if (characterData != null)
        {
            _characterNameText.text = characterData.CharacterName;

            _healthText.text = $"체력: {characterData.BaseHealth}";
            _speedText.text = $"추가 속도: + {characterData.BonusMoveSpeed}";
        }

        if (_characterDisplayImage != null && characterData.CharacterSprite != null)
        {
            _characterDisplayImage.sprite = characterData.CharacterSprite;
        }

        if (_characterAnimator != null && characterData.CharacterAnimator != null)
        {
            _characterAnimator.runtimeAnimatorController = characterData.CharacterAnimator;
        }
    }
}
