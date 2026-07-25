using Game.Character;
using UnityEngine;


public class PlayerCharacter : CharacterBase
{
    [Header("---Player Setting---")]
    [SerializeField] private IdentityMasterSO identityMasterSO;
    public IdentityMasterSO IdentityMasterSO => identityMasterSO;


    #region Start Logic
    private void Start()
    {
        // 전투 시작 시 보정
        // -> 일단 여기에 넣었지만 나중에 BattleManager에서 SetUp() 과정에서 관리할것
        // -> 이게 Player character의 부모 클래스 역할을 한다면 그대로 둬도 ㄱㅊ지 않나?

        SetFacing(Facing.Right);
    }

    /// <summary>
    /// 플레이어 - 스테이터스 세팅 기능
    /// </summary>
    /// <param name="data"></param>
    protected override void SetupStatus(StatusDataSO data)
    {
        maxHp = data.BaseHp // 기본 체력
            + data.SyncUpData[sync].hp // 동기화 체력 
            + Mathf.RoundToInt(data.LevelUpData.hp * level * data.GrowthFactorData.hpFactor); // 레벨업 체력

        curHp = maxHp;

        attack = data.BaseAttackPoint
            + data.SyncUpData[sync].attack
            + Mathf.RoundToInt(data.LevelUpData.attack * level * data.GrowthFactorData.attackFactor);

        defence = data.BaseDefencePoint
            + data.SyncUpData[sync].defence
            + Mathf.RoundToInt(data.LevelUpData.defence * level * data.GrowthFactorData.defenceFactor);

        speedRange = data.SyncUpData[sync].attackSpeed;

        stagger.Clear();
        foreach (int g in data.Groggy)
        {
            stagger.Add(Mathf.RoundToInt(maxHp * g / 100));
        }
    }
    #endregion
}
