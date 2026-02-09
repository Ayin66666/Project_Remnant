using System.Collections.Generic;
using UnityEngine;


public class ExpManager : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private int maxLevel = 90;
    [SerializeField] private int startExp = 500;
    [SerializeField] private int expIncrease = 108;


    [Header("---Data---")]
    [SerializeField] private List<int> expData;


    private void Awake()
    {
        SetUp();
    }


    /// <summary>
    /// 각 레벨별 필요한 경험치 데이터 세팅
    /// </summary>
    public void SetUp()
    {
        expData = new List<int>(maxLevel - 1);

        for (int level = 1; level < maxLevel; level++)
        {
            int needExp = startExp + expIncrease * (level - 1);
            expData.Add(needExp);
        }
    }

    /// <summary>
    /// 다음 레벨까지 필요한 경험치 반환
    /// </summary>
    public int GetNeedExp(int level)
    {
        if (level >= maxLevel) 
            return 0;
        else 
            return expData[level - 1];
    }
}
