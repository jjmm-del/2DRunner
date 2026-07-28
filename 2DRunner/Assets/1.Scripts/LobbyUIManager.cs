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
    }
}
