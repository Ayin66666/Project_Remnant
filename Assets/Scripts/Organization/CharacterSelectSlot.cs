using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CharacterSelectSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("---Setting---")]
    [SerializeField] private EgoData characterInfo;


    [Header("---UI---")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image border;
    [SerializeField] private GameObject[] rankIcon;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;


    public void SetUp()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 
    }
}
