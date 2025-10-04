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
     * 담당할 작업
     * 1. 편성 데이터 저장
     * 2. 에고 데이터 저장
     * 3. 보유 캐릭터 풀 저장
    */

    public static Organization_Manager instance;

    [Header("---holding Data---")]
    public List<Character_ListDataSO> characterData; // 인격 & 에고 보유 데이터


    [Header("---Organization Data---")]
    public List<OrganizationData> organizationList; // 인격 & 에고 편성 데이터
    public List<Character> organizationOrderList; // 편성 순서 데이터


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
    /// 인격 변경
    /// </summary>
    /// <param name="character"></param>
    /// <param name="body"></param>
    public void Change_Body(Character character, Player_Base body)
    {
        var data = organizationList.Find(o => o.character == character);
        if (data != null) data.body = body;
    }

    /// <summary>
    /// 에고 변경
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
    /// 편성 순서 설정
    /// </summary>
    /// <param name="character"></param>
    /// <param name="isAdd"></param>
    public void Change_Organization(Character character)
    {
        if(organizationOrderList.Contains(character))
        {
            // 이미 편성 상태라면 - 편성 제거
            organizationOrderList.Remove(character);
        }
        else
        {
            // 편성 상태가 아니라면 - 추가
            organizationOrderList.Add(character);
        }
    }
}
