using System.Collections.Generic;
using UnityEngine;


public class SelectNodeGroup : MonoBehaviour
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

    [Header("---Node---")]
    [SerializeField] private SkillSelectNode upperNode;
    [SerializeField] private SkillSelectNode lowerNode;
    [SerializeField] private CharacterPortraitNode portraitNode;



    /// <summary>
    /// 그룹 데이터 세팅
    /// </summary>
    /// <param name="character"></param>
    public void SetUp(PlayerCharacter character)
    {
        // 오너 세팅
        owner = character;
        portraitNode.SetUp(character);
    }

    /// <summary>
    /// 선택된 스킬 데이터 표시
    /// </summary>
    /// <param name="skill"></param>
    public void SetSkillNodes(List<SkillBase> skill)
    {
        // 데이터 전달
        upperNode.SetUp(skill[0]);
        lowerNode.SetUp(skill[1]);
    }

    /// <summary>
    /// 현제 선택된 노드 설정
    /// </summary>
    public void Selected(Select type)
    {
        selectNode = type;
    }

    /// <summary>
    /// 노드 그룹 초기화
    /// </summary>
    public void Clear()
    {
        owner = null;
        upperNode.Clear();
        lowerNode.Clear();
        portraitNode.Clear();
    }
}
