using System.Collections.Generic;
using UnityEngine;


public class OrganizationManager : MonoBehaviour
{
    [Header("---Setting / Data---")]
    [SerializeField] private List<IdentityInfo> characterEgoInfo;

    [Header("---UI---")]
    [SerializeField] private GameObject characterListSet;
    [SerializeField] private List<CharacterSlot> characterSlot;

    private void Start()
    {
        Application.targetFrameRate = 30;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SetUp();
        }
    }

    public void SetUp()
    {
        characterEgoInfo = new List<IdentityInfo>();
        foreach(CharacterId characterId in System.Enum.GetValues(typeof(CharacterId)))
        {
            IdentityInfo egoInfo = new IdentityInfo();
            egoInfo.character = characterId;
            
            // 경로 지정
            string path = $"Identity/{characterId.ToString().ToUpper()}";

            // 경로 내 캐릭터 데이터 로드
            IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>(path);

            // 런타임 데이터 생성
            egoInfo.info = new List<IdentityData>(masters.Length);
            foreach(IdentityMasterSO master in masters)
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
}
