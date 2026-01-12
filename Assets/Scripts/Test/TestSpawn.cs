using UnityEngine;


public class TestSpawn : MonoBehaviour
{
    [SerializeField] private CharacterInfo info;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GameObject obj = Instantiate(info.Spawn(), transform.position, Quaternion.identity);
            CharacterBase b = obj.GetComponent<CharacterBase>();

            b.SetUp(info.level, info.sync);
        }
    }
}
