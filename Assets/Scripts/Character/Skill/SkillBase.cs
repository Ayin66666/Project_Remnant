using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


// 하는 동작
// 1. 애니메이션 제어
// 2. 이펙트 & 데미지 이벤트 함수
// 3. 스킬의 세부 동작 & 조건은 상속받은 스크립트에서 구현

public abstract class SkillBase : MonoBehaviour
{
    [Header("---Setting---")]
    protected Coroutine useCoroutine;

    [Header("---Runtime Data---")]
    [SerializeField] protected int totalDamage;
    [SerializeField] protected List<SkillEffectRuntimeData> effectRuntimeList;
    [SerializeField] protected List<CharacterBase> targetList;

    [Header("---Component---")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected SkillSO skillSO;
    [SerializeField] protected CharacterBase character;
    protected List<Action> originalActions;
    public SkillSO SkillSO => skillSO;

    [Header("---Prefabs---")]
    [SerializeField] protected List<GameObject> effects;


    #region 데미지 데이터? -> 미묘한 위치

    /// <summary>
    /// 코인 앞뒷면 표시
    /// -> 1차 제작 완료 / UI 이벤트 필요
    /// </summary>
    /// <returns></returns>
    protected bool CoinToss()
    {
        // 정신력 0 기준 기본확률은 50%,
        // 45 기준 95%,
        // -45 기준 5% 확률로 앞면이 나옴

        // 연출 부분은 어디에 둘지 고민중
        int chance = 50 + character.Mentality;
        return UnityEngine.Random.Range(0, 100) < chance;
    }

    /// <summary>
    /// 공격자의 총 데미지 계산
    /// </summary>
    protected void CalTotalDamage()
    {
        // 데미지 계산을 위한 Info 제작
        AttackInfo info = new AttackInfo()
        {
            sinType = skillSO.sinType,
            attackType = skillSO.attackType,
            attackPoint = character.Attack,
            motionValue = skillSO.syncDatas[character.Sync].motionValue,
        };

        // 스킬의 총 데미지 계산
        // (해당 데미지를 기반으로 CalCoinDamage() 함수에서 각 공격의 배율만큼 나눠서 사용함)
        totalDamage = character.CalDamage(info);
    }

    /// <summary>
    /// 치명타 여부 및 해당 공격의 최종 데미지 계산 후 전달
    /// </summary>
    /// <param name="totalDamage"></param>
    /// <param name="percentage"></param>
    /// <returns></returns>
    protected (bool, int) CalCoinDamage(int totalDamage, float percentage)
    {
        bool isCir = PoiseSO.IsCritical(character);
        int damage = (int)(totalDamage / percentage);
        return (isCir, damage);
    }
    #endregion


    #region 스킬 효과 로직
    /// <summary>
    /// 스킬 효과 발동 함수
    /// </summary>
    protected void ApplyEffect(EffectNode effectNode, List<CharacterBase> targets)
    {
        // 타겟 체크 (사망 혹은 이외의 이유로 없어졌다면 종료)
        if (effectNode.Condition.checkTarget != EffectNode.TargetType.Self && targets.Count == 0)
            return;

        // 동작 조건 체크 -> Targets 의 0번은 메인 타겟 고정
        CharacterBase checkTarget = effectNode.Condition.checkTarget ==
            EffectNode.TargetType.Self ? character : targets[0];

        if (!ConditionCheck(effectNode.Condition, checkTarget))
            return;

        // 동작 타입 체크
        switch (effectNode.Action.actionType)
        {
            case ActionType.None:
                break;

            case ActionType.AddEffect:
                EffectRuntimeData effectData = new EffectRuntimeData()
                {
                    effectSO = effectNode.Action.valueNode.effect,
                    power = effectNode.Action.valueNode.value,
                    count = effectNode.Action.valueNode.duration,
                };

                foreach (var target in targets)
                {
                    target.AddEffect(effectData);
                }
                break;

            case ActionType.RemoveEffect:
                effectData = new EffectRuntimeData()
                {
                    effectSO = effectNode.Action.valueNode.effect,
                    power = effectNode.Action.valueNode.value,
                    count = effectNode.Action.valueNode.duration,
                };

                foreach (var target in targets)
                {
                    target.RemoveEffect(effectData);
                }
                break;

            case ActionType.SkillEffect:
                // 26.08.05
                // 스킬 이펙트의 경우 무조건 사용자에게 부여되는 효과임!
                // 조건 만족 시 런타임 데이터를 제작해서 List에 넣고,
                // 스킬 데미지 계산 시 해당 List를 확인 후
                // 데미지 계산에 적용하는 방식으로 구현함!

                // 데이터 생성
                SkillEffectRuntimeData runtimeData = new SkillEffectRuntimeData(
                    (SkillEffectSO)effectNode.Action.valueNode.effect,
                    EffectRange.Skill,
                    effectNode.Action.valueNode.value
                    );

                effectRuntimeList.Add(runtimeData);
                break;

            case ActionType.Original:
                UseOriginalEffect(effectNode.Action.originalActionId);
                break;
        }
    }

    /// <summary>
    /// 동작 조건 체크 로직 (조건에 부합하다면 True, 부합하지 않다면 False 반환)
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    private bool ConditionCheck(EffectNode.ConditionNode condition, CharacterBase checkTarget)
    {
        // 조건이 없다면 즉시 True 반환
        if (condition.compareType == EffectNode.CompareType.None)
            return true;

        // 조건 값 체크
        bool canUse = false;
        int total = 0;
        for (int i = 0; i < condition.values.Count; i++)
        {
            EffectRuntimeData data = checkTarget.GetEffect(condition.values[i].effect);
            if (data == null) continue;

            int val = condition.values[i].valueType == ValueType.Power ? data.power : data.count;
            total += val;
        }

        // 조건 확인
        switch (condition.compareType)
        {
            case EffectNode.CompareType.LessEqual:
                // 체크 값이 조건보다 작거나 같다면
                canUse = total <= condition.conditionValue;
                break;

            case EffectNode.CompareType.Equal:
                // 체크 값이 조건과 같다면
                canUse = total == condition.conditionValue;
                break;

            case EffectNode.CompareType.GreaterEqual:
                // 체크 값이 조건보다 크거나 같다면
                canUse = total >= condition.conditionValue;
                break;
        }

        // 결과값 반환
        return canUse;
    }

    /// <summary>
    /// 오리지널 액션을 호출하는 함수
    /// </summary>
    /// <param name="index"></param>
    protected void UseOriginalEffect(int index)
    {
        if (originalActions.Count < index || index < 0)
            return;

        originalActions[index]?.Invoke();
    }
    #endregion


    #region 기본 동작
    /// <summary>
    /// 스킬 동작 호출 함수
    /// </summary>
    public virtual void Use(SkillUseData useData)
    {
        if (useCoroutine != null) StopCoroutine(useCoroutine);
        useCoroutine = StartCoroutine(SkillAction(useData));
    }

    /// <summary>
    /// 기능 동작 코루틴
    /// </summary>
    protected abstract IEnumerator SkillAction(SkillUseData useData);


    /// <summary>
    /// 스킬 동작으로 인한 캐릭터 이동 호출 함수
    /// </summary>
    /// <param name="index"></param>
    public virtual void Movement(int index)
    {
        // 세부 구현은 상속받은 스크립트에서 구현
    }

    /// <summary>
    /// 동작 초기화 - 혹시 모를 상황 대비
    /// </summary>
    public virtual void Reset()
    {
        // 데미지 초기화
        totalDamage = 0;

        // 이펙트 종료
        foreach (var obj in effects)
        {
            obj.SetActive(false);
        }

        // 타겟 리스트 초기화
        targetList.Clear();
    }
    #endregion



    [System.Serializable]
    /// <summary>
    /// 스킬 효과 런타임 데이터
    /// </summary>
    public class SkillEffectRuntimeData
    {
        [Header("---Skill EffectRuntime Data---")]
        public SkillEffectSO so;
        public EffectRange effectRange;
        public int value;

        /// <summary>
        /// 생성자 - 데이터 생성 시 무조건 데이터가 빈 곳이 없도록 만들어야 함!
        /// </summary>
        /// <param name="so"></param>
        /// <param name="effectRange"></param>
        /// <param name="value"></param>
        public SkillEffectRuntimeData(SkillEffectSO so, EffectRange effectRange, int value)
        {
            this.so = so;
            this.effectRange = effectRange;
            this.value = value;
        }
    }
}


[System.Serializable]
public struct SkillUseData
{
    [Header("---Use Data---")]
    public List<CharacterBase> targets;
    public List<bool> isCoinDestroy;
}