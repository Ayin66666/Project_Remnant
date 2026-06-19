using System.Collections.Generic;
using System.Text;

public static class SkillDescriptionBuilder
{
    // 스킬 정보를 읽어 자동으로 string 화 시키는 로직

    public static string MakeDescription(SkillSO skillSO, int sync)
    {
        // 사용 시 발동 효과 데이터 텍스트화
        StringBuilder sd = new StringBuilder();
        for (int i = 0; i < skillSO.syncDatas[sync].skillEffects.Count; i++)
        {
            // 조건 텍스트화
            string triggerText = GetTriggerText(skillSO.syncDatas[sync].skillEffects[i].Trigger);
            string conditionText = GetConditionText(skillSO.syncDatas[sync].skillEffects[i]);
            string actionText = GetActionText(skillSO.syncDatas[sync].skillEffects[i].Actions);

            // 데이터 추가
            sd.Append($"[{triggerText}] {conditionText} {actionText}\n");
        }

        // 코인 효과 데이터 텍스트화
        List<SkillSO.CoinInfo> coinInfo = skillSO.syncDatas[sync].coins;
        for (int i = 0; i < coinInfo.Count; i++)
        {
            // 코인 표시 텍스트
            sd.Append($"{i + 1} 코인\n");

            // 조건 & 동작 데이터
            for (int j = 0; j < coinInfo[i].effectNodes.Count; j++)
            {
                // 트리거 - ([사용 시], [피격 시], [합 승리 시] 등등)
                string triggerText = GetTriggerText(coinInfo[i].effectNodes[j].Trigger);

                // 조건 - (n1이 n2 이상이면, n1 + n2 의 합이 n3 이상이면 등등)
                string conditionText = GetConditionText(coinInfo[i].effectNodes[j]);

                // 효과 - (체력 50 회복, 실드 25% 획득, 주는 데미지 50% 증가 등등)
                string actionText = GetActionText(coinInfo[i].effectNodes[j].Actions);

                // 텍스트 조립
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
        return re;
    }

    /// <summary>
    /// 이펙트 종류 (위력, 횟수) 텍스트 전환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string GetEffectTypeText(EffectNode.ValueType type)
    {
        string re = type switch
        {
            EffectNode.ValueType.Power => "위력",
            EffectNode.ValueType.Count => "횟수",
            _ => "",
        };
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
            EffectNode.CompareType.Equal => "이라면 ",
            EffectNode.CompareType.LessEqual => "이하라면 ",
            EffectNode.CompareType.GreaterEqual => "이상이라면 ",
            _ => "",
        };

        return re;
    }

    /// <summary>
    /// 동작 액션 텍스트 전환 함수
    /// </summary>
    /// <returns></returns>
    private static string GetActionText(List<EffectNode.ActionNode> nodes)
    {
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

            if(i < nodes.Count-1)
            {
                sb.Append(", ");
            }
        }

        return sb.ToString();
    }
}
