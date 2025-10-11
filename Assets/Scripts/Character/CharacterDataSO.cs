using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Status Data", menuName = "Character/Status Data", order = int.MaxValue)]
public class CharacterDataSO : MonoBehaviour
{
    [Header("---Setting---")]
    // ü��
    public int baseHp;
    public int hpUpByLevel;

    // �׷α�
    public int[] groggyPercent;

    // �ӵ�
    public Vector2Int baseMinMaxSpeed;

    // ����
    public int baseAttackPoint;
    public int attackPointUpbyLevel;

    // ���
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
