using UnityEngine;
using System;
public class ScoreManager : MonoBehaviour
{
    [Header("점수 설정")]
    [Tooltip("1초당 획득하는 거리 기본 점수")]
    [SerializeField] private float _scorePerSecond;
    
    private float _currentScore;
    private bool _isGameActive;
    
    public event Action<float> OnScoreChanged;

    private void Start()
    {
        _currentScore = 0f;
        _isGameActive = true;
    }

    private void Update()
    {
        if (_isGameActive)
        {
            float bonusSpeed = GameManager.Instance.CurrentBonusSpeed;
            float finalScorePerSecond = _scorePerSecond + bonusSpeed;
            
            _currentScore += finalScorePerSecond*Time.deltaTime;
            
            OnScoreChanged?.Invoke(_currentScore);
        }
    }

    public void StopScoreCalculation()
    {
        _isGameActive = false;

        if (_currentScore > DataManager.Instance.CurrentPlayerData.HighestScore)
        {
            DataManager.Instance.CurrentPlayerData.HighestScore = _currentScore;
            DataManager.Instance.SaveData();
            Debug.Log($"최고 점수 갱신 : {_currentScore}");
        }
    }

    public void AddScore(float amount)
    {
        _currentScore += amount;
        OnScoreChanged?.Invoke(_currentScore);
    }
}
