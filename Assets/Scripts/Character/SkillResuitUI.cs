using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SkillResuitUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private SkillSO skillSO;
    [SerializeField] private bool isOn;
    [SerializeField] private bool isCoinTossing;
    public bool IsCoinTossing => isCoinTossing;
    private Coroutine coinCoroutine;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI coinPowerText;
    [SerializeField] private TextMeshProUGUI totalPowerText;
    [SerializeField] private RectTransform coinRect;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> coins;


    /// <summary>
    /// 스킬 데이터를 받아 UI세팅 or 초기화
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="so"></param>
    public void SetUI(bool isOn, int sync, SkillSO so)
    {
        if (isOn)
        {
            // Null 체크
            if (so == null)
            {
                Debug.Log("스킬 SO가 null 상태에서 호출됨!");
                return;
            }

            // UI 세팅
            skillSO = so;
            icon.sprite = skillSO.Icon;
            skillNameText.text = skillSO.SkillName;
            coinPowerText.text = $"{skillSO.syncDatas[sync].coinPower}";
            totalPowerText.text = $"{skillSO.syncDatas[sync].originalPower}\n{skillSO.syncDatas[sync].originalPower + (skillSO.syncDatas[sync].coinPower * skillSO.syncDatas[sync].coins.Count)}";

            for (int i = 0; i < skillSO.syncDatas[sync].coins.Count; i++)
            {
                // 파불코면 빨강 & 아니면 하양색 + 투명도는 0.75 세팅
                GameObject coin = Instantiate(coinPrefab, coinRect);
                coin.GetComponent<Image>().color =
                    so.syncDatas[sync].coins[i].Coin == CoinType.Normal ? new Color(1, 1, 1, 0.75f) : new Color(1, 0, 0, 0.75f);
                coins.Add(coin);
            }
        }
        else
        {
            // UI 초기화
            skillSO = null;
            icon.sprite = null;
            skillNameText.text = string.Empty;
            coinPowerText.text = string.Empty;
            totalPowerText.text = string.Empty;
            foreach (var coin in coins)
            {
                Destroy(coin);
            }

            coins.Clear();
        }
    }

    /// <summary>
    /// 합 과정에서 코인이 앞면인지 뒷면인지 표시하는 함수
    /// </summary>
    /// <param name="coinIndex"></param>
    /// <param name="isFront"></param>
    public void SetCoin(bool[] isFront, int sync)
    {
        if (!isOn)
        {
            Debug.Log("스킬 UI가 꺼져있는 상태에서 호출됨!");
            return;
        }

        if (skillSO == null)
        {
            Debug.Log("스킬 SO가 null 상태에서 호출됨!");
            return;
        }

        isCoinTossing = true;

        if (coinCoroutine != null) StopCoroutine(coinCoroutine);
        coinCoroutine = StartCoroutine(CoSetCoin(isFront, sync));
    }

    private IEnumerator CoSetCoin(bool[] isFront, int sync)
    {
        // 코인 컬러 초기화
        ResetCoin();

        // 앞 뒷면에 따른 코인 컬러 세팅
        float delayTime = 0.5f / coins.Count;
        int totalPower = skillSO.syncDatas[sync].originalPower;
        for (int i = 0; i < coins.Count; i++)
        {
            // 코인 컬러 변경
            Image ima = coins[i].GetComponent<Image>();
            ima.color = new Color(ima.color.a, ima.color.g, ima.color.b, isFront[i] ? 1 : 0.75f);

            // 코인 파워 변경
            totalPower += isFront[i] ? skillSO.syncDatas[sync].coinPower : 0;
            totalPowerText.text = $"{totalPower}";

            // 대기 (연출)
            yield return new WaitForSeconds(delayTime);
        }

        isCoinTossing = false;
    }

    /// <summary>
    /// 코인 컬러 초기화 로직 - 혹시 몰라서 함수를 빼긴 했는데, 재사용 안한다면 그냥 코루틴 안에서 처리해도 됨
    /// </summary>
    public void ResetCoin()
    {
        foreach (var coin in coins)
        {
            Image iam = coin.GetComponent<Image>();
            iam.color = new Color(iam.color.a, iam.color.g, iam.color.b, 0.75f);
        }
    }
}
