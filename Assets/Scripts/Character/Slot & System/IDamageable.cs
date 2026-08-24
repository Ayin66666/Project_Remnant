public interface IDamageable
{
    /// <summary>
    /// 이 함수 지금은 bool, int 를 개별 변수로 해서 받고 있지만, 
    /// 나중에 가면 결국 구조체나 데이터클래스를 사용하지 않을까 싶음
    /// </summary>
    /// <param name="isCri"></param>
    /// <param name="damage"></param>
    public void TakeDamage(AttackInfo info);
    public void Die();
}

