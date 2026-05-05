using Game.Character;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class SkillDescriptionSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private IdentityData identityData;
    [SerializeField] private SkillSO skillData;


    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI coinPowerText;
    [SerializeField] private RectTransform coinRect;
    [SerializeField] private GameObject coinPrefab;


    /// <summary>
    /// 데이터를 받아와서 슬롯에 설정
    /// </summary>
    /// <param name="skillSO"></param>
    public void SetUp(IdentityData identity, SkillSO skillSO)
    {
        // 보더 컬러는 어떻게? - 이거 속성에 따라서 변경 예정
        // 분노 = 빨강
        // 오만 = 파랑
        // 우울 = 하늘
        // 탐식 = 초록
        // 나태 = 노랑
        // 질투 = 보라
        // 색욕 = 주황

        // 초기화
        Clear();

        // 데이터 입력
        identityData = identity;
        skillData = skillSO;
        Debug.Log(skillSO);

        // UI 배치
        icon.sprite = skillSO.icon;
        coinPowerText.text = $"+ {skillSO.Skill[identity.sync].coinPower}";
        for (int i = 0; i < skillSO.Skill[identity.sync].coins.Count; i++)
        {
            GameObject obj = Instantiate(coinPrefab, coinRect);
        }
    }

    /// <summary>
    /// 슬롯 초기화 로직 - 혹시 모를 상황 대비
    /// </summary>
    public void Clear()
    {
        skillData = null;
        icon.sprite = null;
        coinPowerText.text = "";
    }


    #region Mouse Event
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skillData != null)
            GameManager.instance.characterDescription.ShowSkillSlotDescription(true, identityData, skillData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.instance.characterDescription.ShowSkillSlotDescription(false, identityData, null);
    }
    #endregion
}
