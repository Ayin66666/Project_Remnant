using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharacterData
{
    public bool isDie;
    public bool isSally;
    public int Organization_Order;
    public Character_Base character;
}

public class Player_Turn_Manager : MonoBehaviour
{
    public static Player_Turn_Manager instance;


    [Header("---Setting---")]
    public List<CharacterData> characterList;


    [Header("---Player Resource---")]
    public Dictionary<IDamageSystem.SinType, int> sinResource = new Dictionary<IDamageSystem.SinType, int>
    {
        
        { IDamageSystem.SinType.Anger, 0 }, // �г�
        { IDamageSystem.SinType.Lust, 0 }, // ����
        { IDamageSystem.SinType.Sloth, 0 }, // ����
        { IDamageSystem.SinType.Gluttony, 0 }, // Ž��
        { IDamageSystem.SinType.Melancholy, 0 }, // ���
        { IDamageSystem.SinType.Pride, 0 }, // ����
        { IDamageSystem.SinType.Envy, 0 } // ����
    };


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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click_Character();
        }
    }

    /// <summary>
    /// �� ���� �� �÷��̾� �߰� ���� üũ
    /// </summary>
    public void Player_Check()
    {
        // ��� üũ
        
        // ���� ��� ĳ���� ����

        // ���
    }

    /// <summary>
    /// �������� ���� �� ��â���� ������ ������ ����
    /// </summary>
    public void EgoData_Setting()
    {

    }

    /// <summary>
    /// ĳ���� Ŭ�� �� ����
    /// </summary>
    public void Click_Character()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            Character_Base character = hit.collider.GetComponent<Character_Base>();
            if (character != null)
            {
                character.Click_Status();
            }
        }
    }
}
