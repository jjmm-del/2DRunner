using UnityEngine;
using System;
[Serializable]
public class PlayerData
{
    public int CoinCount;           //현재 보유한 동전
    public int MaxHealthLevel;      //최대 체력 업그레이드
    public int CoinScoreLevel;      // 동전 획득 시 추가 점수 레벨
    public float HighestScore;      //유저의 최고 점수
    public int LastUsedCharacterID; //마지막으로 플레이했던 캐릭터의 ID

    public PlayerData()
    {
        CoinCount = 0;
        MaxHealthLevel = 1;
        CoinScoreLevel = 1;
        HighestScore = 0;
        LastUsedCharacterID = 0;
    }

}
