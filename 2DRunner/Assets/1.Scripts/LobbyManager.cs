using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [Header("캐릭터 데이터 설정")]
    [Tooltip("게임에서 사용할 수 있는 5가지 캐릭터의 데이터를 순서대로 넣습니다.")]
    [SerializeField] private CharacterData[] _availableCharacters;
    
    [Header("UIManager")]
    [Tooltip("UI업데이트 책임을 위임할 UI매니저")]
    [SerializeField] private LobbyUIManager _uiManager;
    
    private int _currentIndex = 0;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int lastUsedId = DataManager.Instance.CurrentPlayerData.LastUsedCharacterID;
        
        _currentIndex = FindCharacterIndexById(lastUsedId);
        CharacterData initialData = _availableCharacters[_currentIndex];
        _uiManager.UpdateCharacterInfo(initialData);
        
    }

    private int FindCharacterIndexById(int id)
    {
        for (int i = 0; i < _availableCharacters.Length; i++)
        {
            if (_availableCharacters[i].CharacterID == id)
            {
                return i;
            }
        }

        return 0;
    }

    public void SelectNextCharacter()
    {
        _currentIndex++;
        if (_currentIndex >= _availableCharacters.Length)
        {
            _currentIndex = 0;
        }

        UpdateLobbyState();
    }

    public void SelectPreviousCharacter()
    {
        _currentIndex--;
        if (_currentIndex < 0)
        {
            _currentIndex = _availableCharacters.Length - 1;
        }

        UpdateLobbyState();
    }

    private void UpdateLobbyState()
    {
        CharacterData selectedData = _availableCharacters[_currentIndex];

        DataManager.Instance.CurrentPlayerData.LastUsedCharacterID = selectedData.CharacterID;
        DataManager.Instance.SaveData();

        _uiManager.UpdateCharacterInfo(selectedData);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        
    }
}
