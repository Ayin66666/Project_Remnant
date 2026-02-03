using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 캐릭터 설명 UI 중 캐릭터의 요약 UI 담당 스크립트
/// </summary>
public class CharacterSummation : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private List<EgoDescriptionSlot> egoSlots; // 에고는 슬롯 복제하는거 아님!
    [SerializeField] private List<SkillDescriptionSlot> skillSlots; // 스킬은 복제 맞음!


    [Header("---UI---")]
    [SerializeField] private RectTransform egoRect;
    [SerializeField] private RectTransform skillRect;


    [Header("---Prefab---")]
    [SerializeField] private GameObject egoSlot_Prefab;
    [SerializeField] private GameObject skillSlot_Prefab;


    /// <summary>
    /// 데이터를 받아와 슬롯에 전달 & 배치
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(OrganizationData data)
    {
        Clear();

        // 에고 데이터 설정
        for (int i = 0; i < data.ego.Count; i++)
        {
            egoSlots[i].SetUp(data.ego[i]);
        }

        // 스킬 데이터 설정 - IdentityMasterSO 에 스킬 데이터 추가 필요!
        // for(int i = 0; i < data.identity.master.s)
    }

    /// <summary>
    /// 복제된 슬롯 초기화 & 데이터 삭제
    /// </summary>
    public void Clear()
    {
        foreach (EgoDescriptionSlot slot in egoSlots)
        {
            slot.Clear();
        }

        foreach(var obj in skillSlots)
        {
            Destroy(obj);
        }
        skillSlots.Clear();
    }
}
