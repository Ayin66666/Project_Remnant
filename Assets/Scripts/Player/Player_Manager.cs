using UnityEngine;


public class Player_Manager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click_Character();
        }
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
