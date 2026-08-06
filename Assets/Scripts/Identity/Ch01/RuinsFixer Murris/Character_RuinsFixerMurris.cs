using UnityEngine;


public class Character_RuinsFixerMurris : PlayerCharacter
{

    [Header("---Passive---")]
    private EffectBaseSO blackWaterSO;
    private EffectBaseSO CintamaniSO;
    private EffectBaseSO JiaoCintamaniSO;
    private GameObject fieldEffectSO; // 임시 작성 - 필드 이펙트 완성 후 재작성 필요


    /* 구현 방식
    // 패시브 로직은 패시브 단위로 작성하는게 아니라
    // 기능별(흑수, 여의, 교룡여의) 단위로 작성하는 방식으로 교체 필요
    // CharacterBase에 virtual 함수로 각 이벤트를 구현해야함!
    // 해당 이벤트 중 필요한 이벤트만 Character_Name 에서 Override 하는 방식으로 구현 예정
    // 다만 특수한 이벤트나 action 기반이 더 어울리는 효과라면 action 구독 형태도 혼용 가능
    */

    /* 패시브 설명
    //패시브 1 - 교룡의 여의
    //흑수 : 스킬 강화에 사용 (최대 20)
    //- 스킬 사용 시 흑수 5 획득
    //- 흑수가 10 이상이면 스킬 사용 시 흑수를 10 소모하고 아래 효과 발동 (턴당 2회) 
    //- 1~3스킬이 강화 
    //- 교룡 여의가 없다면 여의 1 획득

    //여의 : 여의 1당 다음 효과를 얻음 (최대 3)
    //- 공격 포인트 1 증가
    //- 방어 포인트 1 증가
    //- 여의가 3 이상, 교룡 여의가 없다면 여의를 3 소모하고 교룡 여의 1 획득 (턴당 1회)

    //교룡 여의 : 교룡 여의 1당 다음 효과를 얻음 (최대 1)
    //- 공격 포인트 5 증가
    //- 방어 포인트 5 증가
    //- [턴 시작 시] 흑수 20 획득
    //- [턴 시작 시] 체력의 2.5% 만큼 실드 획득 (소숫점 버림)


    // 패시브 2 - 현수격법
    // 공격 슬롯 2개로 전투 시작
    // 치명타 피해량 20% 증가
    // [턴 시작 시] 호흡 위력 2, 호흡 횟수 1 획득


    //패시브 3 - 유수 지대
    //턴 시작 시 자신에게 교룡 여의가 있을 경우 발동.
    //지역 효과 [유수 지대] 를 추가한다.

    //유수 지대
    //지역 내에서 침잠 발동 시, 침잠 위력의 1/2만큼 우울 데미지를 준다.
    //[턴 시작 시] 아군의 체력을 5 회복한다.
    //[턴 시작 시] 아군의 정신력을 5 회복한다.
    */


    #region 패시브 - 흑수
    public void AddBlackWater()
    {
        // 스킬 사용 시 동작 - 흑수 5 추가
        // 이것도 따지자면 Skill에 있는 게 맞지 않나?
        EffectRuntimeData data = new EffectRuntimeData()
        {
            effectSO = blackWaterSO,
            power = 5,
            count = -1
        };

        AddEffect(data);
    }
    #endregion


    #region 패시브 - 여의
    public void AddCintamani()
    {
        // 여의 1 추가
        EffectRuntimeData data = new EffectRuntimeData()
        {
            effectSO = CintamaniSO,
            power = 1,
            count = -1
        };

        AddEffect(data);
    }
    #endregion


    #region 패시브 - 교룡 여의
    /// <summary>
    /// 교룡 여의 추가 (추가 조건은 여의 3개 보유 + 교룡 여의가 없음)
    /// </summary>
    public void AddJiaoCintamani()
    {
        // 이미 교룡 여의가 있다면 무시
        if (GetEffect(CintamaniSO) == null) return;

        EffectRuntimeData data = new EffectRuntimeData()
        {
            effectSO = JiaoCintamaniSO,
            power = 1,
            count = -1
        };

        // 버프 - 교룡 여의 추가
        AddEffect(data);

        // 필드 이펙트 - 유수 지대 추가
        AddFieldEffect();
    }

    /// <summary>
    /// 교룡 여의 획득 시 발동, 유수 지대 필드 효과 추가
    /// </summary>
    public void AddFieldEffect()
    {

    }
    #endregion
}
