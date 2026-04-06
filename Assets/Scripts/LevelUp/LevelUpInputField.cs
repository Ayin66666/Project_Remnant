using TMPro;
using UnityEngine;
using Item;


public class LevelUpInputField : MonoBehaviour
{
    [Header("---Setting---")]
    // 지금은 enum 타입인데 이거 id 기반으로 교체해야하나?
    [SerializeField] private int ticketType; 

    [Header("---UI---")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("---Component---")]
    [SerializeField] private LevelUpUI levelUpUI;


    /// <summary>
    /// 기본 정보 입력
    /// </summary>
    public void SetUp(LevelUpUI levelUpUI)
    {
        this.levelUpUI = levelUpUI;

        // UI 업데이트
        int count = GameManager.instance.inventory.GetItemCount(ticketType);
        countText.text = $"(보유 : {count})";
    }

    /// <summary>
    /// 인풋 필드에 작성된 값을 전달
    /// </summary>
    /// <returns></returns>
    public int GetInputData()
    {
        // 입력값 받아오기
        int inputValue = 0;
        bool change = int.TryParse(inputField.text, out inputValue);
        if (!change) return -1;

        // 보유 티켓 값 보다 더 많은 수치가 입력되었는지 체크
        int ticket = GameManager.instance.inventory.GetItemCount(ticketType);
        if (ticket < inputValue)
        {
            inputValue = ticket;
            inputField.text = inputValue.ToString();
        }

        // 입력값 반환
        return inputValue;
    }


    #region 버튼 이벤트
    /// <summary>
    /// 티켓 증가 버튼 클릭 시 호출
    /// </summary>
    public void AddTicket()
    {
        // 남은 경험치 체크
        int remainExp = levelUpUI.CalRemainingExp();
        if (remainExp <= 0)
        {
            Debug.Log("이미 최대치로 채움!");
            return;
        }

        // 보유 티켓 수 확인
        int ticket = GameManager.instance.inventory.GetItemCount(ticketType);
        if (ticket > levelUpUI.GetUsedTicketCount(ticketType))
        {
            // 보유 티켓보다 입력 값이 작다면 1 증가
            levelUpUI.SetAddExp(ticketType, 1);
            inputField.text = levelUpUI.GetUsedTicketCount(ticketType).ToString();
        }
        else
        {
            Debug.Log($"이미 보유 최대치까지 입력함 / {ticketType}");
        }
    }

    /// <summary>
    /// 티켓 감소 버튼 클릭 시 호출
    /// </summary>
    public void RemoveTicket()
    {
        // 보유 티켓 수 확인
        if (levelUpUI.GetUsedTicketCount(ticketType) > 0)
        {
            // 입력 값이 0보다 크다면 1 감소
            levelUpUI.SetAddExp(ticketType, -1);
            inputField.text = levelUpUI.GetUsedTicketCount(ticketType).ToString();
        }
    }

    public void AddAllTicket()
    {

    }

    public void RemoveAllTicket()
    {

    }
    #endregion
}
