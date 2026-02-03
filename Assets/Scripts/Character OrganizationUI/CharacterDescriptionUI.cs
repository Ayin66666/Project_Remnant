using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDescriptionUI : MonoBehaviour
{
    public static CharacterDescriptionUI instance;


    [Header("---UI---")]
    private GameObject[] uiSet;


    [Header("---Prefab---")]
    [SerializeField] private GameObject skillSlotPrefab;
    [SerializeField] private GameObject egoSlotPrefab;


    private void Awake()
    {
        instance = this;
    }


    #region 최초 & 종료 시 실행 
    public void SetUp()
    {
        // 데이터 초기화
        Clear();

        // 최초 1회 실행 - 캐릭터의 편성 데이터를 받아와서 UI에 전달

        // 캐릭터 능력치

        // 에고 UI

        // 스킬 UI

        // 패시브

    }

    public void Clear()
    {

    }
    #endregion


    #region 클릭 이벤트
    /// <summary>
    /// 0 : Summation
    /// 1 : Skill
    /// 2 : Ego
    /// 3 : Mentality
    /// </summary>
    /// <param name="index"></param>
    public void UISetting(int index)
    {
        foreach (GameObject obj in uiSet)
        {
            obj.SetActive(false);
        }

        uiSet[index].SetActive(true);
    }
    #endregion


    #region 스킬 & 에고 슬롯 이벤트
    /// <summary>
    /// 스킬 상세정보 - 스킬 설명 슬롯 클릭 시 호출
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="skill"></param>
    public void ShowSkillSlotDescription(bool isOn, SkillBase skill)
    {
        // 받은 정보를 기반으로 데이터 표시
        // - 이거 UI가 슬롯 옆에 있어야 하는데?
    }

    /// <summary>
    /// 인격 상세정보 - 에고 설명 슬롯 클릭 시 호출
    /// </summary>
    /// <param name="info"></param>
    public void ShowEgoSlotDescription(bool isOn, EgoData info)
    {
        // 받은 정보를 기반으로 데이터 표시
        // - 이거 UI가 슬롯 옆에 있어야 하는데?
    }
    #endregion
}
