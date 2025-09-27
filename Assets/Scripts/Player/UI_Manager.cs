using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [Header("---Character Status---")]
    [SerializeField] private GameObject statusSet;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Slider hpSlider;


    [Header("---Ego---")]
    [SerializeField] private List<UI_Ego_Slot> slot_Ego;


    [Header("---Skill---")]
    [SerializeField] private List<GameObject> slot_Skill;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Player Status
    public void Status_Setting(Character_Base data, bool isOn)
    {
        // ���� �������ͽ�

        // ���� ������

        // ��ų ������

        // UI On
        statusSet.SetActive(isOn);
    }
    #endregion
}
