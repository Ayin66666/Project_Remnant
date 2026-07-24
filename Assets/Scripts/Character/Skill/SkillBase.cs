using System.Collections;
using UnityEngine;


// 하는 동작
// 1. 애니메이션 제어
// 2. 이펙트 & 데미지 이벤트 함수
// 3. 스킬의 세부 동작 & 조건은 상속받은 스크립트에서 구현

public abstract class SkillBase : MonoBehaviour
{
    [Header("---Component---")]
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private CharacterBase character;
    [SerializeField] private Animator anim;
    private Coroutine useCoroutine;
    public SkillSO SkillSO => skillSO;


    #region 기본 동작
    /// <summary>
    /// 공격을 위한 데이터 생성 함수
    /// </summary>
    /// <param name="isFront"></param>
    /// <param name="coinIndex"></param>
    /// <returns></returns>
    protected virtual AttackInfo CreateInfo(bool isFront, int coinIndex)
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
            motionValue = isFront ? 0.65f : 1f,
            isCritical = PoiseSO.IsCritical(character),
            critMultiplier = 1.5f,
        };

        return info;
    }

    /// <summary>
    /// 스킬 동작 호출 함수
    /// </summary>
    public virtual void Use()
    {
        if (useCoroutine != null) StopCoroutine(useCoroutine);
        useCoroutine = StartCoroutine(SkillAction());
    }

    /// <summary>
    /// 기능 동작 코루틴
    /// </summary>
    protected abstract IEnumerator SkillAction();

    /// <summary>
    /// 동작 초기화 - 혹시 모를 상황 대비
    /// </summary>
    public abstract void Reset();
    #endregion
}
