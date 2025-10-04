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
        
        { IDamageSystem.SinType.Anger, 0 }, // 분노
        { IDamageSystem.SinType.Lust, 0 }, // 색욕
        { IDamageSystem.SinType.Sloth, 0 }, // 나태
        { IDamageSystem.SinType.Gluttony, 0 }, // 탐식
        { IDamageSystem.SinType.Melancholy, 0 }, // 우울
        { IDamageSystem.SinType.Pride, 0 }, // 오만
        { IDamageSystem.SinType.Envy, 0 } // 질투
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
    /// 턴 시작 시 플레이어 추가 여부 체크
    /// </summary>
    public void Player_Check()
    {
        // 사망 체크
        
        // 다음 출격 캐릭터 설정

        // 출격
    }

    /// <summary>
    /// 스테이지 시작 시 편성창에서 선택한 데이터 설정
    /// </summary>
    public void EgoData_Setting()
    {

    }

    /// <summary>
    /// 캐릭터 클릭 시 동작
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
