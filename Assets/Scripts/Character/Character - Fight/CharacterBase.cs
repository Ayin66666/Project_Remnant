using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    // 플레이어 & 몬스터의 공용 기능

    // 1. 피격 계산
    // 2. 공격 슬롯
    // 3. 스테이터스 (체력, 흐트러짐, 정신력)
    // 4. 인게임 UI (체력바, 이름, 버프 & 디버프 표시)

    [Header("---Status---")]
    [SerializeField] private int level;
    [SerializeField] private int sync;
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;
    [SerializeField] private List<int> groggy;
    [SerializeField] private int attack;
    [SerializeField] private int defence;
    [SerializeField] private Vector2Int speed;
    public int MaxHp { get { return maxHp; } set {  maxHp = value; } }
    public List<int> Groggy { get {  return groggy; } set {  groggy = value; } }


    [Header("------")]
    [SerializeField] private AttackSlot[] attackSlots;


    [Header("---Status Effect---")]
    [SerializeField] private StatusEffectContainer statusEffectContainer;


    [Header("---Component---")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private CharacterUI characterUI;


    #region Status
    /// <summary>
    /// 통합 능력치 설정 함수 - Data_Setting() & Status_Setting() 둘 다 호출함
    /// </summary>
    /// <param name="level"></param>
    /// <param name="sync"></param>
    public void SetUp(StatusDataSO data, int level, int sync)
    {
        Data_Setting(level, sync);
        Status_Setting(data);
        characterUI.UI_Setting(this);
    }
    
    /// <summary>
    /// 캐릭터 데이터 전달
    /// </summary>
    /// <param name="level"></param>
    /// <param name="sync"></param>
    public void Data_Setting(int level, int sync)
    {
        this.level = level;
        this.sync = sync;
    }

    /// <summary>
    /// 스테이터스 셋팅
    /// </summary>
    public void Status_Setting(StatusDataSO data)
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

        speed = data.SyncUpData[sync].attackSpeed;

        groggy.Clear();
        foreach (int g in data.Groggy)
        {
            groggy.Add(Mathf.RoundToInt(maxHp * g / 100));
        }
    }
    #endregion


    /// <summary>
    /// 공격 주사위 속도 셋팅
    /// </summary>
    public void SlotSpeed_Setting()
    {

    }


    #region Damage
    public void TakeDamage(DamageInfo info)
    {
        int damage = info.damageType switch
        {
            DamageType.Normal => CalNormalDamage(info),
            DamageType.Keyword => info.keywordDamage,
            _ => 0,
        };

        curHp -= damage;
        if(curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    /// <summary>
    /// 데미지 계산식을 활용한 일반 데미지 계산
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public int CalNormalDamage(DamageInfo info)
    {
        // 데미지 공식
        // (공격 포인트 * 모션 배율 * 치명타 배율[1.5]) * step
        // step = 공격자의 공격 포인트 - 방어 포인트 했을 때, 얼마나 차이 나는지
        // (3 차이 날 때마다 데미지 10% 증감)

        bool isCri = Random.Range(0, 100) < info.critChance;
        float damage = info.attackPoint * info.motionValue * (isCri ? info.critMultiplier : 1);
        int diff = info.attackPoint - defence;
        int step = diff / 3;
        damage *= 1 + step * 0.1f;
        damage = Mathf.Max(1, damage);

        return (int)damage;
    }


    public abstract void Die();
    #endregion
}
