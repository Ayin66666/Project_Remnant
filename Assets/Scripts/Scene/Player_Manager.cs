using System.Collections.Generic;
using UnityEngine;


public class Player_Manager : MonoBehaviour
{
    /*
     * 1. ���� - ����ȭ���� �ΰݵ� ǥ��
     * 2. �ϴ� ��ư Ŭ�� �� â �Ѿ��
     * 3. ������ ���� �̸�����?
     */
    public static Player_Manager instacne;

    [Header("---Chapter & Stage Data---")]
    [SerializeField] private List<ChapterData_SO> chapterData;

    [Header("---Selected Stage---")]
    [SerializeField] private StageData stageData;


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
    public void Click_Chapter(int chapterIndex)
    {
        // 1. é�� Ŭ�� �� �ش� é�� UI ǥ��
        UI_Manager.instance.ChapterUI(chapterData[chapterIndex], chapterIndex);
    }

    /// <summary>
    /// é�� �� �������� ���� ��ư Ŭ��
    /// </summary>
    /// <param name="charpterIndex"></param>
    /// <param name="stageIndex"></param>
    public void Click_Stage(int charpterIndex, int stageIndex)
    {
        // ���� ������ ����
        stageData = chapterData[charpterIndex].stageList[stageIndex];

        // UI ǥ��
        UI_Manager.instance.EntrycheckUI(true);
    }

    /// <summary>
    /// �������� ���� ��ư Ŭ��
    /// </summary>
    /// <param name="isIn"></param>
    public void Click_StageIn(bool isIn)
    {
        if(isIn)
        {
            // ������ üũ
            if(stageData != null)
            {
                Debug.Log("���� �Ұ� - ������ ����!");
                return;
            }

            // �� �ε� - �Ŵ��� �߰�
            Load_Manager.LoadScene(stageData.stageSceneName);
        }
        else
        {
            // ������ �ʱ�ȭ
            stageData = null;

            // ���� ����
            UI_Manager.instance.EntrycheckUI(false);
        }
    }
    #endregion
}
