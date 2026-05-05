using Game.Character;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EnemyIconUI : MonoBehaviour, IPointerClickHandler
{
    [Header("---Setting---")]
    [SerializeField] private EnemyMasterSO enemySO;
    [SerializeField] private Image characterIcon;

    public void SetUp(EnemyMasterSO so)
    {
        enemySO = so;
        characterIcon.sprite = so.portrait;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 시 해당 몬스터의 상세설명 UI 표시
        // 기능 구현 필요!
    }
}
