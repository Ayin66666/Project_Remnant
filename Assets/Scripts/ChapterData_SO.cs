using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Chapter Data", menuName = "Chapter/Chapter Data", order = int.MaxValue)]
public class ChapterData_SO : ScriptableObject
{
    public string chatperName;
    public List<StageData> stageList;
}


[System.Serializable]
public class StageData
{
    public bool isClear;
    public string stageSceneName;
    public List<ClearCondition> clearConditions;
    public List<Character_Base> enemyData;
}

[System.Serializable]
public class ClearCondition
{
    public ExClear_Condition exClear;
    public int index;
}

