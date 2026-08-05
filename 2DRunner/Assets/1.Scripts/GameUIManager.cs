using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour
{
    [Header("UI요소 연결")]
    [Tooltip("현재 점를 표시할 텍스트")]
    [SerializeField]private TextMeshProUGUI _scoreText;
    [Tooltip("체력바 역할을 할 UIImage")]
    [SerializeField]private Image _hpBarFillImage;
    [Tooltip("HP 텍스트")]
    [SerializeField]private TextMeshProUGUI _hpText;
    
    [Header("게임오버UI")]
    [Tooltip("게임 오버 시 중앙에 나타날 텍스트")]
    [SerializeField]private GameObject _gameOverTextObject;
    
    
    [Header("데이터 소스 연결")]
    [Tooltip("점수 이벤트를 발생시키는 매니저")]
    [SerializeField]private ScoreManager _scoreManager;
    [Tooltip("체력 이벤트를 발생 시키는 플레이어의 Health스크립트")]
    [SerializeField]private PlayerHealth _playerHealth;

    private void Start()
    {
        if (_gameOverTextObject != null)
        {
            _gameOverTextObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (_scoreManager != null)
        {
            _scoreManager.OnScoreChanged += UpdateScoreUI;
        }

        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged += UpdateHPUI;
            _playerHealth.OnDied += ShowGameOverUI;
        }
    }

    private void OnDisable()
    {
        if (_scoreManager != null)
        {
            _scoreManager.OnScoreChanged -= UpdateScoreUI;
        }

        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= UpdateHPUI;
        }
    }

    private void UpdateScoreUI(float currentScore)
    {
        _scoreText.text = $"점수 : {Mathf.FloorToInt(currentScore)}";
    }

    private void UpdateHPUI(float currentHealth, float maxHealth)
    {
        _hpText.text = $"{currentHealth}/{maxHealth}";
        float healthRatio = currentHealth / maxHealth;
        _hpBarFillImage.fillAmount = healthRatio;
    }

    private void ShowGameOverUI()
    {
        if (_gameOverTextObject != null)
        {
            _gameOverTextObject.SetActive(true);
        }
    }
}
