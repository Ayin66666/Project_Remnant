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
