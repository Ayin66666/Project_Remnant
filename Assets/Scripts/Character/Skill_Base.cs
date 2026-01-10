using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill_Base : MonoBehaviour
{
    // 하는 동작
    // 1. 스킬 벨류 (공격 타입, 타격 횟수, 배율)
    // 2. 타격 횟수
    // 3. 공격 타입
    // 4. 세부 동작 - Use() 함수만 선언
    // 5. 스킬의 세부 기능 & 조건은 상속받은 스크립트에서 구현

    [Header("---Setting---")]
    public AttackType type;
    [SerializeField] private List<CoinInfo> coins;
    public enum AttackType { Slash, Pierce, Blunt }

    [Header("---Component---")]
    [SerializeField] private Character_Base character;
    [SerializeField] private Animator anim;


    [System.Serializable]
    public struct CoinInfo
    {
        public float value;
        public int attackCount;
    }


    /// <summary>
    /// 기능 동작 함수
    /// </summary>
    public abstract IEnumerator Use();
}
