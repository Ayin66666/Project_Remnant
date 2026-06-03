using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    // 플레이어 & 몬스터의 공용 기능
    // 1. 피격 계산
    // 2. 공격 슬롯
    // 3. 스테이터스 (체력, 흐트러짐, 정신력)
    // 4. 인게임 UI (체력바, 이름, 버프 & 디버프 표시)

    [Header("---Test---")]
    [SerializeField] private float testSpeed;
    [SerializeField] private Transform testPos;
    [SerializeField] private KnockbackType testKnockbackType;

    [Header("---Status---")]
    [SerializeField] protected int level;
    [SerializeField] protected int sync;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int curHp;
    [SerializeField] protected List<int> groggy;
    [SerializeField] protected int attack;
    [SerializeField] protected int defence;
    [SerializeField] protected int speed;
    [SerializeField] protected Vector2Int speedRange;
    public int MaxHp => maxHp;
    public List<int> Groggy => groggy;
    public int Speed => speed;

    [Header("---Slot---")]
    [SerializeField] protected SkillSlot[] attackSlots;

    [Header("---Status Effect---")]
    [SerializeField] protected StatusEffectContainer statusEffectContainer;

    [Header("---Component---")]
    [SerializeField] protected Transform body;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected Animator anim;
    [SerializeField] protected CharacterUI characterUI;

    [Header("---Setting---")]
    [SerializeField] private Facing facing;
    [SerializeField] private bool isMove;
    public bool IsMove => isMove;
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


    private void Update()
    {
        // 테스트용 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClashKnockback(testKnockbackType, testSpeed);
        }
    }

    #region 시작 로직
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
        // characterUI.UpdataHpUI();
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


    /// <summary>
    /// 캐릭터 이동 로직 호출부
    /// </summary>
    public void CharacterMove(float moveSpeed, Vector2 pos)
    {
        if (movementCoroutine != null) 
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(CharacterMoveCoroutine(moveSpeed, pos));
    }

    /// <summary>
    /// 캐릭터 이동 로직 동작부
    /// </summary>
    /// <param name="moveSpeed"></param>
    /// <param name="movePos"></param>
    /// <returns></returns>
    private IEnumerator CharacterMoveCoroutine(float moveSpeed, Vector2 movePos)
    {
        isMove = true;

        // 좌우 체크 & 스프라이트 반전
        float dir = movePos.x - transform.position.x;
        SetFacing(dir > 0 ? Facing.Right : Facing.Left);

        // 이동
        Vector2 startPos = transform.position;
        float timer = 0;
        while(timer < 1)
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
        if(movementCoroutine != null) 
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(ClashKnockbackCoroutine(type, time));
    }

    /// <summary>
    /// 캐릭터 합 밀림 로직 동작부
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClashKnockbackCoroutine(KnockbackType type, float time)
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
        while(timer < 1)
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


    #region 데미지 로직
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

    public virtual void Die()
    {
        // 사망 스프라이트
    }
    #endregion
}
