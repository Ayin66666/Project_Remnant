using UnityEngine;


public class OrganizationUI : MonoBehaviour
{
    public static OrganizationUI instance;

    [Header("---UI---")]
    [SerializeField] private GameObject selectListUI;
    [SerializeField] private GameObject egoListUI;
    [SerializeField] private GameObject identityDescriptionUI;


    private void Start()
    {
        instance = this;
    }


    /// <summary>
    /// 인격 리스트 표시
    /// </summary>
    /// <param name="isOn"></param>
    public void IdentityListUI(bool isOn)
    {
        selectListUI.SetActive(isOn);
        if(!isOn) egoListUI.SetActive(false);
    }

    /// <summary>
    /// 에고 UI 표시
    /// </summary>
    /// <param name="isOn"></param>
    public void EgoListUI(bool isOn)
    {
        egoListUI.SetActive(!isOn);
    }

    /// <summary>
    /// 인격 상세정보 표시
    /// </summary>
    public void ShowSinnerDescriptionUI(bool isOn, CharacterId sinner)
    {
        // UI 설정

        // UI On
        identityDescriptionUI.SetActive(isOn);
    }



    /// <summary>
    /// 인격 상세정보 - 에고 클릭 시 표시되는 정보
    /// </summary>
    /// <param name="info"></param>
    public void ShowEgoSlotDescription(bool isOn, EgoData info)
    {
        
    }
}
