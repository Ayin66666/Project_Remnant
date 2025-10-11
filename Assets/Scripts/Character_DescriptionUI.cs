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
    /// ������ ����
    /// </summary>
    /// <param name="character"></param>
    public void Setting(Character_Base character)
    {
        // ������ ����
        data = character;

        // UI�� ������ ����

        // 1. Status

        // 2. Summation

        // 3. Passive

        // 4. EGO

        // 5. Mentality
    }

    /// <summary>
    /// ��� ��ư Ŭ�� �� UI ���� ���
    /// 0 : ���
    /// 1 : ��ų
    /// 2 : ����
    /// 3 : ���ŷ�
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
