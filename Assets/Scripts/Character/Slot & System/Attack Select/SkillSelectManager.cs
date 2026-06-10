using System.Collections.Generic;
using UnityEngine;


public class SkillSelectManager : MonoBehaviour
{
    public static SkillSelectManager instance;

    [Header("---Data---")]
    [SerializeField] private bool isSelecting;
    [SerializeField] private int lastIndex;
    [SerializeField] private List<SelectNodeGroup> skillNodeGroups;

    [Header("---Component---")]
    [SerializeField] private SelectStartPoint startPoint;
    public SelectStartPoint StartPoint => startPoint;

    [Header("---UI---")]
    [SerializeField] private RectTransform skillGroupRect;
    [SerializeField] private GameObject nodeGroupPrefab;
    [SerializeField] private GameObject linePrefab;


    private void Awake()
    {
        // 임시로 싱글톤 처리하였으나
        // 추후 BattleManager 경유 방식 고려중
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        isSelecting = Input.GetMouseButton(0);
    }


    public void SkillSelect(SelectNodeGroup group)
    {
        // 선택모드인지 체크
        if (!isSelecting) 
            return;

        // 시작 지점보다 이전 노드를 선택했다면
        if (group.GroupIndex < lastIndex)
        {
            // 해당 노드 앞의 선택 데이터를 초기화 데이터 초기화
        }

        // 시작 노드를 선택했다면
        if (group.GroupIndex == lastIndex)
        {
            // 시작 노드의 데이터 변경 후 시작
        }

        // 다음 노드를 선택했다면
        if (group.GroupIndex == lastIndex + 1)
        {
            // 바로 선택 데이터 입력
        }

        // 다음 이후(2번째~) 노드를 선택했다면
        if (group.GroupIndex >= lastIndex + 2)
        {
            return;
        }
    }


    #region 그룹 추가 & 사망 처리
    /// <summary>
    /// 캐릭터 공격 선택 그룹 추가 함수
    /// </summary>
    /// <param name="owner"></param>
    public void AddNodeGroup(PlayerCharacter owner)
    {
        // 슬롯 오브젝트 생성 & 데이터 초기화   
        GameObject groupObj = Instantiate(nodeGroupPrefab, skillGroupRect);
        SelectNodeGroup groupScr = groupObj.GetComponent<SelectNodeGroup>();
        groupScr.SetUp(owner);

        skillNodeGroups.Add(groupScr);
    }

    /// <summary>
    /// 죽은 캐릭터의 공격 선택 그룹 비활성화 함수
    /// </summary>
    /// <param name="owner"></param>
    public void DeactivateNodeGroup(CharacterBase owner)
    {
        // 일단은 삭제 처리지만
        // 추후 사망 캐릭터의 노드는 사망 표시 후 사망자 rect에 넣을 예정
        List<SelectNodeGroup> groups = skillNodeGroups.FindAll(x => x.owner == owner);
        for (int i = groups.Count - 1; i > 0; i--)
        {
            Destroy(groups[i].gameObject);
        }
    }
    #endregion
}
