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

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI coinPowerText;
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
            if(so == null)
            {
                Debug.Log("스킬 SO가 null 상태에서 호출됨!");
                return;
            }

            // UI 세팅
            skillSO = so;
            icon.sprite = skillSO.Icon;
            skillNameText.text = skillSO.SkillName;
            coinPowerText.text = $"{skillSO.syncDatas[sync].coinPower}";
            for (int i = 0; i < skillSO.syncDatas[sync].coins.Count; i++)
            {
                // 회색 상태로 코인 소환
                GameObject coin = Instantiate(coinPrefab, coinRect);
                coin.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
            foreach (var coin in coins)
            {
                Destroy(coin);
            }

            coins.Clear();
        }
    }


    /// <summary>
    /// 스킬의 코인이 앞면인지 뒷면인지 표시하는 함수
    /// </summary>
    /// <param name="coinIndex"></param>
    /// <param name="isFront"></param>
    public void SetCoin(bool[] isFront, float delayTime)
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
    }


    /// <summary>
    /// 코인 토스 후 최종 위력을 표시하는 함수
    /// </summary>
    /// <param name="power"></param>
    public void SetTotalCoinPower(int power, float delayTime)
    {
        StartCoroutine(CoSetToralCoinPower(power, delayTime));
    }

    private IEnumerator CoSetToralCoinPower(int power, float deleyTime)
    {
        yield return null;
    }
}
