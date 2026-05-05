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
    [SerializeField] private GameObject identityUI;
    [SerializeField] private GameObject egoListUI;
    [SerializeField] private CharacterDescription descriptionUI;


    private void Start()
    {
        instance = this;
    }


    /// <summary>
    /// 편성창 UI OnOff
    /// </summary>
    /// <param name="isOn"></param>
    public void OrganizationUISetting(bool isOn)
    {
        // 기본적으로 UI는 identityUI 가 켜지는게 기본
        selectListUI.SetActive(isOn);
        IdentityListUI(isOn);
    }

    /// <summary>
    /// 인격 리스트 OnOff
    /// </summary>
    /// <param name="isOn"></param>
    public void IdentityListUI(bool isOn)
    {
        identityUI.SetActive(isOn);
        egoListUI.SetActive(false);
    }

    /// <summary>
    /// 에고 UI OnOff
    /// </summary>
    /// <param name="isOn"></param>
    public void EgoListUI(bool isOn)
    {
        egoListUI.SetActive(isOn);
        identityUI.SetActive(false);
    }

    /// <summary>
    /// 인격 상세정보 표시 - 캐릭터 슬롯 꾹 클릭으로 동작
    /// </summary>
    public void ShowSinnerDescriptionUI(bool isOn, OrganizationData data)
    {
        // UI 설정
        if(isOn) descriptionUI.SetUp(data);

        // UI On
        descriptionUI.gameObject.SetActive(isOn);
    }
}
