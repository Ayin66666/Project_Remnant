using UnityEngine;


public class Character_RuinsFixerMurris : PlayerCharacter
{

    [Header("---Passive 3---")]
    private EffectBaseSO so;
    private GameObject fieldEffectSO; // 임시 작성 - 필드 이펙트 완성 후 재작성 필요
    private bool isPassive3;


    #region Passive

    // 패시브 로직은 패시브 단위로 작성하는게 아니라
    // 기능별(흑수, 여의, 교룡여의) 단위로 작성하는 방식으로 교체 필요
    // CharacterBase에 virtual 함수로 각 이벤트를 구현해야함!
    // 해당 이벤트 중 필요한 이벤트만 Character_Name 에서 Override 하는 방식으로 구현 예정
    // 다만 특수한 이벤트나 action 기반이 더 어울리는 효과라면 action 구독 형태도 혼용 가능

    public void Passive1()
    {
        /*
        패시브 1 - 교룡의 여의

        흑수 : 스킬 강화에 사용 (최대 20)
	        - 스킬 사용 시 흑수 5 획득
	        - 흑수가 10 이상이면 스킬 사용 시 흑수를 10 소모하고 아래 효과 발동 (턴당 2회) 
		        - 1~3스킬이 강화 
		        - 교룡 여의가 없다면 여의 1 획득

        여의 : 여의 1당 다음 효과를 얻음 (최대 3)
	        - 공격 포인트 1 증가
	        - 방어 포인트 1 증가
	        - 여의가 3 이상, 교룡 여의가 없다면 여의를 3 소모하고 교룡 여의 1 획득 (턴당 1회)

        교룡 여의 : 교룡 여의 1당 다음 효과를 얻음 (최대 1)
	        - 공격 포인트 5 증가
	        - 방어 포인트 5 증가
	        - [턴 시작 시] 흑수 20 획득
	        - [턴 시작 시] 체력의 2.5% 만큼 실드 획득 (소숫점 버림)
         */
    }

    public void Passive2()
    {
        /*
        패시브 2 - 현수격법
        공격 슬롯 2개로 전투 시작
        치명타 피해량 20% 증가
        [턴 시작 시] 호흡 위력 2, 호흡 횟수 1 획득
        */
    }

    public void Passive3()
    {
        /*
        패시브 3 - 유수 지대
        턴 시작 시 자신에게 교룡 여의가 있을 경우 발동.
        지역 효과 [유수 지대] 를 추가한다.

        유수 지대
	    지역 내에서 침잠 발동 시, 침잠 위력의 1/2만큼 우울 데미지를 준다.
	    [턴 시작 시] 아군의 체력을 5 회복한다.
	    [턴 시작 시] 아군의 정신력을 5 회복한다.
        */

        // 유수 지대가 활성화되어 있다면
        if (BattleManager.instance.GetfieldEffect(fieldEffectSO))
        {
            // 1. 턴이 시작되었다면
            if(true)
            {
                // 아군 전체의 체력 5, 정신력 5 회복
            }

            // 2. 침잠 효과가 발동했다면
            if (true)
            {
                // 침잠 효과가 발동한 대상에게 침잠 위력의 1/2만큼 우울 데미지 부여
            }
        }
        else
        {
            // 자신에게 교룡 여의가 있고, 유수 지대가 활성화되지 않았다면
            if (GetEffect(so) != null)
            {
                // 필드 이펙트 - 유수 지대 활성화
                BattleManager.instance.SetFieldEffect(fieldEffectSO);
            }
        }
    }
    #endregion
}
