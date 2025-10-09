using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;


    [Header("---Main UI---")]
    [SerializeField] private GameObject[] uiSet;
    [SerializeField] private GameObject exitSet;
    [SerializeField] private GameObject entrycheckSet;


    [Header("---Organization Character List---")]
    [SerializeField] private GameObject organization_CharacterList;
    [SerializeField] private List<GameObject> organization_ListSlot;
    [SerializeField] private List<Character_Slot> character_Slots;


    [Header("---Character Description---")]
    [SerializeField] private GameObject character_Descriptionset;


    [Header("---Chapter & Stage---")]
    [SerializeField] private GameObject[] chapterUISet;


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

    #region ���� ȭ��
    /// <summary>
    /// ���� ȭ�� �ϴ� ��ư Ŭ��
    /// 0 : ����â 
    /// 1 : ��
    /// 2 : ������
    /// </summary>
    /// <param name="index"></param>
    public void Click_Button(int index)
    {
        foreach (GameObject item in uiSet)
        {
            item.SetActive(false);
        }

        uiSet[index].SetActive(true);
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void Click_Exit()
    {
        exitSet.SetActive(true);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Exit(bool isExit)
    {
        if (isExit)
        {
            // ���� ����
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
        else
        {
            // ���� UI Off
            exitSet.SetActive(false);
        }
    }
    #endregion


    #region ��â
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
        character_Descriptionset.SetActive(isOn);
        if (!isOn)
        {

        }
    }
    #endregion


    #region ������
    public void EntrycheckUI(bool isOn)
    {
        entrycheckSet.SetActive(isOn);
    }

    /// <summary>
    /// é�� Ŭ�� �� ������ �޾ƿ��� & UI ǥ��
    /// </summary>
    /// <param name="data">ǥ���� é�� ������</param>
    public void chapterUI(ChapterData_SO data, int chapterIndex)
    {
        // ������ ����

        // é�� UI ǥ��
        chapterUISet[chapterIndex].SetActive(true);
    }
    #endregion
}
