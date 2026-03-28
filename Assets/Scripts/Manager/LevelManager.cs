using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [Header("---Identity Level Setting---")]
    [SerializeField] private int maxLevel = 90;
    [SerializeField] private int startExp = 500;
    [SerializeField] private int expIncrease = 108;
    [SerializeField] private List<int> expData;

    [Header("---Ego Level Setting---")]
    /// <summary>
    /// 동기화에 필요한 끈 & 파편 데이터
    /// </summary>
    [SerializeField] private List<EgoExp> egoExpList;

    [System.Serializable]
    public struct EgoExp
    {
        [SerializeField] private string name;
        public int fragment;
        public int cord;
    }




    private void Awake()
    {
        SetUp();
    }


    #region (Identity & Ego) Level & Sync Up
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
            return -1;
        else 
            return expData[level - 1];
    }

    /// <summary>
    /// 에고 강화에 필요한 재료 전달
    /// </summary>
    /// <param name="sync"></param>
    /// <returns></returns>
    public EgoExp GetEgoExp(int sync)
    {
        return egoExpList[sync];
    }
    #endregion
}
