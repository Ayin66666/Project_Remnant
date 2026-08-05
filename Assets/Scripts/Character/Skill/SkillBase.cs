using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] protected List<SkillEffectRuntimeData> effectRuintimeList;
    public enum EffectRange
    {
        Skill,
        Coin
    }

    [Header("---Component---")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected SkillSO skillSO;
    [SerializeField] protected CharacterBase character;
    protected List<Action> originalActions;
    public SkillSO SkillSO => skillSO;


    #region 기본 동작
    /// <summary>
    /// 공격을 위한 데이터 생성 함수
    /// </summary>
    /// <param name="isFront"></param>
    /// <param name="coinIndex"></param>
    /// <returns></returns>
    protected virtual AttackInfo CreateInfo()
    {
        // Info는 코인 공격 시작 시 생성해야 함
        // 생성 후 타겟에게 데이터를 넘겨주고 계산된 최종 데미지 값을 반환받음
        // 받은 값은 Dictionary에 저장 후, 공격 시 각 모션의 데미지 % 만큼 타겟의 TakeDamage()로 전달
        // List 내 모든 타겟이 사망했다면 공격 종료 후 dic 초기화

        AttackInfo info = new AttackInfo()
        {
            sinType = skillSO.sinType,
            attackType = skillSO.attackType,
            attackPoint = character.Attack,
            motionValue = skillSO.syncDatas[character.Sync].motionValue,
        };

        return info;
    }

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
    /// 스킬 효과 발동 함수
    /// </summary>
    protected void ApplyEffect(EffectNode effectNode, List<CharacterBase> targets)
    {
        // 동작 조건 체크 -> Targets 의 0번은 메인 타겟 고정
        CharacterBase checkTarget = effectNode.Condition.checkTarget ==
            EffectNode.TargetType.Self ? character : targets[0];

        if (!ConditionCheck(effectNode.Condition, checkTarget))
            return;

        // 적용할 값 데이터 생성
        EffectRuntimeData effectData = new EffectRuntimeData()
        {
            effectSO = effectNode.Action.valueNode.effect,
            power = effectNode.Action.valueNode.value,
            count = effectNode.Action.valueNode.duration,
        };

        // 동작 대상에게 적용
        foreach (var target in targets)
        {
            // 동작 타입 체크
            switch (effectNode.Action.actionType)
            {
                case ActionType.None:
                    break;

                case ActionType.AddEffect:
                    target.AddEffect(effectData);
                    break;

                case ActionType.RemoveEffect:
                    target.RemoveEffect(effectData);
                    break;

                case ActionType.SkillEffect:
                    // 스킬 버프는 어떻게?
                    // 함수를 하나 만들어 두고 index 형태로 호출해야하나?
                    // ㅇㅇ 버프 동작 이렇게?

                    // 26.08.05
                    // 조건 만족 시 런타임 데이터를 제작해서 List에 넣고,
                    // 스킬 데미지 계산 시 해당 List를 확인 후, 데미지 계산에 적용하는 방식으로

                    // 데이터 생성
                    SkillEffectRuntimeData runtimeData = new SkillEffectRuntimeData
                        (
                        (SkillEffectSO)effectNode.Action.valueNode.effect,
                        EffectRange.Skill,
                        effectNode.Action.valueNode.value
                        );

                    effectRuintimeList.Add(runtimeData);
                    break;

                case ActionType.Original:
                    // 오리지널 이펙트는 어떻게?
                    // 인덱스 값은 어디서 넘겨주지? -> 이미 만들어둔 인덱스가 있음
                    UseOriginalEffect(effectNode.Action.originalActionId);
                    break;
            }
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