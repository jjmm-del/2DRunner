using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("캐릭터 및 플레이어 설정")]
    [Tooltip("로비매니저에서 사용했던 캐릭터 데이터를 순서대로 넣습니다.")]
    [SerializeField] private CharacterData[] _availableCharacters;
    
    [Tooltip("메인 씬에 배치된 플레이어의 Setup스크립트")]
    [SerializeField] private PlayerSetup _playerSetup;

    [Tooltip("플레이어 죽음 이벤트를 감지하기 위해")]
    [SerializeField] private PlayerHealth _playerHealth;
    
    [Tooltip("게임 오버 시 점수 기록을정지 하기 위해")]
    [SerializeField] private ScoreManager _scoreManager;
    
    public float CurrentBonusSpeed { get; private set; }
    public bool IsGameOver { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        IsGameOver = false;
        ApplySelectedCharacter();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMainBGM();
        }
    }

    private void OnEnable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnDied += HandleGameOver;
        }
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnDied += HandleGameOver;
        }
    }
    
    private void ApplySelectedCharacter()
    {
        // 1. DataManager에서 유저가 마지막으로 선택한 캐릭터 ID를 가져옵니다.
        int lastUsedId = DataManager.Instance.CurrentPlayerData.LastUsedCharacterID;
        CharacterData selectedData = null;

        // 2. 배열에서 해당 ID를 가진 데이터를 찾습니다.
        for (int i = 0; i < _availableCharacters.Length; i++)
        {
            if (_availableCharacters[i].CharacterID == lastUsedId)
            {
                selectedData = _availableCharacters[i];
                break;
            }
        }

        // 혹시라도 데이터를 찾지 못했다면 안전을 위해 기본 캐릭터(0번)를 선택합니다.
        if (selectedData == null)
        {
            selectedData = _availableCharacters[0];
        }

        // 3. 추가 이동 속도를 저장해둡니다. (추후 MapController의 스크롤 속도 등에 더해줄 수 있습니다)
        CurrentBonusSpeed = selectedData.BonusMoveSpeed;

        // 4. 찾은 데이터를 바탕으로 플레이어를 조립합니다.
        _playerSetup.SetupCharacter(selectedData);
    }

    private void HandleGameOver()
    {
        IsGameOver = true;
        Debug.Log("게임 오버! 잠시 후 로비 씬으로 돌아갑니다.");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlayGameOver();
        }
        if (_scoreManager != null)
        {
            _scoreManager.StopScoreCalculation();
        }

        StartCoroutine(ReturnToLobbyRoutine());
    }

    private IEnumerator ReturnToLobbyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("0.Scenes/LobbyScene");
    }
}
