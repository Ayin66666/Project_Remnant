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
    [SerializeField] protected int totalDamage;
    protected Coroutine useCoroutine;

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
    /// 스킬 사용 시 발동되는 효과 적용 함수
    /// </summary>
    protected void ApplySkillEffect()
    {

    }

    /// <summary>
    /// 코인 이펙트 적용 함수
    /// </summary>
    /// <param name="coinIndex"></param>
    /// <param name="isBrokenCoin"></param>
    /// <param name="targets"></param>
    protected void ApplyCoinEffects(int coinIndex, bool isBrokenCoin, List<CharacterBase> targets)
    {
        // 이펙트의 개수만큼 동작
        foreach (var effect in skillSO.syncDatas[character.Sync].coins[coinIndex].EffectNodes)
        {
            // 대상 체크
            List<CharacterBase> effectTargets = new List<CharacterBase>();
            switch (effect.Target)
            {
                case EffectNode.TargetType.Self:
                    effectTargets.Add(character);
                    break;

                case EffectNode.TargetType.Target:
                    effectTargets = targets;
                    break;

                case EffectNode.TargetType.Both:
                    effectTargets.Add(character);
                    effectTargets.AddRange(targets);
                    break;

                case EffectNode.TargetType.AllEnemies:
                    // effectTargets = BattleManager.instance.GetAllEnemies(); 
                    break;

                case EffectNode.TargetType.AllAllies:
                    // effectTargets = BattleManager.instance.GetAllPlayer();
                    break;

                case EffectNode.TargetType.Everyone:
                    // effectTargets.AddRange(BattleManager.instance.GetAllEnemies());
                    // effectTargets.AddRange(BattleManager.instance.GetAllPlayer());
                    break;
            }

            // 대상에게 데이터 값을 아래 효과로 적용
            foreach (var tar in effectTargets)
            {
                // 적용을 위한 데이터 생성
                // -> 해당 기능은 일단 이곳에 있으나, characterbase로 이전도 고민해볼것!
                EffectRuntimeData addEffect = new EffectRuntimeData()
                {
                    effectSO = effect.Action.valueNode.effect,
                    power = effect.Action.valueNode.value,
                    count = effect.Action.valueNode.duration,
                };


                switch (effect.Action.actionType)
                {
                    case ActionType.AddEffect:
                        tar.AddEffect(addEffect);
                        break;

                    case ActionType.RemoveEffect:
                        tar.RemoveEffect(addEffect);
                        break;

                    case ActionType.Original:
                        // 오리지널은 어떻게?
                        // characterbase에 오리지널 액션을 부르는 통합 함수를 만들어두고,
                        // 해당 함수를 characterbase를 상속받은 스크립트에서 내부 기능을 채우는 방식은?
                        // 인자값은 index를 받는 식이면 충분할거 같은데
                        OriginalEffect(skillSO.syncDatas[character.Sync].skillEffects[coinIndex].Action.originalActionId);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 오리지널 액션을 호출하는 함수
    /// </summary>
    /// <param name="index"></param>
    protected void OriginalEffect(int index)
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
}


[System.Serializable]
public struct SkillUseData
{
    [Header("---Use Data---")]
    public List<CharacterBase> targets;
    public List<bool> isCoinDestroy;
}