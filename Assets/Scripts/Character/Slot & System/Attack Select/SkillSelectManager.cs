using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BattleManager;



public class SkillSelectManager : MonoBehaviour
{
    public static SkillSelectManager instance;

    [Header("---Data---")]
    [SerializeField] private bool isSelecting;
    [SerializeField] private int lastIndex;
    [SerializeField] private List<SelectNodeGroup> skillNodeGroups;
    private Coroutine selectCoroutine;

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

    /* -> 일반 전투 코드 / 제외함
    public void SkillSelect(SelectNodeGroup group)
    {
        // 선택모드인지 체크
        if (!isSelecting) 
            return;

        // 인덱스 받아오기
        int index = skillNodeGroups.IndexOf(group);

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
    */


    /// <summary>
    /// 공격 선택
    /// </summary>
    public void Selecting(SkillSelectNode node)
    {
        if(selectCoroutine != null) StopCoroutine(selectCoroutine);
        selectCoroutine = StartCoroutine(Line(node));
    }

    /// <summary>
    /// 선 그리기 로직
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private IEnumerator Line(SkillSelectNode node)
    {
        // 라인 소환
        RectTransform startRect = node.GetComponent<RectTransform>();
        GameObject lineObj = Instantiate(linePrefab, startRect);
        RectTransform lineRect = lineObj.GetComponent<RectTransform>();

        while (isSelecting)
        {
            // 위치 & dir 체크
            Vector2 startPos = startRect.position;
            Vector2 mousePos = Input.mousePosition;
            Vector2 dir = mousePos - startPos;

            // 회전 & 길이 조절
            lineRect.up = dir.normalized;
            lineRect.sizeDelta = new Vector2(lineRect.sizeDelta.x, dir.magnitude);

            yield return null;
        }

        // 라인 제거
        Destroy(lineObj);

        // 마우스 종료 위치에 슬롯이 있는지 체크
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldMousePos, Vector2.zero);
        SkillSlot targetSlot = hits.Select(x=>x.transform.gameObject.GetComponent<SkillSlot>())
            .OfType<SkillSlot>()
            .FirstOrDefault();

        // 만약 슬롯이 있다면
        if (targetSlot != null)
        {
            switch (targetSlot.Onwer.CharacterType)
            {
                // 플레이어 & 아군 Npc면 공격 불가
                case CharacterBase.CharacterGroup.Player:
                case CharacterBase.CharacterGroup.AllyNpc:
                    Debug.Log("아군 슬롯! 공격 불가!");
                    break;

                // 적이라면 공격 가능
                case CharacterBase.CharacterGroup.Enemy:
                    Debug.Log("적군 슬롯! 공격 가능!");

                    // 공격 리퀘스트 작성
                    AttackRequest request = new AttackRequest()
                    {
                        // 공격자 (플레이어)
                        owner = node.Group.owner,
                        ownerSlot = node.Slot,

                        // 방어자 (몬스터)
                        target =targetSlot.Onwer,
                        targetSlot =targetSlot,
                    };

                    // 공격 추가
                    BattleManager.instance.AddAttack(request);
                    break;
            }
        }

        // 코루틴 변수 초기화
        selectCoroutine = null;
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
