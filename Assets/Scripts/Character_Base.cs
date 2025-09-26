using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character_Base : MonoBehaviour, IDamageSystem
{
    [Header("---State---")]
    public State curState;
    public enum State { Idle, Move, Attack, Die }

    [Header("---Status---")]
    public int hp;
    public int attackPoint;
    public int defensePoint;
    public Vector2Int minMax_Speed;

    // Anger, Lust, Sloth, Gluttony, Melancholy, Pride, Envy
    public Dictionary<IDamageSystem.SinType, float> sinDefence;
    public Dictionary<IDamageSystem.DamageType, float> typeDefence;


    [Header("---Body---")]
    [SerializeField] private GameObject body;


    [Header("---Buff Debuff---")]
    public int buff_AttackPoint;
    public int buff_defensePoint;
    public float buff_Damage;
    public float buff_Defecne;

    public void LookAt()
    {

    }

    public void Move(Vector3 movePos, float moveSpeed)
    {
        StartCoroutine(Movement(movePos, moveSpeed));
    }

    private IEnumerator Movement(Vector3 movePos, float moveSpeed)
    {
        curState = State.Move;
        Vector3 startPos = transform.position;
        Vector3 endPos = movePos;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / moveSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return null;
        }

        transform.position = endPos;
        curState = State.Idle;
    }

    public void TakeDamage(IDamageSystem.SinType sin, IDamageSystem.DamageType type,  bool isCriticl, int hitcount, int damage)
    {
        float criticalValue = isCriticl ? 1.2f : 1f;
        float totalDefenceValue = Mathf.Clamp(buff_Defecne, 0.1f, 1);
        int totalDefencePoint = buff_defensePoint + defensePoint;
        int calDamage = (int)((damage * criticalValue * (1 * (typeDefence[type] + sinDefence[sin])) - (totalDefencePoint * 0.1f)) * totalDefenceValue);
    }
}
