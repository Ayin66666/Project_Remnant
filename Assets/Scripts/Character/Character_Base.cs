using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class Groggy
{
    public int groggyValue;
    public bool isused;
}


public abstract class Character_Base : MonoBehaviour, IDamageSystem, IBuffUse
{
    [Header("---State---")]
    public CharacterType characterType;
    public State curState;
    public enum State { Idle, Move, Attack, Groggy, Die }
    public enum CharacterType { Player, Enemy }


    [Header("---Status---")]
    public int curHp;
    public int maxHp;
    public List<Groggy> groggy_Gauge;
    public int attackPoint;
    public int defensePoint;
    public Vector2Int minMax_Speed;
    private int groggy_Turn;

    // Anger, Lust, Sloth, Gluttony, Melancholy, Pride, Envy
    public Dictionary<IDamageSystem.SinType, float> sinDefence;
    public Dictionary<IDamageSystem.DamageType, float> typeDefence;


    [Header("---Buff & DeBuff---")]
    [SerializeField] private List<Buff_Base> buff_Noraml;
    [SerializeField] private Dictionary<BuffType, Buff_Base> buff_keyword;


    [Header("---Body---")]
    [SerializeField] private SpriteRenderer body;


    [Header("---Attack Slot---")]
    [SerializeField] private List<Attack_Slot> attackSlot;


    #region Buff
    public void BuffUse(IBuffEffect effect)
    {
        effect.Use();
    }

    /// <summary>
    /// �Ϲ�(�������ͽ�) ���� & ����� �߰�
    /// </summary>
    /// <param name="buff"></param>
    public void BuffNormal_Add(Buff_Base buff)
    {
        bool found = false;
        foreach (Buff_Base value in buff_Noraml)
        {
            if (value.type == buff.type)
            {
                // ��ġ ����
                found = true;
                value.buff_Value += buff.buff_Value;
                value.buff_Duration = buff.buff_Duration;
                break;
            }
        }

        if (!found)
        {
            buff_Noraml.Add(buff);
        }
    }

    /// <summary>
    /// Ű����(7��) ���� & ����� �߰�
    /// </summary>
    /// <param name="buff"></param>
    public void BuffKeyword_Add(Buff_Base buff)
    {
        if (buff_keyword.TryGetValue(buff.type, out var baseBuff))
        {
            baseBuff.buff_Value += buff.buff_Value;
            baseBuff.buff_Duration += buff.buff_Duration;

            // 6�� Ű���� ���� Ÿ�� ���� [����) ���� -> ����-�۷�] 
            baseBuff.Type_Change(buff);
        }
        else
        {
            buff_keyword[buff.type] = buff;
        }
    }

    /// <summary>
    /// ���� & ����� üũ �� ����
    /// </summary>
    public void Buff_Check()
    {
        // �Ϲ� ���� üũ
        for (int i = buff_Noraml.Count - 1; i >= 0; i--)
        {
            buff_Noraml[i].UpdateBuff();
            if (buff_Noraml[i].buff_Duration <= 0)
            {
                buff_Noraml.RemoveAt(i);
            }
        }

        // Ű���� ���� üũ
        var list = buff_keyword.Keys.ToList();
        foreach (var key in list)
        {
            var buff = buff_keyword[key];
            buff.UpdateBuff();

            if (buff.buff_Duration <= 0) buff_keyword.Remove(key);
        }
    }
    #endregion

    #region Fight
    /// <summary>
    /// ���� ���� �ӵ� ����
    /// </summary>
    public void Speed_Setting()
    {

    }

    /// <summary>
    /// ���� �߰�
    /// </summary>
    public void Slot_Add()
    {
        
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
        float total_DamageReduction = 1f /* - (buff[Buff.BuffType.DamageReduction] / 10f)*/;
        total_DamageReduction = Mathf.Clamp01(total_DamageReduction);

        // �� ����
        int total_DefencePoint = /*buff[Buff.BuffType.DefensePoint]*/ +defensePoint;

        // ���� ������
        int calDamage = (int)((damage * criticalValue * (typeDefence[type] + sinDefence[sin]) - total_DefencePoint) * total_DamageReduction);
        if (calDamage < 0) calDamage = 0;

        // ü�� ����
        for (int i = 0; i < hitcount; i++)
        {
            curHp -= calDamage;
        }

        // �׷α� üũ
        Groggy_Check();

        // ��� üũ
        if (curHp <= 0) Die();

    }

    public void TakeBuffDamage(IDamageSystem.DamageTarget target, int damage)
    {
        // Buff_Base ���� character_base�� ��� �ִ� Use() ���� ȣ���ؾ��ϳ�?

        switch (target)
        {
            case IDamageSystem.DamageTarget.Hp:
                curHp -= damage;
                if (curHp <= 0) Die();
                else Groggy_Check();
                break;

            case IDamageSystem.DamageTarget.Groggy:
                for (int i = 0; i < groggy_Gauge.Count; i++)
                {
                    if (!groggy_Gauge[i].isused)
                        groggy_Gauge[i].groggyValue -= damage;
                }

                Groggy_Check();
                break;
        }
    }

    private void Groggy_Check()
    {
        bool isGroggy = false;
        for (int i = 0; i < groggy_Gauge.Count; i++)
        {
            if (!groggy_Gauge[i].isused && curHp <= groggy_Gauge[i].groggyValue)
            {
                isGroggy = true;
                groggy_Gauge[i].isused = true;
            }
        }

        if (isGroggy) Groggy();
    }

    protected void Groggy()
    {
        curState = State.Groggy;
        groggy_Turn = Stage_Manager.instance.turnCount;
    }

    protected void GroggyOff()
    {
        if(Stage_Manager.instance.turnCount > groggy_Turn)
        {
            curState = State.Idle;
        }
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
