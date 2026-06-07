using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectManager : MonoBehaviour
{
    [Header("---Data---")]
    [SerializeField] private List<SkillNodeGroup> skillNodeGroups;

    [Header("---UI---")]
    [SerializeField] private RectTransform skillGroupRect;
    [SerializeField] private GameObject nodeGroupPrefab;


    public void SkillSelect(SkillSelectNode skillSelectNode)
    {

    }

    public void SkillRemove()
    {

    }

    /// <summary>
    /// 캐릭터 공격 선택 그룹 추가 함수
    /// </summary>
    /// <param name="owner"></param>
    public void AddNodeGroup(CharacterBase owner)
    {
        // 슬롯 오브젝트 생성 & 데이터 초기화   
        GameObject groupObj = Instantiate(nodeGroupPrefab, skillGroupRect);
        SkillNodeGroup group = new SkillNodeGroup(owner, groupObj);
    }

    /// <summary>
    /// 캐릭터 공격 선택 그룹 제거 함수
    /// </summary>
    /// <param name="owner"></param>
    public void RemoveNodeGroup(CharacterBase owner)
    {

    }
}


[System.Serializable]
public class SkillNodeGroup
{
    [Header("---Setting---")]
    public CharacterBase owner;
    public Select selectNode;
    public enum Select
    {
        None,
        Upper,
        Lower
    }

    [Header("---Obj---")]
    public GameObject group;
    public SkillSelectNode upperNode;
    public SkillSelectNode lowerNode;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Slider hp;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mentalText;


    /// <summary>
    /// 생성자 - 초기화 로직
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="groupObj"></param>
    public SkillNodeGroup(CharacterBase owner, GameObject groupObj)
    {
        this.owner = owner;
        selectNode = Select.None;

        group = groupObj;
        upperNode = groupObj.transform.GetChild(0).GetComponent<SkillSelectNode>();
        lowerNode = groupObj.transform.GetChild(1).GetComponent<SkillSelectNode>();
    }
}
