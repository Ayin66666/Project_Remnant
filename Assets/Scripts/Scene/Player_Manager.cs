using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ChapterData
{
    public List<StageData> stageList;
}

[System.Serializable]
public class StageData
{
    public bool isClear;
    public string stageSceneName;
}


public class Player_Manager : MonoBehaviour
{
    /*
     * 1. ���� - ����ȭ���� �ΰݵ� ǥ��
     * 2. �ϴ� ��ư Ŭ�� �� â �Ѿ��
     * 3. ������ ���� �̸�����?
     */
    public static Player_Manager instacne;


    [Header("---Main UI---")]
    [SerializeField] private GameObject[] uiSet;
    [SerializeField] private GameObject exitSet;
    [SerializeField] private GameObject entrycheckSet;


    [Header("---Chapter & Stage Data---")]
    [SerializeField] private List<ChapterData> chapterData;
    [SerializeField] private int curChapterIndex;
    [SerializeField] private int curSTageIndex;


    private void Awake()
    {
        if (instacne == null)
        {
            instacne = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }



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


    #region ���� ����
    /// <summary>
    /// ���� ���� Ŭ��
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
            exitSet.SetActive(isExit);
        }
    }
    #endregion


    #region ����â
    /// <summary>
    /// ����â�� ĳ���� ��ư Ŭ�� (�ʻ�ȭ ����)
    /// </summary>
    /// <param name="index"></param>
    public void Click_CharacterChange(int index)
    {
        // �̰� ������ �ΰ� 1���ε� �ʿ��Ѱ�?
    }

    /// <summary>
    /// ĳ���� �ʻ�ȭ Ŭ�� (��� ���)
    /// </summary>
    /// <param name="index"></param>
    public void Click_CharacterPortrait(int index)
    {
        // ��� �����ʹ� Player_Base����? - ������ ������?
    }
    #endregion


    #region ������
    /// <summary>
    /// �������� ���� ��ư Ŭ��
    /// </summary>
    /// <param name="index"></param>
    public void Click_Chapter(int ChapterIndex)
    {
        // 1. é�� Ŭ�� �� �ش� é�� UI ǥ��

        // 2. é�� �� �������� Ŭ�� �� �ش� �������� ���� ���� üũ

        // 3. ���� Ŭ�� �� �������� ����
    }

    /// <summary>
    /// é�� �� �������� ���� ��ư Ŭ��
    /// </summary>
    /// <param name="charpterIndex"></param>
    /// <param name="stageIndex"></param>
    public void Click_Stage(int charpterIndex, int stageIndex)
    {
        // ���� ������ ����
        curChapterIndex = charpterIndex;
        curSTageIndex = stageIndex;

        // UI ǥ��
        entrycheckSet.SetActive(true);
    }

    /// <summary>
    /// �������� ���� ��ư Ŭ��
    /// </summary>
    /// <param name="isIn"></param>
    public void Click_Stage(bool isIn)
    {
        if(isIn)
        {
            // ������ üũ
            if(curChapterIndex == -1)
            {
                Debug.Log("���� �Ұ� - ������ ����!");
                return;
            }

            // �� �ε� - �Ŵ��� �߰� �ʿ�
            // Load_Manager.LoadScene(chapterData[curChapterIndex].stageList[curSTageIndex]);
        }
        else
        {
            // ������ �ʱ�ȭ
            curChapterIndex = -1;
            curSTageIndex = -1;

            // ���� ����
            entrycheckSet.SetActive(false);
        }
    }
    #endregion
}
