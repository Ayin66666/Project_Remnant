using System.Collections.Generic;
using UnityEngine;


public class OrganizationUI : MonoBehaviour
{
    public static OrganizationUI instance;

    [Header("---Setting---")]
    // 캐릭터 창 On 시 최초 1회 스킬 & 에고 데이터를 받아온 뒤 표시하는 슬롯들 모음 (Instantiate 후 리스트에 추가)
    [SerializeField] private List<SkillDescriptionSlot> skillDescriptionSlots;
    [SerializeField] private List<SkillDescriptionSlot> egoDescriptionSlots;


    [Header("---UI---")]
    [SerializeField] private GameObject selectListUI;
    [SerializeField] private GameObject egoListUI;
    [SerializeField] private GameObject identityDescriptionUI;





    private void Start()
    {
        instance = this;
    }


    /// <summary>
    /// 인격 리스트 표시
    /// </summary>
    /// <param name="isOn"></param>
    public void IdentityListUI(bool isOn)
    {
        selectListUI.SetActive(isOn);
        if(!isOn) egoListUI.SetActive(false);
    }

    /// <summary>
    /// 에고 UI 표시
    /// </summary>
    /// <param name="isOn"></param>
    public void EgoListUI(bool isOn)
    {
        egoListUI.SetActive(!isOn);
    }

    /// <summary>
    /// 인격 상세정보 표시 - 캐릭터 슬롯 꾹 클릭
    /// </summary>
    public void ShowSinnerDescriptionUI(bool isOn, CharacterId sinner)
    {
        // UI 설정

        // UI On
        identityDescriptionUI.SetActive(isOn);
    }
}
