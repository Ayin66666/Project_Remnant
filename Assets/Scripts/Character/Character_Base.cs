using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Base : MonoBehaviour, IDamageable
{
    // 플레이어 & 몬스터의 공용 기능

    // 1. 피격 계산
    // 2. 공격 슬롯
    // 3. 스테이터스 (체력, 흐트러짐, 정신력)
    // 4. 인게임 UI (체력바, 이름, 버프 & 디버프 표시)

    [Header("---Status---")]
    [SerializeField] private int level;
    [SerializeField] private int sync;
    [SerializeField] private int hp;
    [SerializeField] private List<int> groggy;
    [SerializeField] private int attack;
    [SerializeField] private int defence;
    [SerializeField] private Vector2Int speed;
    [SerializeField] private StatusDataSO statData;
    public int Hp { get { return hp; } set {  hp = value; } }
    public List<int> Groggy { get {  return groggy; } set {  groggy = value; } }

    [Header("------")]
    [SerializeField] private GameObject[] attackSlots;


    #region Status
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
    public void Status_Setting()
    {
        hp = statData.BaseHp // 기본 체력
            + statData.SyncUpData[sync].hp // 동기화 체력 
            + Mathf.RoundToInt(statData.levelUpStat.hp * level * statData.growthFactor.hpFactor); // 레벨업 체력

        attack = statData.BaseAttackPoint
            + statData.syncUpData[sync].attack
            + Mathf.RoundToInt(statData.levelUpStat.attack * level * statData.growthFactor.attackFactor);

        defence = statData.BaseDefencePoint
            + statData.syncUpData[sync].defence
            + Mathf.RoundToInt(statData.levelUpStat.defence * level * statData.growthFactor.defenceFactor);

        speed = statData.syncUpData[sync].attackSpeed;

        foreach(int g in statData.Groggy)
        {
            groggy.Add(Mathf.RoundToInt(hp * g / 100));
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

        hp -= (int)damage;
    }

    public abstract void Die();
    #endregion
}
