using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OrganizationData
{
    public Character character;
    public Player_Base body;
    public List<EgoData> egoList;
}

[System.Serializable]
public class EgoData
{
    public EgoGrade egoGrade;
    public Skill_Base ego;
}


public class Organization_Manager : MonoBehaviour
{
    /*
     * ����� �۾�
     * 1. �� ������ ����
     * 2. ���� ������ ����
     * 3. ���� ĳ���� Ǯ ����
    */

    public static Organization_Manager instance;

    [Header("---holding Data---")]
    public List<Character_ListDataSO> characterData; // �ΰ� & ���� ���� ������


    [Header("---Organization Data---")]
    public List<OrganizationData> organizationList; // �ΰ� & ���� �� ������
    public List<Character> organizationOrderList; // �� ���� ������


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
    /// �ΰ� ����
    /// </summary>
    /// <param name="character"></param>
    /// <param name="body"></param>
    public void Change_Body(Character character, Player_Base body)
    {
        var data = organizationList.Find(o => o.character == character);
        if (data != null) data.body = body;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="character"></param>
    /// <param name="egoGrade"></param>
    /// <param name="ego"></param>
    public void Change_Ego(Character character, EgoGrade egoGrade, Skill_Base ego)
    {
        var data = organizationList.Find(o => o.character == character);

        if (data != null)
        {
            var egoData = data.egoList.Find(o => o.egoGrade == egoGrade);
            if(egoData != null) egoData.ego = ego;
        }
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    /// <param name="character"></param>
    /// <param name="isAdd"></param>
    public void Change_Organization(Character character)
    {
        if(organizationOrderList.Contains(character))
        {
            // �̹� �� ���¶�� - �� ����
            organizationOrderList.Remove(character);
        }
        else
        {
            // �� ���°� �ƴ϶�� - �߰�
            organizationOrderList.Add(character);
        }
    }
}
