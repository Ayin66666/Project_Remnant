using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    // [Header("---Setting---")]
    // [SerializeField] private List<PhaseData> phaseData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 스테이지 데이터를 받아서 맵 & 몬스터 세팅
    /// </summary>
    public void SetUp()
    {

    }
}
