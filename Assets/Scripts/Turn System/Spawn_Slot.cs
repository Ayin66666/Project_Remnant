using UnityEngine;


public class Spawn_Slot : MonoBehaviour
{
    [Header("---Setting---")]
    private GameObject character;
    public bool haveCharacter;


    public void Spawn(GameObject character)
    {
        this.character = character;
        haveCharacter = true;
    }

    public void Die()
    {
        character = null;
        haveCharacter = false;
    }
}
