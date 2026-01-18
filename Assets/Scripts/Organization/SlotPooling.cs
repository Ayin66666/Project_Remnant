using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SlotPooling : MonoBehaviour
{
    [Header("---Identity Slot---")]
    [SerializeField] private List<CharacterSlot> selectSlot;
    [SerializeField] private GameObject inentitySlotPrefab;
    [SerializeField] private RectTransform identityListRect;

    [Header("---Ego Slot---")]
    [SerializeField] private List<EgoSlot> egoSlot;
    [SerializeField] private GameObject egoSlotPrefab;
    [SerializeField] private RectTransform egoListRect;


    private void Awake()
    {
        SlotSetUp();
    }

    /// <summary>
    /// 최초 1회 슬롯 설정
    /// </summary>
    public void SlotSetUp()
    {
        // 인격 슬롯
        selectSlot = new List<CharacterSlot>();
        for (int i = 0; i < 30; i++)
        {
            GameObject slot = Instantiate(inentitySlotPrefab, identityListRect);
            selectSlot.Add(slot.GetComponent<CharacterSlot>());
            slot.SetActive(false);
        }

        // 에고 슬롯
        egoSlot = new List<EgoSlot>();
        for (int i = 0; i < 30; i++)
        {
            GameObject slot = Instantiate(egoSlotPrefab, egoListRect);
            egoSlot.Add(slot.GetComponent<EgoSlot>());
            slot.SetActive(false);
        }
    }


    #region Identity Slot
    /// <summary>
    /// 슬롯 보내기 & 여유분이 없는 경우 생성
    /// </summary>
    /// <returns></returns>
    public CharacterSlot GetIdentitySlot()
    {
        CharacterSlot slot = selectSlot.FirstOrDefault(s => !s.gameObject.activeSelf);
        if (slot == null)
        {
            slot = Instantiate(inentitySlotPrefab, identityListRect).GetComponent<CharacterSlot>();
            selectSlot.Add(slot);
        }

        slot.Clear();
        slot.gameObject.SetActive(true);
        return slot;
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void ClearIdentitySlot()
    {
        foreach (CharacterSlot s in selectSlot)
        {
            s.Clear();
            s.gameObject.SetActive(false);
        }
    }
    #endregion


    #region Ego Slot
    /// <summary>
    /// 슬롯 보내기 & 여유분이 없는 경우 생성
    /// </summary>
    /// <returns></returns>
    public EgoSlot GetEgoSlot()
    {
        EgoSlot slot = egoSlot.FirstOrDefault(s => !s.gameObject.activeSelf);
        if (slot == null)
        {
            slot = Instantiate(egoSlotPrefab, egoListRect).GetComponent<EgoSlot>();
            egoSlot.Add(slot);
        }
        slot.Clear();
        slot.gameObject.SetActive(true);
        return slot;
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void ClearEgoSlot()
    {
        foreach (EgoSlot s in egoSlot)
        {
            s.Clear();
            s.gameObject.SetActive(false);
        }
    }
    #endregion
}
