using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "CharacterList Data", menuName = "Character/CharacterList Data", order = int.MaxValue)]

public class Character_ListDataSO : ScriptableObject
{
    [Header("---Data---")]
    public Character character;
    public List<CharacterBody> characterBodyList;
    public List<CharacterEgo> characterEgoList;
}


[System.Serializable]
public class CharacterBody // �ΰ�
{
    public Player_Base body;
    public bool haveBody;
}


[System.Serializable]
public class CharacterEgo // ����
{
    public Skill_Base ego;
    public bool haveEgo;
}



