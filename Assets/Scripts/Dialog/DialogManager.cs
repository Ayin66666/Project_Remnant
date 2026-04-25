using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogManager : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private Dictionary<int, GameObject> spriteDic;
    [SerializeField] private GameObject characterSpritePrefab;

    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI AffiliationText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private RectTransform spriteRect;


    /// <summary>
    /// 대화 데이터를 전달받은 후 진행
    /// </summary>
    public void SetUp()
    {

    }

    /// <summary>
    /// 클릭 & F키로 바로 다음 대화로 넘어가기
    /// </summary>
    public void NextDialog()
    {

    }

    /// <summary>
    /// 대화 로그 On/Off
    /// </summary>
    /// <param name="isOn"></param>
    public void DialogLog(bool isOn)
    {

    }

    /// <summary>
    /// 대화 즉시 종료
    /// </summary>
    public void Skip()
    {

    }
}
