using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 플레이어 & 몬스터의 공용 기능
// 1. 피격 계산
// 2. 공격 슬롯
// 3. 스테이터스 (체력, 흐트러짐, 정신력)
// 4. 인게임 UI (체력바, 이름, 버프 & 디버프 표시)

public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    [Header("---State---")]
    #region
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isPanic;
    [SerializeField] private int panicTurn;
    [SerializeField] private bool isStagger;
    [SerializeField] private int staggerTurn;
    public bool IsAttack => isAttack;
    #endregion

    [Header("---Status---")]
    #region
    [SerializeField] protected int level;
    [SerializeField] protected int sync;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int curHp;
    [SerializeField] protected int mentality;
    [SerializeField] protected List<int> stagger;
    [SerializeField] protected int attack;
    [SerializeField] protected int defence;
    [SerializeField] protected int speed;
    [SerializeField] protected Vector2Int speedRange;
    public int MaxHp => maxHp;
    public List<int> StaggerList => stagger;
    public int Mentality => mentality;
    public int Speed => speed;
    public int Attack => attack;
    public int Defence => defence;
    public int Sync => sync;
    #endregion

    [Header("---Status Effect---")]
    #region
    [SerializeField] protected Dictionary<KeywordType, EffectRuntimeData> keywordEffects;
    [SerializeField] protected List<EffectRuntimeData> statusEffects;
    #endregion

    [Header("---Component---")]
    #region
    [SerializeField] protected Transform body;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected Animator anim;
    [SerializeField] protected CharacterUI characterUI;
    [SerializeField] protected SkillSlot[] attackSlots;
    #endregion

    [Header("---Movement---")]
    #region
    [SerializeField] protected CharacterGroup characterGroup;
    [SerializeField] private Facing facing;
    [SerializeField] private bool isMove;
    private Coroutine movementCoroutine;
    public enum Facing
    {
        Left,
        Right
    }
    public enum KnockbackType
    {
        Low,
        Mid,
        High
    }
    public enum CharacterGroup
    {
        Player,
        Enemy,
        AllyNpc,
    }
    public bool IsMove => isMove;
    public CharacterGroup CharacterType => characterGroup;
    #endregion

    [Header("---Effect---")]
    protected List<GameObject> effects;


    #region 시작 로직
    private void Awake()
    {
        keywordEffects = new Dictionary<KeywordType, EffectRuntimeData>();
        statusEffects = new List<EffectRuntimeData>();
    }

    /// <summary>
    /// 통합 능력치 설정 함수 - Data_Setting() & Status_Setting() 둘 다 호출함
    /// </summary>
    /// <param name="level"></param>
    /// <param name="sync"></param>
    public void SetUp(StatusDataSO data, int level, int sync)
    {
        SetupData(level, sync);
        SetupStatus(data);
        characterUI.SetUp(this);
    }

    /// <summary>
    /// 캐릭터 데이터 전달
    /// </summary>
    /// <param name="level"></param>
    /// <param name="sync"></param>
    protected virtual void SetupData(int level, int sync)
    {
        this.level = level;
        this.sync = sync;
    }

    /// <summary>
    /// 스테이터스 셋팅
    /// </summary>
    protected abstract void SetupStatus(StatusDataSO data);
    #endregion


    #region 전투 로직
    /// <summary>
    /// 공격 주사위 속도 세팅
    /// </summary>
    protected void SpeedSetUp()
    {
        // 속도 세팅
        speed = Random.Range(speedRange.x, speedRange.y);

        // UI 반영
        characterUI.UpdataSpeedUI();
    }

    /// <summary>
    /// 공격 상태 bool 값을 세팅하는 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetAttackState(bool value)
    {
        isAttack = value;
    }

    /// <summary>
    /// 바디의 바라보는 방향 설정
    /// </summary>
    /// <param name="facing"></param>
    protected void SetFacing(Facing facing)
    {
        switch (facing)
        {
            case Facing.Left:
                this.facing = Facing.Left;
                body.localScale = new Vector3(-1, 1, 1);
                break;

            case Facing.Right:
                this.facing = Facing.Right;
                body.localScale = new Vector3(1, 1, 1);
                break;
        }
    }
    #endregion


    #region 이동 로직
    /// <summary>
    /// 캐릭터 이동 로직 호출부
    /// </summary>
    public void CharacterMove(float moveSpeed, Vector2 pos)
    {
        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(CoCharacterMove(moveSpeed, pos));
    }

    /// <summary>
    /// 캐릭터 이동 로직 동작부
    /// </summary>
    /// <param name="moveSpeed"></param>
    /// <param name="movePos"></param>
    /// <returns></returns>
    private IEnumerator CoCharacterMove(float moveSpeed, Vector2 movePos)
    {
        isMove = true;

        // 좌우 체크 & 스프라이트 반전
        float dir = movePos.x - transform.position.x;
        SetFacing(dir > 0 ? Facing.Right : Facing.Left);

        // 이동
        Vector2 startPos = transform.position;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / moveSpeed;
            transform.position = Vector3.Lerp(startPos, movePos, timer);
            yield return null;
        }
        transform.position = movePos;

        isMove = false;
        movementCoroutine = null;
    }

    /// <summary>
    /// 캐릭터 합 밀림 로직 호출부
    /// </summary>
    /// <param name="type">밀림 타입 (Low = 1f, Mid = 3f, High = 5f)</param>
    /// <param name="time">밀림 시간</param>
    public void ClashKnockback(KnockbackType type, float time)
    {
        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(CoClashKnockback(type, time));
    }

    /// <summary>
    /// 캐릭터 합 밀림 로직 동작부
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoClashKnockback(KnockbackType type, float time)
    {
        isMove = true;

        // 이동 위치 세팅
        float distance = type switch
        {
            KnockbackType.Low => 1f,
            KnockbackType.Mid => 3f,
            KnockbackType.High => 5f,
            _ => 0f,
        };
        Vector2 dir = facing == Facing.Left ? Vector2.right : Vector2.left;
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + dir * distance;
        endPos.y += Random.Range(-0.25f, 0.25f);

        // 이동
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / time;
            transform.position = Vector2.Lerp(startPos, endPos, timer);
            yield return null;
        }
        transform.position = endPos;

        movementCoroutine = null;
        isMove = false;
    }
    #endregion


    #region 데미지 & 상태이상 로직
    /// <summary>
    /// 데미지 계산식을 활용한 일반 데미지 계산
    /// 단, 크리티컬 데미지는 계산되어 있지 않음
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public int CalDamage(AttackInfo info)
    {
        // 데미지 공식
        // ((공격 포인트 * 모션 배율) * step) * 치명타 배율[1.5]
        // step = 공격자의 공격 포인트 - 방어 포인트 했을 때, 얼마나 차이 나는지
        // (3 차이 날 때마다 데미지 10% 증감)

        // 크리티컬의 경우 공격 시 poise 값을 별도로 체크해서 크리티컬 체크를 해줘야 함!

        // 크리티컬은 호흡에 영향을 받기 때문에
        // 크리티컬 데미지의 경우 즉시 계산이 아닌 코인 회전 시 계산이 맞음
        //float damage = info.attackPoint * info.motionValue * (info.isCritical ? info.critMultiplier : 1);

        float damage = info.attackPoint * info.motionValue;
        int diff = info.attackPoint - defence;
        int step = diff / 3;
        damage *= 1 + step * 0.1f;
        damage = Mathf.Max(1, damage);

        return (int)damage;
    }

    /// <summary>
    /// 데미지 계산 로직 / 모든 데미지 계산 후 해당 함수 호출해야함!
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        // 데미지 계산 방식 변경 예정
        // 1. target의 정보와 내 공격 정보를 기반으로 데미지 계산을 위한 Info 전달
        // 2. info 기반 데미지 계산 후 반환
        // 3. 반환 데미지를 target의 takeDamage에 전달

        // 이벤트
        OnHit();

        // 데미지 계산
        curHp -= damage;
        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    /// <summary>
    /// 정신력 데미지 계산 로직 / 침잠 등 정신력 관련 데미지를 받았을 때 동작
    /// </summary>
    /// <param name="mDamage"></param>
    public void TakeMDamage(int mDamage)
    {
        if (isPanic) return;

        mentality -= mDamage;
        if (mentality <= -45)
        {
            mentality = -45;
            Panic();
        }
    }

    /// <summary>
    /// 흐트러짐 게이지 계산 로직 / 진동 등 흐트러짐 게이지를 당기는 효과를 받았을 때 호출
    /// </summary>
    /// <param name="sDamage"></param>
    public void TakeSDamage(int sDamage)
    {
        for (int i = 0; i < stagger.Count; i++)
        {
            stagger[i] += sDamage;
        }
    }


    /// <summary>
    /// 패닉 동작 / 정신력이 -45 이하가 되면 패닉 상태로 전환, 패닉 상태에서는 공격 불가
    /// </summary>
    public virtual void Panic()
    {
        // 턴 진행 도중 패닉 상태가 되면 다음 턴에 패닉 상태 시작
        // -> 그 전까진 정신력 -45 상태로 동작은 함!

        isPanic = true;
        panicTurn = 1;
    }

    /// <summary>
    /// 흐트러짐 동작 / 흐트러짐 상태가 되면 턴 종료 시
    /// </summary>
    public void Stagger()
    {
        // 턴 진행 도중 흐트러짐 상태가 되면 해당 턴, 다음 턴에 흐트러짐 상태 유지
        // 턴 시작 시 흐트러짐 상태가 되면 해당 턴만 흐트러짐 상태 유지

        // 상태 변경
        isStagger = true;
        staggerTurn = 2;

        // 애니메이션
        // anim.SetTrigger("Action");
        // anim.SetBool("isPanic", true);
    }

    /// <summary>
    /// 사망 로직 / 사망 시 이벤트가 있다면 override해서 사용
    /// </summary>
    public virtual void Die()
    {
        // 사망 스프라이트
    }


    /// <summary>
    /// 턴 종료 시 호출 / 패닉 상태라면 패틱 기간 감소 / 패닉 기간이 끝나면 패닉 상태 해제
    /// </summary>
    public void PanicCount()
    {
        if (!isPanic) return;

        panicTurn--;
        if (panicTurn <= 0)
        {
            isPanic = false;
            panicTurn = 0;
        }
    }

    /// <summary>
    /// 턴 종료 시 호출 / 흐트러짐 상태라면 흐트러짐 기간 감소 / 흐트러짐 기간이 끝나면 흐트러짐 상태 해제
    /// </summary>
    public void StaggerCount()
    {
        if (!isStagger) return;

        staggerTurn--;
        if (staggerTurn <= 0)
        {
            isStagger = false;
            staggerTurn = 0;
        }
    }
    #endregion


    #region 버프 & 디버프 로직
    /// <summary>
    /// 필요한 키워드의 보유 값 전달 / 없을 경우 Null 반환
    /// </summary>
    /// <param name="keyword">값이 필요한 키워드</param>
    /// <returns></returns>
    public EffectRuntimeData GetKeyword(KeywordType keyword)
    {
        return keywordEffects.ContainsKey(keyword) ? keywordEffects[keyword] : null;
    }

    /// <summary>
    /// 필요한 버프 & 디버프의 보유 값 전달 / 없을 경우 Null 반환
    /// </summary>
    /// <param name="so"></param>
    /// <returns></returns>
    public EffectRuntimeData GetEffect(EffectBaseSO so)
    {
        EffectRuntimeData data = statusEffects.Where(x => x.effectSO == so).FirstOrDefault();
        return data;
    }



    /// <summary>
    /// 키워드 데이터 추가 - 기존 데이터가 있다면 합산, 없다면 신규 추가
    /// </summary>
    /// <param name="data"></param>
    public void AddEffect(EffectRuntimeData data)
    {
        // 이펙트 데이터 체크
        if(data.effectSO.Category == EffectBaseSO.EffectCategory.Public)
        {
            // 공용 이펙트
            if (data.effectSO == null)
            {
                Debug.LogError($"데이터 추가 실패! / {data}, {data.effectSO} 가 없음!");
                return;
            }

            // 데이터 추가
            statusEffects.Add(data);
        }
        else
        {
            // 키워드
            // 일반 키워드는 파생 키워드를 덮어쓰지 못함
            // 파생 키워드는 이미 부여된 일반 & 파생 키워드가 있다면 해당 키워드를 덮어씀
            keywordEffects.TryGetValue(data.effectSO.Keyword, out var runtimeData);
            if (runtimeData == null)
            {
                // 데이터가 없다면 - 신규 데이터 추가
                EffectRuntimeData newData = new EffectRuntimeData()
                {
                    effectSO = data.effectSO,
                    power = data.power,
                    count = data.count
                };

                keywordEffects.Add(data.effectSO.Keyword, newData);

                Debug.Log("키워드 신규 추가 : " + keywordEffects.ContainsKey(data.effectSO.Keyword));
            }
            else
            {
                // 데이터가 있다면 - 데이터 합산
                if (data.effectSO is KeywordSO keyword)
                {
                    // 파생 키워드라면 / 추가하려는 파생 키워드로 전환
                    if (keyword.IsDerived)
                        runtimeData.effectSO = data.effectSO;
                }

                runtimeData.power += data.power;
                runtimeData.count += data.count;
            }
        }
    }

    /// <summary>
    /// 키워드 사용 시, 해당 키워드의 횟수를 차감시키는 함수
    /// 전부 사용했다면 딕셔너리 or List에서 제거함
    /// </summary>
    /// <param name="type"></param>
    public void ConsumeKeyword(KeywordType type, int consumeCount = 1)
    {
        // 키워드 보유 여부 체크
        if (!keywordEffects.TryGetValue(type, out var keyword))
        {
            Debug.Log($"존재하지 않는 키워드에 접근함! / CharacterBase.UseKeyword({type})");
            return;
        }

        // 이벤트
        OnStatusEffectActivate(type);

        // 키워드 감소
        keyword.count -= consumeCount;
        if (keyword.count <= 0)
        {
            // dic에서 키워드 제거
            keywordEffects.Remove(type);
        }
    }

    /// <summary>
    /// 이펙트 제거
    /// </summary>
    /// <param name="data"></param>
    public void RemoveEffect(EffectRuntimeData data)
    {
        // 구현 필요
        if (data.effectSO.Category == EffectBaseSO.EffectCategory.Public)
        {
            // 공용 이펙트
        }
        else
        {
            // 키워드
        }
    }
    #endregion


    #region Virtual 트리거 함수 
    /// <summary>
    /// 턴 시작 시 호출
    /// </summary>
    public virtual void OnTurnStart()
    {

    }

    /// <summary>
    /// 턴 종료 시 호출
    /// </summary>
    public virtual void OnTurnEnd()
    {

    }

    /// <summary>
    /// 합 결과 발생 시 호출
    /// </summary>
    /// <param name="isWin"></param>
    public virtual void OnCrush(bool isWin)
    {

    }

    /// <summary>
    /// 공격 시 호출
    /// </summary>
    public virtual void OnAttack()
    {

    }

    /// <summary>
    /// 피격 시 호출
    /// </summary>
    public virtual void OnHit()
    {

    }

    /// <summary>
    /// 7대 키워드 동작 (호침진파충출화)
    /// </summary>
    /// <param name="type"></param>
    public virtual void OnStatusEffectActivate(KeywordType type)
    {

    }
    #endregion
}


#region
[System.Serializable]
/// <summary>
/// 키워드 런타임 데이터
/// </summary>
public class EffectRuntimeData
{
    [Header("---Data---")]
    public EffectBaseSO effectSO;
    public int power;

    /// <summary>
    /// Keyword의 경우 횟수, 공용의 경우 지속 턴
    /// </summary>
    public int count; // -1의 경우 무한유지
}
#endregion
