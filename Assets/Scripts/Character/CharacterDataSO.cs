using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Status Data", menuName = "Character/Status Data", order = int.MaxValue)]
public class CharacterDataSO : MonoBehaviour
{
    [Header("---Setting---")]
    // 체력
    public int baseHp;
    public int hpUpByLevel;

    // 그로기
    public int[] groggyPercent;

    // 속도
    public Vector2Int baseMinMaxSpeed;

    // 공격
    public int baseAttackPoint;
    public int attackPointUpbyLevel;

    // 방어
    public int baseDefencePoint;
    public int defencePointUpbyLevel;
    public List<SinDefence> sinDefence;
    public List<DamageDefence> typeDefence;


    [System.Serializable]
    public struct SinDefence
    {
        public IDamageSystem.SinType sinType;
        public float defenceValue;
    }

    [System.Serializable]
    public struct DamageDefence
    {
        public IDamageSystem.DamageType damageType;
        public float defenceValue;
    }
}
