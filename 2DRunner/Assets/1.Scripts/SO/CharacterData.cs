using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "GameData/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("캐릭터 기본 정보")]
    [Tooltip("캐릭터를 구분하는 고유 ID")]
    public int CharacterID;
    
    [Tooltip("게임 내에 표시될 캐릭터의 이름")]
    public string CharacterName;
    
    [Header("캐릭터 능력치")]
    [Tooltip("캐릭터가 가지는 기본 체력")]
    public float BaseHealth;
    [Tooltip("캐릭터가 가지는 추가 이동속도")]
    public float BonusMoveSpeed;
    
    
    
}
