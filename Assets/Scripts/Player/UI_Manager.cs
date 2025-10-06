using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [Header("---Organization Character List---")]
    [SerializeField] private GameObject organization_CharacterList;
    [SerializeField] private List<GameObject> organization_ListSlot;
    [SerializeField] private List<Character_Slot> character_Slots;


    [Header("---Character Description---")]
    [SerializeField] private GameObject characterDescriptionset;



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


    /// <summary>
    /// ĳ���� ����Ʈ On Off
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="character"></param>
    public void Organization_CharacterList(bool isOn, Character_ListDataSO data)
    {
        organization_CharacterList.SetActive(isOn);
        if (isOn)
        {
            // ���� �� ������ ����
            foreach (Character_Slot slot in character_Slots)
            {
                slot.Reset_Slot();
            }

            // ������ ǥ��
            for (int i = 0; i < data.characterBodyList.Count; i++)
            {
                character_Slots[i].Setting(data.character, data.characterBodyList[i].body);
            }

            // ���� ���� ��Ȱ��ȭ
            foreach (Character_Slot slot in character_Slots)
            {
                if (slot.character == Character.None)
                    slot.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ��� ĳ���� ����Ʈ ������ ���õ� ǥ�� Off
    /// </summary>
    public void SelectedOff()
    {
        foreach(Character_Slot slot in character_Slots)
        {
            slot.selectedText.SetActive(false);
        }
    }

    /// <summary>
    /// ĳ���� ����Ʈ���� ������ ĳ������ �󼼼��� UI OnOff
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="character"></param>
    public void Character_Description(bool isOn, Character_Base character)
    {
        characterDescriptionset.SetActive(isOn);
        if (!isOn)
        {

        }
    }
}
