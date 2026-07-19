using System.Collections.Generic;
using System.Text;


public static class SkillDescriptionBuilder
{
    // 스킬 정보를 읽어 자동으로 string 화 시키는 로직

    public static string MakeDescription(SkillSO skillSO, int sync)
    {
        // 사용 시 발동 효과 텍스트화
        StringBuilder sd = new StringBuilder();
        for (int i = 0; i < skillSO.syncDatas[sync].skillEffects.Count; i++)
        {
            // 조건 텍스트화
            string triggerText = GetTriggerText(skillSO.syncDatas[sync].skillEffects[i].Trigger);
            string conditionText = GetConditionText(skillSO.syncDatas[sync].skillEffects[i]);
            string actionText = GetActionText(skillSO.syncDatas[sync].skillEffects[i].Action);

            // 데이터 추가
            sd.Append($"[{triggerText}] {conditionText} {actionText}\n");
        }

        // 코인 효과 텍스트화
        List<CoinInfoSO> coinInfo = skillSO.syncDatas[sync].coins;
        for (int i = 0; i < coinInfo.Count; i++)
        {
            // 코인 표시 텍스트
            sd.Append($"{i + 1} 코인\n");

            // 조건 & 동작 데이터
            for (int j = 0; j < coinInfo[i].EffectNodes.Count; j++)
            {
                // 트리거 - ([사용 시], [피격 시], [합 승리 시] 등등)
                string triggerText = GetTriggerText(coinInfo[i].EffectNodes[j].Trigger);

                // 조건 - (n1이 n2 이상이면, n1 + n2 의 합이 n3 이상이면 등등)
                string conditionText = GetConditionText(coinInfo[i].EffectNodes[j]);

                // 효과 - (누구에게 + 체력 50 회복, 실드 25% 획득, 주는 데미지 50% 증가 등등)
                string actionText = GetActionText(coinInfo[i].EffectNodes[j].Action);

                // 텍스트 조립 ([트리거] [조건] [동작])
                sd.Append($"[{triggerText}] {conditionText} {actionText}\n");
            }
        }

        return sd.ToString();
    }

    /// <summary>
    /// 트리거 조건 텍스트 전환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    private static string GetTriggerText(TriggerType type)
    {
        string re = type switch
        {
            TriggerType.SkillUse => "스킬 사용 시",
            TriggerType.Hit => "적중 시",
            TriggerType.SkillEnd => "스킬 종료 시",

            TriggerType.ClashWin => "합 승리 시",
            TriggerType.ClashLose => "합 패배 시",

            TriggerType.TurnStart => "턴 시작 시",
            TriggerType.TurnEnd => "턴 종료 시",

            TriggerType.None => "",
            _ => $"<Unknown Trigger : {type}>",
        };

        return re;
    }

    /// <summary>
    /// 발동 조건 텍스트 전환 함수
    /// </summary>
    /// <returns></returns>
    private static string GetConditionText(EffectNode node)
    {
        /* 구버전 -> 노드 변경 이전 코드
        // 값
        StringBuilder valueText = new StringBuilder();
        for (int i = 0; i < node.Values.Count; i++)
        {
            valueText.Append($"{node.Values[i].effect.EffectName} {GetEffectTypeText(node.Values[i].valueType)}");

            if (i < node.Values.Count - 1)
                valueText.Append(" + ");
        }
        valueText.Append(node.Values.Count == 1 ? "(이)가" : "의 합이");

        // 조건 
        string conditionText = GetConditionText(node.Compare);

        // 조합
        string re = $"{valueText.ToString()} {node.ConditionValue} {conditionText}";
        */

        // 신버전 -> 노드 변경 이후 코드 (26.07.15)
        // 값
        StringBuilder valueText = new StringBuilder();
        for (int i = 0; i < node.Condition.values.Count; i++)
        {
            EffectNode.EffectValue val = node.Condition.values[i];
            valueText.Append($"{val.effect.EffectName} {GetEffectTypeText(val.valueType)}");

            if (i < node.Condition.values.Count - 1)
                valueText.Append(" + ");
        }
        valueText.Append(node.Condition.values.Count == 1 ? "(이)가" : "의 합이");

        // 조건
        string conditionText = GetConditionText(node.Condition.compareType);

        // 조합
        string re = $"{valueText.ToString()} {node.Condition.conditionValue} {conditionText}";
        return re;
    }

    /// <summary>
    /// 비교 조건 텍스트 전환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string GetConditionText(EffectNode.CompareType type)
    {
        string re = type switch
        {
            EffectNode.CompareType.None => "",
            EffectNode.CompareType.Equal => "이라면 ",
            EffectNode.CompareType.LessEqual => "이하라면 ",
            EffectNode.CompareType.GreaterEqual => "이상이라면 ",
            _ => "",
        };

        return re;
    }

    /// <summary>
    /// 이펙트 종류 (위력, 횟수) 텍스트 전환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string GetEffectTypeText(ValueType type)
    {
        string re = type switch
        {
            ValueType.Power => "위력",
            ValueType.Count => "횟수",
            _ => "",
        };
        return re;
    }

    /// <summary>
    /// 동작 액션 텍스트 전환 함수
    /// </summary>
    /// <returns></returns>
    private static string GetActionText(EffectNode.ActionNode node)
    {
        /* 구버전
        StringBuilder sb = new StringBuilder();

        string re = string.Empty;
        for (int i = 0; i < nodes.Count; i++)
        {
            // 액션 텍스트 작성
            re = nodes[i].actionType switch
            {
                // 효과 추가 & 제거
                ActionType.AddEffect => $"{nodes[i].valueNode.effect.EffectName} {GetEffectTypeText(nodes[i].valueNode.valueType)} {nodes[i].actionValue} 부여",
                ActionType.RemoveEffect => $"{nodes[i].valueNode.effect.EffectName} {GetEffectTypeText(nodes[i].valueNode.valueType)} 제거",

                // 회복 & 실드
                ActionType.HealHp => $"체력 {nodes[i].actionValue} 회복",
                ActionType.Shield => $"실드 {nodes[i].actionValue} 획득",

                // 속성 데미지 & 퍼센트 데미지
                ActionType.Damage => $"{nodes[i].sinType} 속성 피해 {nodes[i].actionValue}",
                ActionType.DamageRatio => $"최종 피해량의 {nodes[i].actionValue}% 만큼 추가 데미지",

                // 데미지 증가 & 치피 증가
                ActionType.DamageMultiplier => $"데미지 {nodes[i].actionValue}% 증가",
                ActionType.CriticalMultiplier => $"치명타 피해량 {nodes[i].actionValue}% 증가",

                // 코인 & 스킬 재사용
                ActionType.ReuseCoin => "이 코인을 재사용",
                ActionType.ReuseSkill => "이 스킬을 재사용",

                // 오리지널 액션
                ActionType.Original => nodes[i].actionDescription,
                _ => "",
            };

            sb.Append(re);

            if (i < nodes.Count - 1)
            {
                sb.Append(", ");
            }
        }
        */

        /* 신버전 1
        StringBuilder sb = new StringBuilder();
        string re = string.Empty;
        for (int i = 0; i < node.valueNode.Count; i++)
        {
            // 액션 텍스트 작성
            re = node.actionType switch
            {
                // 효과 추가 & 제거
                ActionType.AddEffect => $"{node.valueNode[i].effect.EffectName} {GetEffectTypeText(node.valueNode[i].valueType)} {node.valueNode[i].value} 부여",
                ActionType.RemoveEffect => $"{node.valueNode[i].effect.EffectName} {GetEffectTypeText(node.valueNode[i].valueType)} 제거",

                // 회복 & 실드
                ActionType.HealHp => $"체력 {node.valueNode[i].value} 회복",
                ActionType.Shield => $"실드 {node.valueNode[i].value} 획득",

                // 속성 데미지 & 퍼센트 데미지
                ActionType.Damage => $"{node.actionDescription}", // 데이터에서 sin값 제거함! => 오리지널 텍스트로 작성 필요
                ActionType.DamageRatio => $"최종 피해량의 {node.valueNode[i].value}% 만큼 추가 데미지",

                // 데미지 증가 & 치피 증가
                ActionType.DamageMultiplier => $"데미지 {node.valueNode[i].value}% 증가",
                ActionType.CriticalMultiplier => $"치명타 피해량 {node.valueNode[i].value}% 증가",

                // 코인 & 스킬 재사용
                ActionType.ReuseCoin => "이 코인을 재사용",
                ActionType.ReuseSkill => "이 스킬을 재사용",

                // 오리지널 액션
                ActionType.Original => node.actionDescription,
                _ => "",
            };

            sb.Append(re);
            // 호흡, 출혈 같이 위력과 횟수를 한줄에 보여줘야 하는 옵션이라면 , 로 구분
            if (i < node.valueNode.Count - 1)
            {
                sb.Append(", ");
            }
        }
        */

        // 신버전 2 - 데이터 전환 후 로직
        StringBuilder sb = new StringBuilder();
        string re = string.Empty;

        // 효과
        for (int i = 0; i < node.valueNode.Count; i++)
        {
            re = string.Empty;

            // 오리지널 효과인지 체크
            if (node.actionType == ActionType.Original)
            {
                re = node.actionDescription;
            }
            else
            {
                // 여기에 키워드인지, 공용 이펙트인지 체크 필요함!
                // 키워드는 위력, 횟수로 표시하지만
                // 공용 이펙트는 붙는 어미가 달라져야함!

                // 공용 버전 : 효과의 이름, 값, 유지 턴 셋팅
                


                // 키워드 버전 : 효과의 이름, 타입, 값 셋팅
                EffectNode.EffectValue val = node.valueNode[i];
                re = $"{val.effect.EffectName} {GetEffectTypeText(val.valueType)} {val.value}";
            }

            sb.Append(re);

            // 호흡, 출혈 같이 위력과 횟수를 한줄에 보여줘야 하는 옵션이라면 , 로 구분
            if (i < node.valueNode.Count - 1)
            {
                sb.Append(", ");
            }
        }

        // 종결 어미
        sb.Append(node.actionType switch
        {
            ActionType.None => "",
            ActionType.AddEffect => "를 부여",
            ActionType.RemoveEffect => "를 제거",
            ActionType.Original => "",
            _ => ""
        });

        return sb.ToString();
    }
}
