using Game.Character;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager instance;


    [Header("---현제 편성중인 캐릭터의 데이터---")]
    [SerializeField] private CharacterId curSinner;
    /// <summary>
    /// 외부 할당용
    /// </summary>
    [SerializeField] private List<CharacterSlot> sinnerSlots;
    /// <summary>
    /// 실제 로직 동작용
    /// </summary>
    [SerializeField] private Dictionary<CharacterId, CharacterSlot> sinnerSlotDic;
    /// <summary>
    /// 외부 할당용
    /// </summary>
    [SerializeField] private List<EgoEquipSlot> egoSlots;
    /// <summary>
    /// 실제 로직 동작용
    /// </summary>
    [SerializeField] private Dictionary<Rank, EgoEquipSlot> egoSlotDic;
    public CharacterId CurSinner => curSinner;

    [Header("---UI---")]
    [SerializeField] private GameObject organizationUI;
    [SerializeField] private GameObject selectUI;
    [SerializeField] private GameObject identityListUI;
    [SerializeField] private GameObject egoListUI;
    [SerializeField] private GameObject stageInUI;
    [SerializeField] private GameObject backgroundUI;

    [Header("---Component---")]
    [SerializeField] private SlotPooling pooling;


    #region 시작 데이터 세팅 로직
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // 딕셔너리에 데이터 할당
        sinnerSlotDic = sinnerSlots.ToDictionary(x => x.SlotOnwer);
        egoSlotDic = egoSlots.ToDictionary(x => x.SlotRank);
    }
    #endregion

    /// <summary>
    /// 편성창 UI On/Off
    /// </summary>
    /// <param name="isOn">편성창 UI On/Off 여부</param>
    /// <param name="isStageIn">스테이지 선택 - 진입 전 편성창인지 여부 체크</param>
    public void OrganizationUI(bool isOn, bool isStageIn)
    {
        // 하단 UI 종료
        MainSceneManager.instance.BottomUISetting(isStageIn);

        // 배경 UI 활성화
        backgroundUI.SetActive(isStageIn);

        // 스테이지 진입 UI 활성화
        stageInUI.SetActive(isStageIn);

        // 편성 UI 활성화 -> 이때 UI 레이아웃 이슈로 인해 스테이지 UI를 비활성화 하던가 해야할듯?
        organizationUI.SetActive(isOn);
    }


    #region 스테이지 진입 전 편성창 버튼 로직
    /// <summary>
    /// 스테이지 진입 버튼 클릭 - 스테이지 진입 (SceneLoadManager)
    /// </summary>
    public void ClickStageEnter()
    {
        // 데이터 저장
        GameManager.instance.saveDataManager.SaveData();

        // 입력으로 인한 로직은 어디에? - 배틀 컨텐츠 매니저가 관리하는게 맞지 않나?
        BattleContentManager.instance.StageEnter();

    }

    /// <summary>
    /// 스테이지 진입 취소 버튼 클릭 - 스테이지 선택 UI
    /// </summary>
    public void ClickStageOut()
    {
        // 데이터 저장
        GameManager.instance.saveDataManager.SaveData();

        // 관련 UI 비활성화
        BattleContentManager.instance.StageDescriptionUI(false);
        OrganizationUI(false, false);
    }
    #endregion


    #region 인격
    /// <summary>
    /// 캐릭터 클릭 -> 캐릭터 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenCharacterList(CharacterId id)
    {
        // UI 슬롯 초기화
        pooling.ClearIdentitySlot();
        pooling.ClearEgoSlot();

        // 리스트를 연 인격 저장
        curSinner = id;

        // 런타임 데이터 받아오기
        SinnerRuntimeData data = CharacterRosterManager.instance.GetIdentityData(id);
        if (data == null)
        {
            Debug.LogError($"에러 발생 {id} 인격 데이터가 없음!");
            return;
        }

        // 인격 데이터 세팅
        foreach (var iden in data.identityDic.Values)
        {
            if (iden.isUnlocked)
            {
                // 풀링에서 슬롯 받아오기
                CharacterSelectSlot slot = pooling.GetIdentitySlot();

                // 슬롯 데이터 할당 (인격 정보 & 편성 여부)
                bool result = CharacterRosterManager.instance.GetIdentityOrderData(iden.master.sinner);
                slot.SetUp(iden, result);
                slot.gameObject.SetActive(true);
            }
        }


        // 에고 슬롯 초기화
        foreach (var slot in egoSlots)
        {
            slot.Clear();
        }

        // 장착중인 에고 세팅
        OrganizationData egoData = CharacterRosterManager.instance.GetOrganizationData(id);
        foreach (var ego in egoData.ego.Values)
        {
            var slot = egoSlots.FirstOrDefault(x => x.SlotRank == ego.master.egoRank);
            slot?.LoadEquipEgo(ego);
        }

        // UI 오픈
        selectUI.SetActive(true);
        identityListUI.SetActive(true);
        egoListUI.SetActive(false);
    }

    /// <summary>
    /// 리스트 닫기
    /// </summary>
    public void CloseIdentityList()
    {
        pooling.ClearIdentitySlot();
        pooling.ClearEgoSlot();

        egoListUI.SetActive(false);
        selectUI.SetActive(false);
        identityListUI.SetActive(false);
    }

    /// <summary>
    /// 인격 선택 후 슬롯 데이터 업데이트
    /// </summary>
    public void UpdataSinnerSlot(IdentityData data)
    {
        // 해당 캐릭터의 슬롯 찾기
        if (sinnerSlotDic.ContainsKey(data.master.sinner))
            sinnerSlotDic[data.master.sinner].SetUp(data);
        else
            Debug.Log("해당 캐릭터의 슬롯이 존재하지 않습니다.");
    }

    /// <summary>
    /// 수감자 슬롯의 편성 순서 UI 업데이트
    /// </summary>
    public void UpdataSinnerSlotUI()
    {
        Debug.Log("호출");
        foreach (var slot in sinnerSlots)
        {
            slot.UpdataOrderUI();
            Debug.Log("슬롯 설정");
        }
    }
    #endregion


    #region 에고
    /// <summary>
    /// 에고 클릭 -> 에고 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenEgoList(Rank? egoRank)
    {
        // 리스트 클리어
        pooling.ClearEgoSlot();

        // 데이터 받아오기
        SinnerRuntimeData EgoData = CharacterRosterManager.instance.GetIdentityData(curSinner);
        foreach (var data in EgoData.egoDic.Values)
        {
            Debug.Log($"{data.master.egoName} / {data.master.egoRank} / {data.isUnlocked}");

            // 해금 여부 체크
            if (!data.isUnlocked)
                continue;

            // 랭크 필터
            if (egoRank != null && data.master.egoRank != egoRank)
                continue;

            // 슬롯 세팅
            EgoListSlot slot = pooling.GetEgoSlot();
            slot.SetUp(data);
            slot.gameObject.SetActive(true);
        }

        // UI
        egoListUI.SetActive(true);
        selectUI.SetActive(true);
        identityListUI.SetActive(false);
    }

    public void UpdataEgoSlot()
    {
        Debug.Log("호출");
        OrganizationData data = CharacterRosterManager.instance.GetOrganizationData(curSinner);
        foreach (var slot in egoSlots)
        {
            if (data.ego.ContainsKey(slot.SlotRank))
            {
                slot.ChangeEquipEgo(data.ego[slot.SlotRank]);
                Debug.Log($"슬롯 설정 {slot.SlotRank}");
            }
        }
    }
    #endregion
}


