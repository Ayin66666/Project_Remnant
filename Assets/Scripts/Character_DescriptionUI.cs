using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Character_DescriptionUI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiset;
    [SerializeField] private Character_Base data;


    [Header("---Description / Status---")]
    [SerializeField] private Image character_Icon;
    [SerializeField] private TextMeshProUGUI character_nameText;
    [SerializeField] private TextMeshProUGUI character_levelText;
    [SerializeField] private TextMeshProUGUI character_hpText;
    [SerializeField] private TextMeshProUGUI character_MentalityText;
    [SerializeField] private GameObject[] character_GradeIcons;


    [Header("---Description / Summation---")]
    [SerializeField] private List<Skill_Description_Slot> skillSlots;


    [Header("---Description / Passive---")]
    [SerializeField] private TextMeshProUGUI passiveTitleText1;
    [SerializeField] private TextMeshProUGUI passiveText1;
    [SerializeField] private TextMeshProUGUI passiveTitleText2;
    [SerializeField] private TextMeshProUGUI passiveText2;


    [Header("---Description / EGO---")]
    [SerializeField] private Image egoIcon;
    [SerializeField] private GameObject[] ego_AttackCountIcons;
    [SerializeField] private TextMeshProUGUI ego_NameText;
    [SerializeField] private TextMeshProUGUI attackValueText;
    [SerializeField] private GameObject[] ego_AttackRangeIcons;
    [SerializeField] private TextMeshProUGUI ego_DescriptionText;


    [Header("---Description / Mentality---")]
    [SerializeField] private TextMeshProUGUI mentality_UpText;
    [SerializeField] private TextMeshProUGUI mentality_DownText;


    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    /// <param name="character"></param>
    public void Setting(Character_Base character)
    {
        // 데이터 셋팅
        data = character;

        // UI에 데이터 전달

        // 1. Status

        // 2. Summation

        // 3. Passive

        // 4. EGO

        // 5. Mentality
    }

    /// <summary>
    /// 상단 버튼 클릭 시 UI 변경 기능
    /// 0 : 요약
    /// 1 : 스킬
    /// 2 : 에고
    /// 3 : 정신력
    /// </summary>
    /// <param name="index"></param>
    public void Click_Button(int index)
    {
        foreach(GameObject obj in uiset)
        {
            obj.SetActive(false);
        }

        uiset[index].SetActive(true);
    }
}
