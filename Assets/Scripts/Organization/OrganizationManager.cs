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

    [Header("---UI---")]
    [SerializeField] private GameObject selectUI;
    [SerializeField] private GameObject identityListUI;
    [SerializeField] private GameObject egoListUI;

    [Header("---Component---")]
    [SerializeField] private SlotPooling pooling;


    #region 시작 데이터 세팅 로직
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Application.targetFrameRate = 30;

        // 딕셔너리에 데이터 할당
        sinnerSlotDic = sinnerSlots.ToDictionary(x => x.SlotOnwer);
        egoSlotDic = egoSlots.ToDictionary(x => x.SlotRank);
    }

    public void ApplySaveData(SaveData saveData)
    {
        foreach(var data in saveData.organizationDatas)
        {
            // 데이터 삽입
            IdentityData idenitty = 
                CharacterRosterManager.instance
                .GetIdentityData(data.sinner)
                .identityDic[data.identityId];

            int index = sinnerSlots.FindIndex(x => x.SlotOnwer == idenitty.master.sinner);
            if(index != -1)
            {
                // 슬롯 설정
                sinnerSlots[index].SetUp(idenitty);

                // 순서 UI 업데이트
                sinnerSlotDic[data.sinner].OrderSetting();
            }
            else
            {
                Debug.Log("슬롯을 찾지 못함");
            }
        }
    }
    #endregion


    #region 인격
    /// <summary>
    /// 캐릭터 클릭 -> 캐릭터 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenCharacterList(CharacterId id)
    {
        curSinner = id;

        /* 구버전
        // 데이터 세팅 - 인격
        IdentityInfo info1 = CharacterRosterManager.instance.GetIdentityInfo(id);
        if (info1 == null)
        {
            Debug.Log("인격 정보 없음");
            return;
        }

        // 슬롯에 데이터 전달
        for (int i = 0; i < info1.info.Count; i++)
        {
            if (info1.info[i].isUnlocked)
            {
                CharacterSelectSlot slot = pooling.GetIdentitySlot();

                // 여기 일단은 false로 넣긴 하는데, 원래는 편성 여부 체크해서 넣어야 함!
                slot.SetUp(info1.info[i], false);
                slot.gameObject.SetActive(true);
            }
        }
        */

        // 런타임 데이터 받아오기
        SinnerRuntimeData data = CharacterRosterManager.instance.GetIdentityData(id);
        if (data == null)
        {
            Debug.LogError($"에러 발생 {id} 인격 데이터가 없음!");
            return;
        }

        // 데이터 세팅
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
    /// 슬롯 UI 업데이트
    /// </summary>
    public void UpdataSlotUI()
    {
        foreach (var slot in sinnerSlotDic.Values)
        {
            slot.OrderSetting();
        }
    }
    #endregion


    #region 에고
    /// <summary>
    /// 에고 클릭 -> 에고 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenEgoList(Rank rank)
    {
        // 리스트 클리어
        pooling.ClearEgoSlot();
        /*
        // 데이터 세팅 - ego
        EgoInfo info2 = egoInfo.Find(x => x.sinner == curSinner);
        Debug.Log(info2);
        Debug.Log(info2.info.Count);

        for (int i = 0; i < info2.info.Count; i++)
        {
            // 에고 해금 여부 & 에고 티어가 동일한지 체크
            if (info2.info[i].isUnlocked && info2.info[i].master.egoRank == rank)
            {
                // 에고 추가
                EgoListSlot slot = pooling.GetEgoSlot();
                slot.SetUp(info2.info[i]);
                slot.gameObject.SetActive(true);
            }
        }

        egoListUI.SetActive(true);
        selectUI.SetActive(true);
        identityListUI.SetActive(false);
        */
    }
    #endregion


    #region 편성 -> 이쪽 데이터베이스가 이동한다 치면 필요없을듯?
    /// <summary>
    /// 편성 인격 변경하기
    /// </summary>
    /// <param name="info"></param>
    public void OrganizingIdentity(IdentityData info)
    {
        // 데이터베이스로 전달
        CharacterRosterManager.instance.SetIdentity(info);
    }

    /// <summary>
    /// 편성 ego 변경
    /// </summary>
    /// <param name="info"></param>
    public void OrganizingEgo(EgoData info)
    {
        // 데이터베이스로 전달
        CharacterRosterManager.instance.SetEgo(info);
    }
    #endregion
}


