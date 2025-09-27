using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public BuffType type;
    public int buff_Value;
    public int buff_Duration;
    public enum BuffType { AttackPoint, DefensePoint, DamageIncrease, DamageReduction }

    public void Buff_Check()
    {
        buff_Duration--;
    }
}

public abstract class Character_Base : MonoBehaviour, IDamageSystem
{
    [Header("---State---")]
    public CharacterType characterType;
    public State curState;
    public enum State { Idle, Move, Attack, Die }
    public enum CharacterType { Player, Enemy }


    [Header("---Status---")]
    public int hp;
    public int attackPoint;
    public int defensePoint;
    public Vector2Int minMax_Speed;

    // Anger, Lust, Sloth, Gluttony, Melancholy, Pride, Envy
    public Dictionary<IDamageSystem.SinType, float> sinDefence;
    public Dictionary<IDamageSystem.DamageType, float> typeDefence;


    [Header("---Buff & DeBuff---")]
    public List<Buff> buffList;
    public Dictionary<Buff.BuffType, int> buff = new Dictionary<Buff.BuffType, int>()
    {
        { Buff.BuffType.AttackPoint, 0 },
        { Buff.BuffType.DefensePoint, 0 },
        { Buff.BuffType.DamageIncrease, 0 },
        { Buff.BuffType.DamageReduction, 0 }
    };


    [Header("---Skill---")]
    public List<Skill_Base> skill_List;


    [Header("---Body---")]
    [SerializeField] private SpriteRenderer body;


    #region Buff
    public void Buff_Setting(Buff buff, bool isAdd)
    {
        if (isAdd)
        {
            buffList.Add(buff);
            this.buff[buff.type] += buff.buff_Value;
        }
        else
        {
            buffList.Remove(buff);
            this.buff[buff.type] -= buff.buff_Value;
        }
    }

    public void Buff_Check()
    {
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            buffList[i].Buff_Check();
            if (buffList[i].buff_Duration <= 0) Buff_Setting(buffList[i], false);
        }
    }
    #endregion


    #region Movement
    public void LookAt(bool isLeft)
    {
        body.flipX = isLeft;
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
    #endregion


    #region Hit
    public void TakeDamage(IDamageSystem.SinType sin, IDamageSystem.DamageType type, bool isCriticl, int hitcount, int damage)
    {
        // ġ��Ÿ ���
        float criticalValue = isCriticl ? 1.2f : 1f;

        // ���� ������
        float total_DamageReduction = 1f - (buff[Buff.BuffType.DamageReduction] / 10f);
        total_DamageReduction = Mathf.Clamp01(total_DamageReduction);

        // �� ����
        int total_DefencePoint = buff[Buff.BuffType.DefensePoint] + defensePoint;

        // ���� ������
        int calDamage = (int)((damage * criticalValue * (typeDefence[type] + sinDefence[sin]) - total_DefencePoint) * total_DamageReduction);
        if (calDamage < 0) calDamage = 0;

        // ü�� ����
        for (int i = 0; i < hitcount; i++)
        {
            hp -= calDamage; 
        }

        // ��� üũ
        if (hp <= 0) Die();
    }

    protected abstract void Die();
    #endregion


    #region Click Action
    /// <summary>
    /// Ŭ�� �� Player_Manager���� ������ ����
    /// </summary>
    /// <param name="isOn"></param>
    public void Click_Status()
    {
        switch (characterType)
        {
            // �÷��̾�� ���� UI
            case CharacterType.Player:
                break;

            // ���ʹ� ���� UI
            case CharacterType.Enemy:
                break;
        }
    }
    #endregion
}
