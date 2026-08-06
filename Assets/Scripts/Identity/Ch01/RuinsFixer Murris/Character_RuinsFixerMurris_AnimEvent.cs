using UnityEngine;

public class Character_RuinsFixerMurris_AnimEvent : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private Character_RuinsFixerMurris charcter;


    #region 애니메이션 이벤트
    /// <summary>
    /// 공격 이펙트 호출
    /// </summary>
    /// <param name="index"></param>
    public void AttackEffect(int index)
    {
        // 이거 CharacterBase에 Effect() 함수를 만들어서 index 기반으로 이펙트를 호출할건지?
        // 아니면 각자 Character_Name에 List를 만들고 거기에 담아둘건지?
        // charcter.Effect(index);
    }

    /// <summary>
    /// 데미지 호출 (1-1 1타 50% 데미지)
    /// </summary>
    public void Attack1_1()
    {

    }   
    
    /// <summary>
    /// 데미지 호출 (1-2 5타 10% x 5 데미지)
    /// </summary>
    public void Attack1_2()
    {

    }
    #endregion
}
