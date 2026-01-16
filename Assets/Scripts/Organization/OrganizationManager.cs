using System.Collections.Generic;
using UnityEngine;


public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager instance;

    [Header("---Setting / Data---")]
    [SerializeField] private List<IdentityInfo> characterEgoInfo;

    [Header("---UI---")]
    [SerializeField] private GameObject characterListSet;
    [SerializeField] private List<CharacterSlot> characterSlot;

    [Header("---Organization Data---")]
    private Dictionary<CharacterId, IdentityInfo> organizationData = new Dictionary<CharacterId, IdentityInfo>();


    private void Start()
    {
        instance = this;
        Application.targetFrameRate = 30;

        SetUpData();

        organizationData.Clear();
    }


    /// <summary>
    /// 보유중인 인격 데이터 설정
    /// </summary>
    public void SetUpData()
    {
        characterEgoInfo = new List<IdentityInfo>();
        foreach (CharacterId characterId in System.Enum.GetValues(typeof(CharacterId)))
        {
            IdentityInfo egoInfo = new IdentityInfo();
            egoInfo.sinner = characterId;

            // 경로 지정
            string path = $"Identity/{characterId.ToString().ToUpper()}";

            // 경로 내 캐릭터 데이터 로드
            IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>(path);

            // 런타임 데이터 생성
            egoInfo.info = new List<IdentityData>(masters.Length);
            foreach (IdentityMasterSO master in masters)
            {
                // Json 이 없을 경우 데이터 생성 로직임!
                IdentityData data = new IdentityData();
                data.isUnlocked = false;
                data.level = 1;
                data.sync = 1;
                data.master = master;

                egoInfo.info.Add(data);
            }

            characterEgoInfo.Add(egoInfo);
        }
    }

    /// <summary>
    /// 캐릭터 클릭 -> 캐릭터 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenCharacterList(CharacterId id)
    {
        switch (id)
        {
            case CharacterId.ch01:
                break;

            case CharacterId.ch02:
                break;

            case CharacterId.ch03:
                break;

            case CharacterId.ch04:
                break;
        }
    }

    /// <summary>
    /// 편성 인격 변경하기
    /// </summary>
    /// <param name="info"></param>
    public void ChangeIdentity(IdentityInfo info)
    {
        // 딕셔너리에 저장
        if(organizationData.ContainsKey(info.sinner))
        {
            organizationData[info.sinner] = info;
        }
        else
        {
            organizationData.Add(info.sinner, info);
        }

    }
}


