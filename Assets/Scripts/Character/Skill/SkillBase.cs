using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillBase : MonoBehaviour
{
    // 하는 동작
    // 1. 애니메이션 제어
    // 2. 이펙트 & 데미지 이벤트 함수
    // 3. 스킬의 세부 동작 & 조건은 상속받은 스크립트에서 구현

    [Header("---Setting---")]
    [SerializeField] private Crime crimeType;
    public Crime CrimeType => crimeType;

    [Header("---UI---")]
    [SerializeField] private Sprite icon;
    [SerializeField] private string skillName;
    [SerializeField] private string description;
    public Sprite Icon => icon;
    public string SkillName => skillName;
    public string Description => description;

    [Header("---Component---")]
    [SerializeField] private CharacterBase character;
    [SerializeField] private Animator anim;


    /// <summary>
    /// 기능 동작 함수
    /// </summary>
    public abstract IEnumerator Use();
}
